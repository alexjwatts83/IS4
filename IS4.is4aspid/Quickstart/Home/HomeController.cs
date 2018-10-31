// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Linq;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
//using IS4.is4aspid.Quickstart.Home;

namespace IdentityServer4.Quickstart.UI
{
    [SecurityHeaders]
    public class HomeController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;

        public HomeController(IIdentityServerInteractionService interaction)
        {
            _interaction = interaction;
        }

        public IActionResult Index()
        {
            var vm = new HomeIndexViewModel();
            if (User.Identity.IsAuthenticated)
            {
                var usersClaims = User.Claims.Select(claim => claim.Type).ToList();

                foreach (var client in IS4.is4aspid.Config.GetClients())
                {
                    if (string.IsNullOrEmpty(client.ClientUri))
                    {
                        continue;
                    }
                    var allowedScopes = client.AllowedScopes.ToList();
                    var intersect = usersClaims.Intersect(allowedScopes);
                    if (!intersect.Any())
                    {
                        continue;
                    }
                    vm.Clients.Add(new ClientViewModel
                    {
                        Name = client.ClientName,
                        Description = client.ClientName + " description",
                        Url = client.ClientUri,
                        ValidClaims = client.AllowedScopes
                    });
                }
            }
            else
            {
                vm.NotAuthenticatedMessage = "Please login to start";
            }

            return View(vm);
        }

        /// <summary>
        /// Shows the error page
        /// </summary>
        public async Task<IActionResult> Error(string errorId)
        {
            var vm = new ErrorViewModel();

            // retrieve error details from identityserver
            var message = await _interaction.GetErrorContextAsync(errorId);
            if (message != null)
            {
                vm.Error = message;
            }

            return View("Error", vm);
        }
    }
}