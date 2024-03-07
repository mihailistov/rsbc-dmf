﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rsbc.Dmf.CaseManagement.Service;
using Rsbc.Dmf.DriverPortal.Api.Services;
using Rsbc.Dmf.DriverPortal.ViewModels;

namespace Rsbc.Dmf.DriverPortal.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = Policy.Driver)]
    public class DriverController : Controller
    {
        private readonly CaseManager.CaseManagerClient _cmsAdapterClient;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILogger<DriverController> _logger;

        public DriverController(CaseManager.CaseManagerClient cmsAdapterClient, IUserService userService, IMapper mapper, ILoggerFactory loggerFactory)
        {
            _cmsAdapterClient = cmsAdapterClient;
            _userService = userService;
            _mapper = mapper;
            _logger = loggerFactory.CreateLogger<DriverController>();
        }

        /// <summary>
        /// Get case submissions, submission requirements, and letters to driver documents for a given driver
        /// NOTE that this retrieves all documents for the driver and there is no guarantee that a document is linked to a case
        /// </summary>
        /// <returns>CaseDocuments</returns>
        [HttpGet("Documents")]
        [ProducesResponseType(typeof(CaseDocuments), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        [ActionName("GetCaseDocuments")]
        public async Task<ActionResult> GetCaseDocuments()
        {
            var profile = await _userService.GetCurrentUserContext();

            var driverIdRequest = new DriverIdRequest() { Id = profile.DriverId };
            var reply = _cmsAdapterClient.GetDriverDocumentsById(driverIdRequest);
            if (reply.ResultStatus == ResultStatus.Success)
            {
                var result = new CaseDocuments();
                var replyItemsExcludingUploaded = reply.Items.Where(i => i.SubmittalStatus != "Uploaded");
                foreach (var item in replyItemsExcludingUploaded)
                {
                    var document = _mapper.Map<Document>(item);

                    switch (document.SubmittalStatus)
                    {
                        case "Received":
                            // exclude documents with no key
                            if (!string.IsNullOrEmpty(document.DocumentUrl))
                            {
                                result.CaseSubmissions.Add(document);
                            }
                            break;
                        case "Open-Required":
                            // this category by design has documents with no key
                            result.SubmissionRequirements.Add(document);
                            break;
                        case "Issued":
                            // exclude documents with no key
                            // Issued is the new status for Letter outs and these are displayed in Letter To Driver Tab
                            if (!string.IsNullOrEmpty(document.DocumentUrl))
                            {
                                result.LettersToDriver.Add(document);
                            }
                            break;
                    }
                }

                // sort the documents
                if (result.CaseSubmissions.Count > 0)
                {
                    result.CaseSubmissions = result.CaseSubmissions.OrderByDescending(cs => cs.ImportDate).ToList();
                }

                if (result.SubmissionRequirements.Count > 0)
                {
                    result.SubmissionRequirements = result.SubmissionRequirements.OrderByDescending(cs => cs.ImportDate).ToList();
                }

                if (result.LettersToDriver.Count > 0)
                {
                    result.LettersToDriver = result.LettersToDriver.OrderByDescending(cs => cs.ImportDate).ToList();
                }

                return Json(result);
            }
            else
            {
                _logger.LogError($"{nameof(GetCaseDocuments)} failed for driverId: {profile.DriverId}", reply.ErrorDetail);
                return StatusCode(500, reply.ErrorDetail);
            }
        }

        /// <summary>
        /// Get all documents for a given driver but filter out documents without a url
        /// </summary>
        /// <returns>CaseDocuments</returns>
        [HttpGet("AllDocuments")]
        [ProducesResponseType(typeof(IEnumerable<Document>), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        [ActionName("GetAllDocuments")]
        public async Task<ActionResult> GetAllDocuments()
        {
            var profile = await _userService.GetCurrentUserContext();

            var driverIdRequest = new DriverIdRequest() { Id = profile.DriverId };
            var reply = _cmsAdapterClient.GetDriverDocumentsById(driverIdRequest);
            if (reply.ResultStatus == ResultStatus.Success)
            {
                // This includes all the documents except Open Required, Issued, Sent documents on Submission History Tab
                var replyItemsWithDocuments = reply.Items
                    .Where(i => !string.IsNullOrEmpty(i.DocumentUrl))
                    .Where(i => i.SubmittalStatus != "Open-Required" && i.SubmittalStatus != "Issued" && i.SubmittalStatus != "Sent" );
                var result = _mapper.Map<List<Document>>(replyItemsWithDocuments);

                // sort the documents
                if (result.Count > 0)
                {
                    result = result.OrderByDescending(cs => cs.ImportDate).ToList();
                }

                return Json(result);
            }
            else
            {
                _logger.LogError($"{nameof(GetAllDocuments)} failed for driverId: {profile.DriverId}", reply.ErrorDetail);
                return StatusCode(500, reply.ErrorDetail);
            }
        }
    }
}