using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_MVVMBasic.DataAccessLayer
{
    public class DataServiceConfig
    {
        //
        // set the type of persistence
        //
        private DataType _dataType = DataType.MONGODB;

        /// <summary>
        /// instantiate and return the correct data service
        /// </summary>
        /// <returns>data service object</returns>
        public IDataService SetDataService()
        {
            switch (_dataType)
            {
                case DataType.MONGODB:
                    return new DataServiceMongoDb();

                default:
                    throw new Exception();
            }
        }
    }
}
