using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AngularJSAuthentication.Data.Entities;
using AngularJSAuthentication.Data.Interface;
using AngularJSAuthentication.Data.Models;
using Microsoft.AspNet.Identity;

namespace AngularJSAuthentication.Data.Repository
{
    public class MongoAuthRepository : IMongoAuthRepository
    {
        private readonly IClientRepository clientRepository;

        private readonly IRefreshTokenRepository refreshTokenRepository;

        private readonly IUserRepository<User> userRepository;

        private readonly IUserLoginStore<User> userLoginStore;  

        public bool _disposed;

        public MongoAuthRepository(IClientRepository clientRepository, IRefreshTokenRepository refreshTokenRepository, IUserRepository<User> userRepository, IUserLoginStore<User> userLoginStore)
        {
            this.clientRepository = clientRepository;
            this.refreshTokenRepository = refreshTokenRepository;
            this.userRepository = userRepository;
            this.userLoginStore = userLoginStore;

        }

        public Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public Task<IUser> FindUser(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public Client FindClient(string clientId)
        {
            ThrowIfDisposed();
            if (clientId == null)
                throw new ArgumentNullException("clientId");

            var client = clientRepository.FindClient(clientId).Result;
            return client;
        }






        public Task<User> FindAsync(UserLoginInfo loginInfo)
        {
            ThrowIfDisposed();
            if (loginInfo == null)
                throw new ArgumentNullException("loginInfo");

            var user = userLoginStore.FindAsync(loginInfo);
            return user;
        }


        public Task<IdentityResult> CreateAsync(User user)
        {
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException("user");

            try
            {
                var result = userLoginStore.CreateAsync(user);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }

            return Task.FromResult(IdentityResult.Success);
        }


        public Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login)
        {
            try
            {
                var user = userLoginStore.FindByIdAsync(userId).Result;
                var result = userLoginStore.AddLoginAsync(user, login);
            }
            catch (Exception ex)
            {
                
                throw;
            }

            return Task.FromResult(IdentityResult.Success);
        }







        public Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddRefreshToken(RefreshToken token)
        {
            ThrowIfDisposed();
            if (token == null)
                throw new ArgumentNullException("token");

            var refreshToken = refreshTokenRepository.AddRefreshToken(token);
            return refreshToken;
        }

        public List<RefreshToken> GetAllRefreshTokens()
        {
            return refreshTokenRepository.GetAllRefreshTokens();
        }

        public Task<bool> RemoveRefreshToken(string tokenId)
        {
            ThrowIfDisposed();
            if (tokenId == null)
                throw new ArgumentNullException("tokenId");

            var result = refreshTokenRepository.RemoveRefreshToken(tokenId);
            return result;
        }


        private void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(GetType().Name);
        }

        public void Dispose()
        {
            _disposed = true;
        }

    }
}
