using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories.Base
{
    public interface ITokenHandler
    {
        Task<string> CreateTokenAsync(User user);
    }
}
