using Defendeu.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Defendeu.Repositories
{
    public interface IClientRepository : IBaseRepository<Client>
    {
        Client GetByClientId(string clientId);
    }

    public class ClientRepository: BaseRepository<Client>, IClientRepository
    {
        public Client GetByClientId(string clientId)
        {
            var query = Query<Client>.EQ(c => c.ClientId, clientId);
            return Collection.FindOne(query);
        }        
    }
}
