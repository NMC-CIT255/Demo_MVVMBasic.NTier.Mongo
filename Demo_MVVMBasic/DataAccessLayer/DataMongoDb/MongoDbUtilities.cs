﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Demo_MVVMBasic.DataAccessLayer.DataMongoDb;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Demo_MVVMBasic.DataAccessLayer
{
    public static class MongoDbUtilities
    {
        private static MongoClient _client;
        
        /// <summary>
        /// test for connection to online MongoDb 
        /// </summary>
        /// <returns>true if connected</returns>
        public static bool Connection()
        {
            try
            {
                _client = new MongoClient(MongoDbDataSettings.connectionString);

                return true;
            }
            catch (Exception)
            {
                return false;                
            }
        }

        /// <summary>
        /// write seed data to online MongoDb database/collection
        /// </summary>
        /// <returns>true of seed data added successfully</returns>
        public static bool WriteSeedDataToDatabase()
        {
            if (Connection())
            {
                //
                // get collection
                //
                IMongoDatabase database = _client.GetDatabase(MongoDbDataSettings.databaseName);
                IMongoCollection<Widget> collection = database.GetCollection<Widget>(MongoDbDataSettings.collectionName);

                //
                // delete all existing documents in collection
                //
                var filter = Builders<Widget>.Filter.Empty;
                collection.DeleteMany(filter);

                //
                // write all seed data to collection
                //
                collection.InsertMany(SeedData.GetAllWidgets());
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
