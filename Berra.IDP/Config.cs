// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace Berra.IDP
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource("usertype", new [] {"usertype"})
            };


        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                new ApiResource("sokobanapi", 
                    "Sokoban API",
                    new [] {"usertype" }),
                new ApiResource("api1", "My API #1")
            };


        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                //Blazor sokoban app
                new Client
                {
                    ClientId = "sokobanapp",
                    ClientName = "Sokoban App",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RequirePkce = true,
                    RedirectUris = { "https://localhost:44307/authentication/login-callback" },
                    PostLogoutRedirectUris = { "https://localhost:44307/authentication/logout-callback" },
                    AllowedScopes = { "openid", "profile", "email", "sokobanapi", "usertype" },
                    AllowedCorsOrigins = { "https://localhost:44307" },
                    RequireConsent = false
                }
                
            };
    }
}