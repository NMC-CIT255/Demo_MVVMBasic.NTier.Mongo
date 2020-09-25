using Demo_MVVMBasic.DataAccessLayer;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;


namespace Demo_MVVMBasic.BusinessLayer
{
    public class WidgetBusiness
    {
        public FileIoMessage FileIoStatus { get; set; }

        public WidgetBusiness()
        {
            //
            // TODO (Demo Mode) - load seed data to database
            //
            //MongoDbUtilities.WriteSeedDataToDatabase();
            SqlUtilities.WriteSeedDataToDatabase();
        }

        /// <summary>
        /// retrieve a widget using the repository
        /// </summary>
        /// <returns>widget</returns>
        private Widget GetWidget(int id)
        {
            Widget widget = null;
            FileIoStatus = FileIoMessage.None;

            try
            {
                using (WidgetRepository wRepository = new WidgetRepository())
                {
                    widget = wRepository.GetById(id);
                };

                if (widget != null)
                {
                    FileIoStatus = FileIoMessage.Complete;
                }
                else
                {
                    FileIoStatus = FileIoMessage.RecordNotFound;
                }
            }
            catch (Exception)
            {
                FileIoStatus = FileIoMessage.FileAccessError;
            }

            return widget;
        }

        /// <summary>
        /// retrieve a list of all widgets using the repository
        /// </summary>
        /// <returns>all widgets</returns>
        private List<Widget> GetAllWidgets()
        {
            List<Widget> widgets = null;
            FileIoStatus = FileIoMessage.None;

            try
            {
                using (WidgetRepository wRepository = new WidgetRepository())
                {
                    widgets = wRepository.GetAll() as List<Widget>;
                };

                if (widgets != null)
                {
                    FileIoStatus = FileIoMessage.Complete;
                }
                else
                {
                    FileIoStatus = FileIoMessage.NoRecordsFound;
                }
            }
            catch (Exception)
            {
                FileIoStatus = FileIoMessage.FileAccessError;
            }

            return widgets;
        }

        /// <summary>
        /// provide a list of all widgets
        /// </summary>
        /// <returns>list of all widgets</returns>
        public List<Widget> AllWidgets()
        {
            //
            // TODO (Demo Mode) - switch between seed data and persistence
            // Note: disable the business layer and run the method below
            //
            // return SeedData.GetAllWidgets();
            //

            return GetAllWidgets() as List<Widget>;
        }

        /// <summary>
        /// retrieve a widget by id 
        /// </summary>
        /// <param name="id">widget id</param>
        /// <returns>widget</returns>
        public Widget WidgetById(int id)
        {
            return GetWidget(id);
        }

        /// <summary>
        /// add a new widget
        /// </summary>
        /// <param name="widget">widget to add</param>
        public void AddWidget(Widget widget)
        {
            try
            {
                if (widget != null)
                {
                    using (WidgetRepository wRepository = new WidgetRepository())
                    {
                        wRepository.Add(widget);
                    };

                    FileIoStatus = FileIoMessage.Complete;
                }
            }
            catch (Exception)
            {
                FileIoStatus = FileIoMessage.FileAccessError;
            }
        }

        /// <summary>
        /// update a widget
        /// </summary>
        /// <param name="updatedWidget">updated widget</param>
        public void UpdateWidget(Widget updatedWidget)
        {
            try
            {
                if (GetWidget(updatedWidget.Id) != null)
                {
                    using (WidgetRepository repo = new WidgetRepository())
                    {
                        repo.Update(updatedWidget);
                    }

                    FileIoStatus = FileIoMessage.Complete;
                }
                else
                {
                    FileIoStatus = FileIoMessage.RecordNotFound;
                }
            }
            catch (Exception)
            {
                FileIoStatus = FileIoMessage.FileAccessError;
            }
        }
        
        /// <summary>
        /// retrieve a widget by id 
        /// </summary>
        /// <param name="id">widget id</param>
        public void DeleteWidget(int id)
        {
            try
            {
                if (GetWidget(id) != null)
                {
                    using (WidgetRepository wRepository = new WidgetRepository())
                    {
                        wRepository.Delete(id);
                    }

                    FileIoStatus = FileIoMessage.Complete;
                }
                else
                {
                    FileIoStatus = FileIoMessage.RecordNotFound;
                }
            }
            catch (Exception)
            {
                FileIoStatus = FileIoMessage.FileAccessError;
            }
        }



    }
}
