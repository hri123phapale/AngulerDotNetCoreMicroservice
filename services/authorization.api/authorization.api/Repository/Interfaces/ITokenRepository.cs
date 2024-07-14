
 
using Microsoft.AspNetCore.Identity;

namespace CodePulse.Api.Repository.Interfaces
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}
