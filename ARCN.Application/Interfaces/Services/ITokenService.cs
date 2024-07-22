
using System.Threading.Tasks;

namespace ARCN.Application.Interfaces.Services
{
    public interface ITokenService
    {
        ValueTask<string> CreateTokenAsync(ApplicationUser user);
        string GenerateRefreshToken();
    }
}
