
using Demo_MVVMBasic.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Demo_MVVMBasic.BusinessLayer
{
    /// <summary>
    /// Repository for CRUD
    /// Note: the _dataService object is instantiated with either the XML or Json class based on the value set in the DataConfig class
    /// </summary>
    class WidgetRepository : IWidgetRepository, IDisposable
    {
        private IDataService _dataService;
        List<Widget> _widgets;

        /// <summary>
        /// set the correct data service (XML or Json) data service and read in a list of all widgets
        /// </summary>
        public WidgetRepository()
        {
            DataServiceConfig dataService = new DataServiceConfig();
            _dataService = dataService.SetDataService();

            try
            {
                _widgets = _dataService.GetAll() as List<Widget>;
            }
            catch (Exception e)
            {
                string message = e.Message;
                throw;
            }
        }

        /// <summary>
        /// retrieve all widgets
        /// </summary>
        /// <returns>all widgets</returns>
        public IEnumerable<Widget> GetAll()
        {
            return _widgets;
        }

        /// <summary>
        /// retrieve a widget by the id
        /// </summary>
        /// <param name="name">widget name</param>
        /// <returns></returns>
        public Widget GetById(string name)
        {
            return _widgets.FirstOrDefault(w => w.Name == name);
        }

        /// <summary>
        /// add a new widget
        /// </summary>
        /// <param name="widget">widget</param>
        public void Add(Widget widget)
        {
            try
            {
                _dataService.Add(widget);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// delete a widget
        /// </summary>
        /// <param name="name">widget id</param>
        public void Delete(string name)
        {
            try
            {
                _dataService.Delete(name);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// update a widget
        /// </summary>
        /// <param name="widget">widget</param>
        public void Update(Widget widget)
        {
            try
            {
                _dataService.Update(widget);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// required if class will be use in a 'using" block
        /// </summary>
        public void Dispose()
        {
            _dataService = null;
            _widgets = null;
        }
    }
}
