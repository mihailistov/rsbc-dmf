﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Hl7.Fhir.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Rsbc.Dmf.PhsaAdapter.ViewModels;

namespace Rsbc.Dmf.PhsaAdapter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FhirController : ControllerBase
    {
        private readonly ILogger<ReceiveController> _logger;
        private readonly IConfiguration Configuration;

        public FhirController(ILogger<ReceiveController> logger, IConfiguration configuration)
        {
            _logger = logger;
            Configuration = configuration;
        }

        [HttpGet(".well-known/smart-configuration")]
        [AllowAnonymous]
        public SmartConfiguration GetSmartConfiguration()
        {
            List<string> capabilities = new List<string>();
            capabilities.Add("launch-ehr");
            capabilities.Add("client-public");
            capabilities.Add("client-confidential-symmetric");
            capabilities.Add( "context-ehr-patient");
            capabilities.Add("sso-openid-connect");
            SmartConfiguration result = new SmartConfiguration()
            {
                Authorization_endpoint = Configuration["FHIR_AUTHORIZATION_ENDPOINT"],
                Token_endpoint = Configuration["FHIR_TOKEN_ENDPOINT"],
                Introspection_endpoint = Configuration["FHIR_INTROSPECTION_ENDPOINT"],
                Capabilities = capabilities 
            };
            return result;
        }

        [HttpGet("Patient/{id}")]
        [AllowAnonymous]
        public Patient GetPatient([FromRoute] string id)
        {
            Patient result = new Patient()
            {
                Id = id,
                Name = new List<HumanName>(){new HumanName(){
                    Given = new List<string>() {"Test"},
                    Family = "Patient"
                }},
                BirthDateElement = new Date(DateTimeOffset.Now.Year - 20, DateTimeOffset.Now.Month,
                    DateTimeOffset.Now.Day)
            };
            return result;
        }

        [HttpGet("Practitioner/{id}")]
        [AllowAnonymous]
        public Practitioner GetPractitioner([FromRoute] string id)
        {
            Practitioner result = new Practitioner()
            {
                Id = id,
                Name = new List<HumanName>(){new HumanName(){
                    Given = new List<string>() {"Test"},
                    Family = "Patient"
                }},
                BirthDateElement = new Date(DateTimeOffset.Now.Year - 30, DateTimeOffset.Now.Month,
                    DateTimeOffset.Now.Day)
            };
            return result;
        }

        [HttpGet("Bundle/{id}")]
        [AllowAnonymous]
        public Bundle GetBundle([FromRoute] string id)
        {
            Bundle result = new Bundle()
            {
                Id = Guid.NewGuid().ToString()
            };
            return result;
        }

        [HttpPut("Bundle/{id}")]
        [AllowAnonymous]
        public void PutBundle([FromBody] Bundle bundle, [FromRoute] string id)
        {
            // do something with bundle or id.
            _logger.LogInformation(JsonSerializer.Serialize(bundle));
        }

    }
}