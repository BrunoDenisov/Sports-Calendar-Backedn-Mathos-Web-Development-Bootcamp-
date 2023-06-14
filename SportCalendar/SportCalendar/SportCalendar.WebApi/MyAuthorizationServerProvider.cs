using Microsoft.Owin.Security.OAuth;
using SportCalendar.Common;
using SportCalendar.Model;
using SportCalendar.Repository;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SportCalendar.WebApi
{

    public class MyAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            bool isValidUsername = CredentialsValidation.ValidateUsername(context.UserName);
            bool isValidPassword = CredentialsValidation.ValidatePassword(context.Password);

            if (isValidUsername && isValidPassword)
            {
                AuthUser user = AuthRepository.ValidateUser(context.UserName, context.Password);

                if (user == null)
                {
                    context.SetError("invalid_grant", "Provided username and password is incorrect");
                    return;
                }
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("Id", user.Id.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Name, user.Username));
                identity.AddClaim(new Claim(ClaimTypes.Role, user.Access));
                identity.AddClaim(new Claim("Email", user.Email));
                context.Validated(identity);
                return;
            }
            context.SetError("Invalid Username or Password");
            return;
            

        }
    }
}