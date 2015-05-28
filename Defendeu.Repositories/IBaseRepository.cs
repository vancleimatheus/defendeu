using Defendeu.Attributes.Entities;
using Defendeu.Entities;
using MongoDB.Bson;
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
    public interface IBaseRepository<T> : IDisposable
    {
        T Add(T entity);

        void Update(T entity);

        T AddOrUpdate(T entity);

        bool Remove(String id);

        bool Remove(T entity);

        T Get(String id);

        T Get(ObjectId id);

        IList<T> List();
    }

    public abstract class BaseRepository<T> : IBaseRepository<T> where T : Entity
    {
        private MongoDatabase _database;
        private MongoCollection<T> _collection;

        protected String CollectionName
        {
            get
            {
                var collectionNameAttributes = typeof(T).GetCustomAttributes(typeof(CollectionNameAttribute), false) as CollectionNameAttribute[];
                if (collectionNameAttributes.Count() > 0)
                    return collectionNameAttributes[0].CollectionName;

                throw new InvalidOperationException("The entity type must have a collection name attribute");
            }
        }

        protected MongoCollection<T> Collection
        {
            get
            {
                return _collection;
            }
        }

        public BaseRepository()
        {
            var connectionString = ConfigurationManager.AppSettings["connectionstring"];
            var databaseName = ConfigurationManager.AppSettings["databasename"];

            var client = new MongoClient(connectionString);
            _database = client.GetServer().GetDatabase(databaseName);
            _collection = _database.GetCollection<T>(CollectionName);
        }

        public void Dispose()
        {
            _collection = null;
            _database = null;
        }

        public virtual T Add(T entity)
        {
            entity.Id = new ObjectId();
            Collection.Insert(entity);
            return entity;
        }

        public virtual void Update(T entity)
        {
            Collection.Save<T>(entity);
        }

        public virtual T AddOrUpdate(T entity)
        {
            if (entity.Id != new ObjectId())
            {
                Update(entity);
                return entity;
            }
            else
            {
                return Add(entity);
            }
        }

        public virtual bool Remove(string id)
        {
            ObjectId objectId = new ObjectId(id);
            var query = Query<T>.EQ(r => r.Id, objectId);
            Collection.Remove(query);
            return true;
        }

        public virtual T Get(string id)
        {
            ObjectId objectId = new ObjectId(id);
            return Get(objectId);
        }

        public virtual T Get(ObjectId id)
        {
            var query = Query<T>.EQ(c => c.Id, id);
            return Collection.FindOne(query);
        }

        public bool Remove(T entity)
        {
            var query = Query<T>.EQ(r => r.Id, entity.Id);
            Collection.Remove(query);
            return true;
        }

        public IList<T> List()
        {
            return Collection.FindAll().ToList();
        }
    }
}
