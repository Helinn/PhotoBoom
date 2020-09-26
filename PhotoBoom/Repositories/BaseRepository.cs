using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using PhotoBoom.Settings;

namespace PhotoBoom.Repositories{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly GalleryDatabaseSettings databaseSettings;
        protected IMongoCollection<T> _dbCollection;

        protected BaseRepository(IOptions<GalleryDatabaseSettings> options)
        {
            this.databaseSettings = options.Value;
            var client = new MongoClient(this.databaseSettings.ConnectionString);
            var db = client.GetDatabase(this.databaseSettings.DatabaseName);
            _dbCollection = db.GetCollection<T>(typeof(T).Name);
        }

        public IMongoCollection<T> GetCollection(string name)
        {
            return  _dbCollection;
        }

        public bool Add(T obj)
        {
            if(obj == null)
            {
                throw new ArgumentNullException(typeof(T).Name + " object is null");
            }
            var res = _dbCollection.InsertOneAsync(obj);
            return res.IsCompletedSuccessfully;
        }

        public void Delete(string id)
        {
            //var objectId = new ObjectId(id);
            _dbCollection.DeleteOneAsync(Builders<T>.Filter.Eq("_id", id));

        }
        public virtual void Update(T obj)
        {
            _dbCollection.ReplaceOneAsync(Builders<T>.Filter.Eq("_id", obj.ToBson()), obj);
        }

        public T Get(string id)
        {
            //var objectId = new ObjectId(id);

            FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", id);

            return  _dbCollection.Find(filter).FirstOrDefault();

        }


        public ICollection<T> Get()
        {
            var all =  _dbCollection.Find(Builders<T>.Filter.Empty);
            return  all.ToList();
        }

        public async Task<ICollection<T>> GetByFilter(FilterDefinition<T> filter)
        {
            var lst = await _dbCollection.FindAsync(filter);
            return lst.ToList();
        }
    } 
}