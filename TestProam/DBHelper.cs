using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.Common;
using System.Collections.ObjectModel;
using System.Collections;

namespace TestProam
{
    public class DBHelper
    {
        private string connectionString;

        public DBHelper(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public DataSet ExecuteSelectCommand(string query)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                return dataSet;
            }
        }

        public int ExecuteNonQuery(string query)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                int affectedRows = command.ExecuteNonQuery();
                return affectedRows;
            }
        }

        public int ExecuteScalar(string query)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                int result = (int)command.ExecuteScalar();
                return result;
            }
        }

        public int GetRowCount(string query)
        {
            int rowCount = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rowCount++;
                    }
                }
            }

            return rowCount;
        }

        public void Dispose()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Dispose();
            }
            
        }

    }
}