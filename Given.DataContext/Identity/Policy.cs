using Microsoft.AspNetCore.Authorization;

namespace Given.DataContext.Identity
{
    public static class Policies
    {
        public const string SuperAdmin = "SuperAdmin";
        public const string Administrator = "Administrator";
        public const string User = "User";

        public static AuthorizationPolicy SuperAdminPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(SuperAdmin).Build();
        }
        public static AuthorizationPolicy AdminPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Administrator).Build();
        }
        public static AuthorizationPolicy UserPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(User).Build();
        }
    }
}
