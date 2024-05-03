using Microsoft.AspNetCore.Identity;

namespace PaKWalks.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
