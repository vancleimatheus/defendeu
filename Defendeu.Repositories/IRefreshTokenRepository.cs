using Defendeu.Entities.Authentication;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Builders;
using MongoDB.Bson;

namespace Defendeu.Repositories
{
    public interface IRefreshTokenRepository : IBaseRepository<RefreshToken>
    {               
        RefreshToken GetByToken(string token);

        void RemoveByToken(string hashedToken);
    }
    public class RefreshTokenRepository : BaseRepository<RefreshToken>, IRefreshTokenRepository
    {
        public override RefreshToken Add(RefreshToken token)
        {
            RefreshToken existingToken = null;

            if (Collection.AsQueryable<RefreshToken>()
                .Any(r => r.Subject == token.Subject && r.ClientId == token.ClientId))
                existingToken = Collection.AsQueryable<RefreshToken>()
                   .First(r => r.Subject == token.Subject && r.ClientId == token.ClientId);

            if (existingToken != null)
            {
                var result = Remove(existingToken);
            }

            Collection.Insert(token);
            return token;
        }                             

        public RefreshToken GetByToken(string token)
        {
            var query = Query<RefreshToken>.EQ(r => r.Token, token);
            return Collection.FindOne(query);
        }

        public void RemoveByToken(string hashedToken)
        {
            var query = Query<RefreshToken>.EQ(r => r.Token, hashedToken);
            Collection.Remove(query);
        }

        
    }
}
