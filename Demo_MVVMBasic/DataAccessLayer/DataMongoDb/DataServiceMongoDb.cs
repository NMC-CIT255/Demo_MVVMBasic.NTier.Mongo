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

        /// <summary>
        /// connect to online MongoDb database
        /// </summary>
        /// <returns>true if connected</returns>
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

        /// <summary>
        /// get all widgets
        /// </summary>
        /// <returns>IEnumerable of widgets</returns>
        public IEnumerable<Widget> GetAll()
        {
            return _widgets;
        }

        /// <summary>
        /// add a new widget
        /// </summary>
        /// <param name="widget">widget to add</param>
        public void Add(Widget widget)
        {
            widget.Id = NextIdNumber();
            _collection.InsertOne(widget);
        }

        /// <summary>
        /// delete a widget
        /// </summary>
        /// <param name="id">widget to delete</param>
        public void Delete(int id)
        {
            Widget widgitToDelete = _widgets.FirstOrDefault(w => w.Id == id);

            if (widgitToDelete != null)
            {
                var deleteFilter = Builders<Widget>.Filter.Eq("Id", id);
                _collection.DeleteOne(deleteFilter);
            }
        }

        /// <summary>
        /// get a widget by id
        /// </summary>
        /// <param name="id">widget id</param>
        /// <returns>widget</returns>
        public Widget GetById(int id)
        {
            var getFilter = Builders<Widget>.Filter.Eq("Id", id);
            return _collection.Find(getFilter).FirstOrDefault();
        }

        /// <summary>
        /// update a widget
        /// </summary>
        /// <param name="widget">widget to update</param>
        public void Update(Widget widget)
        {
            var updateFilter = Builders<Widget>.Filter.Eq("Id", widget.Id);
            var deleteResult = _collection.DeleteOne(updateFilter);
            _collection.InsertOne(widget);
        }

        /// <summary>
        /// get the next highest id number from the list of widgets
        /// </summary>
        /// <returns>next id number</returns>
        private int NextIdNumber()
        {
            return _widgets.Max(w => w.Id) + 1;
        }
    }
}
