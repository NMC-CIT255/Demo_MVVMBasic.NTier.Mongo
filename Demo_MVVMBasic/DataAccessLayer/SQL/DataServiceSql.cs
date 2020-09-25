using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_MVVMBasic.DataAccessLayer
{
    class DataServiceSql : IDataService
    {
        private List<Widget> _widgets;

        public DataServiceSql()
        {
            DataSet widgets_ds = GetDataSet();
            _widgets = GetWidgets(widgets_ds);
        }

        /// <summary>
        /// query the data set to generate a list of Widget objects
        /// Note: all properties must be converted
        /// </summary>
        /// <param name="widgets_ds">Widget data set</param>
        /// <returns>list of widget objects</returns>
        private List<Widget> GetWidgets(DataSet widgets_ds)
        {
            DataTable widgets_dt = widgets_ds.Tables["Widgets"];

            List<Widget> widgets = (from w in widgets_dt.AsEnumerable()
                           select new Widget()
                           {
                               Id = Convert.ToInt32(w["Id"]),
                               Name = Convert.ToString(w["Name"]),
                               Color = Convert.ToString(w["Color"]),
                               UnitPrice = Convert.ToDouble(w["UnitPrice"]),
                               CurrentInventory = Convert.ToInt32(w["CurrentInventory"])
                           }).ToList();

            return widgets;
        }

        /// <summary>
        /// connect to the SQL database and read all tables into a data set
        /// </summary>
        /// <returns>data set of Widgets database</returns>
        private DataSet GetDataSet()
        {
            DataSet widgets_ds = new DataSet();

            string connString = SqlDataSettings.ConnectionString;
            string sqlCommandString = "SELECT * from Widgets";

            using (SqlConnection sqlConn = new SqlConnection(connString))
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand(sqlCommandString, sqlConn);
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlConn.Open();

                    sqlAdapter.Fill(widgets_ds, "Widgets");
                }
                catch (SqlException sqlEx)
                {
                    var exceptionMessage = sqlEx.Message;
                    throw;
                }
            }

            return widgets_ds;
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
        /// get a widget by id
        /// </summary>
        /// <param name="id">widget id</param>
        /// <returns>widget</returns>
        public Widget GetById(int id)
        {
            return _widgets.FirstOrDefault(w => w.Id == id);
        }

        /// <summary>
        /// add a new widget
        /// </summary>
        /// <param name="widget">widget to add</param>
        public void Add(Widget widget)
        {
            string connString = SqlDataSettings.ConnectionString;

            //
            // build out SQL Command
            //
            var sb = new StringBuilder("INSERT INTO Widgets");
            sb.Append(" ([Id], [Name], [Color], [UnitPrice], [CurrentInventory])");
            sb.Append(" Values (");
            sb.Append("'").Append(NextIdNumber()).Append("',");
            sb.Append("'").Append(widget.Name).Append("',");
            sb.Append("'").Append(widget.Color).Append("',");
            sb.Append("'").Append(widget.UnitPrice).Append("',");
            sb.Append("'").Append(widget.CurrentInventory).Append("')");
            string sqlCommandString = sb.ToString();

            using (SqlConnection sqlConn = new SqlConnection(connString))
            {
                try
                {
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter();
                    sqlConn.Open();
                    sqlAdapter.InsertCommand = new SqlCommand(sqlCommandString, sqlConn);
                    sqlAdapter.InsertCommand.ExecuteNonQuery();
                }
                catch (Exception msg)
                {
                    var exceptionMessage = msg.Message;
                    throw;
                }
            }
        }

        /// <summary>
        /// delete a widget
        /// </summary>
        /// <param name="id">widget to delete</param>
        public void Delete(int id)
        {
            string connString = SqlDataSettings.ConnectionString;

            //
            // build out SQL Command
            //
            var sb = new StringBuilder("DELETE FROM Widgets");
            sb.Append(" WHERE Id = ").Append(id);
            string sqlCommandString = sb.ToString();

            using (SqlConnection sqlConn = new SqlConnection(connString))
            {
                try
                {
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter();
                    sqlConn.Open();
                    sqlAdapter.DeleteCommand = new SqlCommand(sqlCommandString, sqlConn);
                    sqlAdapter.DeleteCommand.ExecuteNonQuery();
                }
                catch (Exception msg)
                {
                    var exceptionMessage = msg.Message;
                    throw;
                }
            }
        }



        /// <summary>
        /// update a widget
        /// </summary>
        /// <param name="widget">widget to update</param>
        public void Update(Widget widget)
        {
            string connString = SqlDataSettings.ConnectionString;

            //
            // build out SQL Command
            //
            var sb = new StringBuilder("UPDATE Widgets SET ");
            sb.Append("Name = '").Append(widget.Name).Append("', ");
            sb.Append("Color = '").Append(widget.Color).Append("', ");
            sb.Append("UnitPrice = '").Append(widget.UnitPrice).Append("', ");
            sb.Append("CurrentInventory = '").Append(widget.CurrentInventory).Append("' ");
            sb.Append("WHERE Id = ").Append(widget.Id);
            string sqlCommandString = sb.ToString();

            using (SqlConnection sqlConn = new SqlConnection(connString))
            {
                try
                {
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter();
                    sqlConn.Open();
                    sqlAdapter.UpdateCommand = new SqlCommand(sqlCommandString, sqlConn);
                    sqlAdapter.UpdateCommand.ExecuteNonQuery();
                }
                catch (Exception msg)
                {
                    var exceptionMessage = msg.Message;
                    throw;
                }
            }
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
