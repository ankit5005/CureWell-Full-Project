using Microsoft.Owin.Security.OAuth;
using SOTI.CureWell.DAL;
using SOTI.CureWell.DAL.Interfaces;
using SOTI.CureWell.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace SOTI.CureWell.WebAPI.Providers
{
    public class AuthProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            IUser userDetails = new CureWellRepository();
            var user = await userDetails.ValidateUserAsync(context.UserName, context.Password);
            if (user != null)
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);//Bearer Token
                identity.AddClaim(new Claim(ClaimTypes.Role, user.Role));
                identity.AddClaim(new Claim(ClaimTypes.Email, user.EmailId));
                identity.AddClaim(new Claim("Password",user.Password ));
                context.Validated(identity);
            }
            else
            {
                context.SetError("Imvalid Details", "Either Username or Password is incorrect");
                return;
            }
        }
    }
}