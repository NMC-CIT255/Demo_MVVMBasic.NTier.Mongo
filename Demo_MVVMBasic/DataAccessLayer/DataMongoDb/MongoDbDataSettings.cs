using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_MVVMBasic.DataAccessLayer.DataMongoDb
{
    public static class MongoDbDataSettings
    {
        private static string userName = "johnvelis";
        private static string password = "biketoday";

        public static string connectionString = $"mongodb+srv://{userName}:{password}@cluster0.hasci.mongodb.net/<{databaseName}>?retryWrites=true&w=majority";

        public static string collectionName = "widgets";       
        public static string databaseName = "cit255";
    }
}
