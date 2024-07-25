
using System.Threading.Tasks;
using ARCN.Application.DataModels.Identity;

namespace ARCN.Application.Interfaces.Services
{
    public interface ITokenService
    {
        ValueTask<string> CreateTokenAsync(ApplicationUser user);
        ValueTask<TokenResponse> CreateTokenAsync(TokenRequest request);
        ValueTask<TokenResponse> CreateRefreshTokenAsync(RefreshTokenRequest request);

    }
}
