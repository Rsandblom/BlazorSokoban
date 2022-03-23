using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sokoban.API.Policies
{
    public static class Policies
    {
        public const string CanCreateLevels = "CanCreateLevels";
        public const string CanPlayLevels = "CanPlayLevels";

        public static AuthorizationPolicy CanCreateLevelsPolicy()
        {
            return new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .RequireClaim("usertype", "ADMIN")
                .Build();
        }

        public static AuthorizationPolicy CanPlayLevelsPolicy()
        {
            return new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .RequireClaim("usertype", "PLAYER", "ADMIN")
                .Build();
        }
    }
}
