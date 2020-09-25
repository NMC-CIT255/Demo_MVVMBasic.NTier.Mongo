using Demo_MVVMBasic;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_MVVMBasic.DataAccessLayer
{
    public interface IDataService
    {
        IEnumerable<Widget> GetAll();
        Widget GetById(int id);
        void Add(Widget character);
        void Update(Widget character);
        void Delete(int id);
    }
}
