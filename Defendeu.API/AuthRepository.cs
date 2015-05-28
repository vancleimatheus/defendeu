using Defendeu.Api.Models;
using AspNet.Identity.MongoDB;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Configuration;

namespace Defendeu.Api
{

    public class AuthRepository : IDisposable
    {
        private IdentityContext _ctx;

        private UserManager<IdentityUser> _userManager;

        public AuthRepository()
        {
            var connectionString = ConfigurationManager.AppSettings["connectionstring"];
            var databaseName = ConfigurationManager.AppSettings["databasename"];

            var client = new MongoClient(connectionString);
            var database = client.GetServer().GetDatabase(databaseName);
            var users = database.GetCollection<IdentityUser>("users");

            _ctx = new IdentityContext(users);
            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_ctx));           
        }

        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = userModel.UserName
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);

            return result;
        }

        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            IdentityUser user = await _userManager.FindAsync(userName, password);

            return user;
        }        

        //public async Task<bool> AddRefreshToken(RefreshToken token)
        //{

        //   var existingToken = _ctx.RefreshTokens.Where(r => r.Subject == token.Subject && r.ClientId == token.ClientId).SingleOrDefault();

        //   if (existingToken != null)
        //   {
        //     var result = await RemoveRefreshToken(existingToken);
        //   }
          
        //    _ctx.RefreshTokens.Add(token);

        //    return await _ctx.SaveChangesAsync() > 0;
        //}

        //public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        //{
        //   var refreshToken = await _ctx.RefreshTokens.FindAsync(refreshTokenId);

        //   if (refreshToken != null) {
        //       _ctx.RefreshTokens.Remove(refreshToken);
        //       return await _ctx.SaveChangesAsync() > 0;
        //   }

        //   return false;
        //}

        //public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
        //{
        //    _ctx.RefreshTokens.Remove(refreshToken);
        //     return await _ctx.SaveChangesAsync() > 0;
        //}

        //public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        //{
        //    var refreshToken = await _ctx.RefreshTokens.FindAsync(refreshTokenId);

        //    return refreshToken;
        //}

        //public List<RefreshToken> GetAllRefreshTokens()
        //{
        //     return  _ctx.RefreshTokens.ToList();
        //}

        public async Task<IdentityUser> FindAsync(UserLoginInfo loginInfo)
        {
            IdentityUser user = await _userManager.FindAsync(loginInfo);

            return user;
        }

        public async Task<IdentityResult> CreateAsync(IdentityUser user)
        {
            var result = await _userManager.CreateAsync(user);

            return result;
        }

        public async Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login)
        {
            var result = await _userManager.AddLoginAsync(userId, login);

            return result;
        }

        public void Dispose()
        {
            _ctx = null;
            _userManager.Dispose();

        }
    }
}