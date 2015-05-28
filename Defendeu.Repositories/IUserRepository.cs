using AspNet.Identity.MongoDB;
using Defendeu.Entities;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Defendeu.Repositories
{
    public interface IUserRepository
    {
        IdentityUser GetByUsername(string username);
    }

    public class UserRepository : IUserRepository
    {
        private MongoDatabase _database;
        private MongoCollection<IdentityUser> _collection;

        protected MongoCollection<IdentityUser> Collection
        {
            get
            {
                return _collection;
            }
        }

        protected string CollectionName
        {
            get { return "users"; }
        }

        public UserRepository()
        {
            var connectionString = ConfigurationManager.AppSettings["connectionstring"];
            var databaseName = ConfigurationManager.AppSettings["databasename"];

            var client = new MongoClient(connectionString);
            _database = client.GetServer().GetDatabase(databaseName);
            _collection = _database.GetCollection<IdentityUser>(CollectionName);
        }

        public IdentityUser GetByUsername(string username)
        {
            var query = Query<IdentityUser>.EQ(c => c.UserName, username);
            return Collection.FindOne(query);
        }
    }
}
