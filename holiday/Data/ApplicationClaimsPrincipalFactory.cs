using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Security.Claims;

namespace holiday.Data
{
	public class AppClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
	{
        public AppClaimsPrincipalFactory(
        UserManager<ApplicationUser> userManager
        , RoleManager<IdentityRole> roleManager
        , IOptions<IdentityOptions> optionsAccessor)
    : base(userManager, roleManager, optionsAccessor)
        {}

        public async override Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            var principal = await base.CreateAsync(user);
            if (!string.IsNullOrWhiteSpace(user.firstName))
            {
                ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
            new Claim(ClaimTypes.GivenName, user.firstName)
          });
            }

            if (!string.IsNullOrWhiteSpace(user.lastName))
            {
                ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
            new Claim(ClaimTypes.Surname, user.lastName),
          });
            }
            return principal;
        }
    }
}

