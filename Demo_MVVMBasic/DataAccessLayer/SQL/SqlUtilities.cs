using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Demo_MVVMBasic.DataAccessLayer
{
    public class SqlUtilities
    {
        public static bool WriteSeedDataToDatabase()
        {
            bool operationSuccessful = true;

            try
            {
                DeleteAllWidgetRecords();
                AddAllWidgetRecords();
            }
            catch (Exception)
            {
                operationSuccessful = false;
                throw;
            }

            return operationSuccessful;
        }

        private static bool DeleteAllWidgetRecords()
        {
            string connString = SqlDataSettings.ConnectionString;
            string sqlCommandString = "DELETE FROM Widgets";
            bool operationSuccessful = true;

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
                    operationSuccessful = false;
                    throw;
                }
            }
            return operationSuccessful;
        }

        private static bool AddAllWidgetRecords()
        {
            string connString = SqlDataSettings.ConnectionString;
            bool operationSuccessful = true;

            foreach (var widget in SeedData.GetAllWidgets())
            {
                //
                // build out SQL Command
                //
                var sb = new StringBuilder("INSERT INTO Widgets");
                sb.Append(" ([Id], [Name], [Color], [UnitPrice], [CurrentInventory])");
                sb.Append(" Values (");
                sb.Append("'").Append(widget.Id).Append("',");
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
                        operationSuccessful = false;
                        throw;
                    }
                }
            }
            return operationSuccessful;
        }
    }
}
