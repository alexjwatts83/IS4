// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace IS4.is4aspid
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource
                {
                    Name = "website",
                    UserClaims = {"website"},
                    DisplayName = "users website"
                },
                new IdentityResource
                {
                    Name = "location",
                    UserClaims = {"location"},
                    DisplayName = "users location"
                },
                new IdentityResource
                {
                    Name = "admin_access",
                    UserClaims = {"admin_access"},
                    DisplayName = "users admin_access"
                },
                new IdentityResource
                {
                    Name = "about_access",
                    UserClaims = {"about_access"},
                    DisplayName = "users about_access"
                },
                new IdentityResource
                {
                    Name = "cancel_access",
                    UserClaims = {"cancel_access"},
                    DisplayName = "users cancel_access"
                },
                new IdentityResource
                {
                    Name = "general_access",
                    UserClaims = {"general_access"},
                    DisplayName = "users general_access"
                },
                new IdentityResource
                {
                    Name = "general_access_loc",
                    UserClaims = {"general_access_loc"},
                    DisplayName = "users general_access_loc"
                },
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[]
            {
                new ApiResource("api1", "My API #1")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                // client credentials flow client
                new Client
                {
                    ClientId = "client",
                    ClientName = "Client Credentials Client",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = {new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256())},

                    AllowedScopes = {"api1"}
                },

                // MVC client using hybrid flow
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",

                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                    ClientSecrets = {new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256())},

                    RedirectUris = {"http://localhost:5001/signin-oidc"},
                    FrontChannelLogoutUri = "http://localhost:5001/signout-oidc",
                    PostLogoutRedirectUris = {"http://localhost:5001/signout-callback-oidc"},

                    AllowOfflineAccess = true,
                    AllowedScopes =
                    {
                        "openid", "profile", "api1", "website", "email", "location", "admin_access", "about_access",
                        "cancel_access"
                    },
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RequireConsent = false
                },

                // SPA client using implicit flow
                new Client
                {
                    ClientId = "spa",
                    ClientName = "SPA Client",
                    ClientUri = "http://identityserver.io",

                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris =
                    {
                        "http://localhost:5002/index.html",
                        "http://localhost:5002/callback.html",
                        "http://localhost:5002/silent.html",
                        "http://localhost:5002/popup.html",
                    },

                    PostLogoutRedirectUris = {"http://localhost:5002/index.html"},
                    AllowedCorsOrigins = {"http://localhost:5002"},

                    AllowedScopes = {"openid", "profile", "api1"}
                },

                // MVC client using hybrid flow
                new Client
                {
                    ClientId = "fleet",
                    ClientName = "Fleet Monitoring",

                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                    ClientSecrets = {new Secret("645A794F-5FE2-4A52-A0EC-DDB8DADE121F".Sha256())},

                    RedirectUris = {"http://localhost:54250/signin-oidc"},
                    FrontChannelLogoutUri = "http://localhost:54250/signout-oidc",
                    PostLogoutRedirectUris = {"http://localhost:54250/signout-callback-oidc"},

                    AllowedScopes = {"openid", "profile", "admin_access", "general_access"},
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RequireConsent = false,
                    ClientUri = "http://localhost:54250/"
                },

                new Client
                {
                    ClientId = "location",
                    ClientName = "Location Monitoring",

                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                    ClientSecrets = {new Secret("472E9365-60CA-42AD-AA5F-FD1B69099E2D".Sha256())},

                    RedirectUris = {"http://localhost:53821/signin-oidc"},
                    FrontChannelLogoutUri = "http://localhost:53821/signout-oidc",
                    PostLogoutRedirectUris = {"http://localhost:53821/signout-callback-oidc"},

                    AllowedScopes = {"openid", "profile", "admin_access", "general_access_loc"},
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RequireConsent = false,
                    ClientUri = "http://localhost:53821/"
                },
            };
        }
    }
}