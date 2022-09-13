using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface ITokenHandler
    {
        Task<String> CreateTokenAsync(User user);
    }
}
