using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories.Base;

namespace NZWalks.API.Repositories.Implements
{
    public class UserRepository : IUserRepository
    {
        private readonly NZWalksDbContext context;

        public UserRepository(NZWalksDbContext context)
        {
            this.context = context;
        }
        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = await context.Users.FirstOrDefaultAsync
                (x => x.UserName.ToLower() == username.ToLower() && x.Password == password);

            if (user == null)
            {
                return null;
            }

            var userRoles = await context.Users_Roles.Where(x => x.UserId == user.Id).ToListAsync();
            
            if (userRoles.Any())
            {
                user.Roles = new List<string>();
                foreach (var userRole in userRoles)
                {
                    var role = await context.Roles.FirstOrDefaultAsync(x => x.Id == userRole.RoleId);
                    if (role != null)
                    {
                        user.Roles.Add(role.Name);
                    }
                }
            }
            user.Password = null;
            return user;
        }
    }
}
