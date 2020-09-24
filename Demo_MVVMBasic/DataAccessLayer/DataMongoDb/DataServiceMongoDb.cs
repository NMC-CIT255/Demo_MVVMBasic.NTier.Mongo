using Demo_MVVMBasic.DataAccessLayer.DataMongoDb;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_MVVMBasic.DataAccessLayer
{
    public class DataServiceMongoDb : IDataService
    {
        private List<Widget> _widgets;
        private IMongoCollection<Widget> _collection;

        public DataServiceMongoDb()
        {
            Connection();
        }

        private bool Connection()
        {
            try
            {
                MongoClient dbClient = new MongoClient(MongoDbDataSettings.connectionString);
                IMongoDatabase database = dbClient.GetDatabase(MongoDbDataSettings.databaseName);
                _collection = database.GetCollection<Widget>(MongoDbDataSettings.collectionName);

                _widgets = _collection.Find(Builders<Widget>.Filter.Empty).ToList();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Widget> GetAll()
        {
            return _widgets;
        }

        public void Add(Widget widget)
        {
            _collection.InsertOne(widget);
        }


        public void Delete(string name)
        {
            Widget widgitToDelete = _widgets.FirstOrDefault(w => w.Name == name);

            if (widgitToDelete != null)
            {
                var deleteFilter = Builders<Widget>.Filter.Eq("Name", name);
                _collection.DeleteOne(deleteFilter);
            }
        }

        public Widget GetById(string name)
        {
            var getFilter = Builders<Widget>.Filter.Eq("Name", name);
            return _collection.Find(getFilter).FirstOrDefault();
        }

        public void Update(Widget widget)
        {
            var updateFilter = Builders<Widget>.Filter.Eq("Name", widget.Name);
            var deleteResult = _collection.DeleteOne(updateFilter);
            _collection.InsertOne(widget);
        }
    }
}
