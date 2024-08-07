﻿using IdentityModel;
using IdentityServer4;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Web;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;

namespace OAuthServer.Controllers
{
    [AllowAnonymous]
    [Route("")]
    public class LoginController : Controller
    {
        private readonly IIdentityServerInteractionService interaction;
        private readonly IEventService events;
        private readonly IProfileService profileService;
        private readonly IClientStore clientStore;
        private readonly IAuthenticationSchemeProvider authenticationSchemeProvider;
        private readonly IConfiguration _configuration;

        public LoginController(
            IIdentityServerInteractionService interaction,
            IEventService events,
            IProfileService profileService,
            IClientStore clientStore,
            IAuthenticationSchemeProvider authenticationSchemeProvider, IConfiguration configuration)
        {
            this.interaction = interaction;
            this.events = events;
            this.profileService = profileService;
            this.clientStore = clientStore;
            this.authenticationSchemeProvider = authenticationSchemeProvider;
            _configuration = configuration;
        }



        /// <summary>
        /// Entry point into the login workflow
        /// </summary>
        [HttpGet("login")]
        public async Task<IActionResult> Login(string returnUrl)
        {
            var scheme = await GetAuthenticationSchemeForClient(returnUrl);
            if (string.IsNullOrEmpty(scheme)) return BadRequest($"No client defined for {returnUrl}");
            if (!string.IsNullOrEmpty(_configuration["ISSUER_URI"]))
            {
                return Redirect(_configuration["ISSUER_URI"] + $"/challenge?scheme={scheme}&returnUrl={HttpUtility.UrlEncode(returnUrl)}");
            }
            else
            {
                return RedirectToAction(nameof(Challenge), new { scheme, returnUrl });
            }
        }

        

        /// <summary>
        /// initiate roundtrip to external authentication provider
        /// </summary>
        [HttpGet("challenge")]
        public IActionResult Challenge(string scheme, string returnUrl)
        {
            if (!Url.IsLocalUrl(returnUrl) && !interaction.IsValidReturnUrl(returnUrl)) return BadRequest($"No client defined for {returnUrl}");

            var props = new AuthenticationProperties
            {
                RedirectUri = Url.Action(nameof(Callback)),
                Items =
            {
                { "returnUrl", returnUrl },
                { "scheme", scheme },
            }
            };

            return Challenge(props, scheme);
        }

        /// <summary>
        /// Post processing of external authentication
        /// </summary>
        [HttpGet("callback")]
        public async Task<IActionResult> Callback()
        {
            Serilog.Log.Logger.Information("**** REACHED CALLBACK ****");

            // read external identity from the temporary cookie

            // adjust the cookie path.

            foreach (var item in HttpContext.Request.Headers)
            {
                Serilog.Log.Information($"{item.Key} = {item.Value}");
            }

            foreach (var cookie in HttpContext.Request.Cookies)
            {
                Serilog.Log.Information($"{cookie.Key} = {cookie.Value}");
            }
            

            var result = await HttpContext.AuthenticateAsync("IdCookie");
            if (result?.Succeeded != true)
            {
                Serilog.Log.Error(result.Failure,"External authentication error");
                //throw new Exception("External authentication error ");
            }

            // retrieve claims of the external user
            var externalUser = result.Principal;
            if (externalUser == null)
            {
                throw new Exception("External authentication error");
            }

            // retrieve claims of the external user
            var userId = externalUser.FindFirstValue(ClaimTypes.NameIdentifier);
            var sessionId = externalUser.FindFirstValue(JwtClaimTypes.SessionId);
            var scheme = result.Properties.Items["scheme"];

            // retrieve returnUrl

            var returnUrl = result.Properties.Items["returnUrl"] ?? "~/";

            string returnUrlPrefix = "";
            if (!string.IsNullOrEmpty(_configuration["RETURN_PREFIX"]))
            {
                int questionPos = result.Properties.Items["returnUrl"].IndexOf("?");
                returnUrl = _configuration["RETURN_PREFIX"] + _configuration["BASE_PATH"] +
                            "/connect/authorize" + result.Properties.Items["returnUrl"].Substring(questionPos);
            }
            

            // use the user information to find your user in your database, or provision a new user
            //var user = FindUserFromExternalProvider(scheme, userId);

            var additionalClaims = new List<Claim>();
            if (sessionId != null) additionalClaims.Add(new Claim(JwtClaimTypes.SessionId, sessionId));
            additionalClaims.AddRange(externalUser.Claims);

            foreach (var claim in additionalClaims)
            {
                Serilog.Log.Logger.Information($"{claim.Type}:{claim.Value}");
            }

            // issue authentication cookie for user
            var user = new IdentityServerUser(userId)
            {
                DisplayName = externalUser.FindFirstValue("given_name") + " " + externalUser.FindFirstValue("family_name"),
                IdentityProvider = scheme,
                AuthenticationTime = DateTime.Now,
                AdditionalClaims = additionalClaims
            }.CreatePrincipal();

            await HttpContext.SignInAsync(user);

            // delete temporary cookie used during external authentication
            await HttpContext.SignOutAsync(IdentityServerConstants.ExternalCookieAuthenticationScheme);

            Serilog.Log.Logger.Information($"Redirecting to {returnUrl}");

            // return back to protocol processing
            return Redirect(returnUrl);



        }

        /// <summary>
        /// Entry point into the logout workflow
        /// </summary>
        [HttpGet("logout")]
        public async Task<IActionResult> Logout(string logoutId)
        {
            if (HttpContext.User?.Identity.IsAuthenticated != true) return Ok();

            await HttpContext.SignOutAsync();

            var logoutContext = await interaction.GetLogoutContextAsync(logoutId);

            //var idp = User.FindFirst(JwtClaimTypes.IdentityProvider)?.Value;
            //if (idp != null && idp != IdentityServerConstants.LocalIdentityProvider)
            //{
            //    //signout from an external provider if it supports logout
            //    var providerSupportsSignout = await HttpContext.GetSchemeSupportsSignOutAsync(idp);
            //    if (providerSupportsSignout)
            //    {
            //        // build a return URL so the upstream provider will redirect back
            //        // to us after the user has logged out. this allows us to then
            //        // complete our single sign-out processing.
            //        string url = Url.Action("Logout", new { logoutId = logoutId });

            //        // this triggers a redirect to the external provider for sign-out
            //        return SignOut(new AuthenticationProperties { RedirectUri = url }, idp);
            //    }
            //}

            return Redirect(logoutContext.PostLogoutRedirectUri);
        }

        private async Task<string> GetAuthenticationSchemeForClient(string returnUrl)
        {
            var context = await interaction.GetAuthorizationContextAsync(returnUrl);
            var client = await clientStore.FindEnabledClientByIdAsync(context.Client.ClientId);

            var scheme = context?.IdP != null
                ? await authenticationSchemeProvider.GetSchemeAsync(context.IdP)
                : (await authenticationSchemeProvider.GetAllSchemesAsync()).FirstOrDefault(s => client.IdentityProviderRestrictions.Contains(s.Name));

            return scheme.Name;
        }
    }
}