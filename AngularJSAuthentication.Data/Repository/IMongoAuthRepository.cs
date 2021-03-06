using System.Collections.Generic;
using System.Threading.Tasks;
using AngularJSAuthentication.Data.Entities;
using AngularJSAuthentication.Data.Models;
using Microsoft.AspNet.Identity;

namespace AngularJSAuthentication.Data.Repository
{
    public interface IMongoAuthRepository
    {
        Task<IdentityResult> RegisterUser(UserModel userModel);

        Task<IUser> FindUser(string userName, string password);

        Client FindClient(string clientId);



        Task<User> FindAsync(UserLoginInfo loginInfo);

        Task<IdentityResult> CreateAsync(User user);

        Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login);


        Task<RefreshToken> FindRefreshToken(string tokenId);

        List<RefreshToken> GetAllRefreshTokens();

        Task<bool> AddRefreshToken(RefreshToken token);

        Task<bool> RemoveRefreshToken(string tokenId);

        void Dispose();
    }
}