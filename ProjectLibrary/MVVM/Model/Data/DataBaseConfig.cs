using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.MVVM.Model.Data
{
    public static class DataBaseConfig
    {
        public static NpgsqlConnection GetDBConnection()
        {
            NpgsqlConnection Connection = new NpgsqlConnection(Utils.Constants.ConnectionStringDB);
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }

            return Connection;
        }

        public static void ExecuteDBQuery(string Query)
        {
            NpgsqlCommand Command = new NpgsqlCommand(Query, GetDBConnection());
            Command.ExecuteNonQuery();
        }

        public static void CloseDBConnection()
        {
            NpgsqlConnection Connection = new NpgsqlConnection(Utils.Constants.ConnectionStringDB);
            if (Connection.State != ConnectionState.Closed)
            {
                Connection.Close();
            }
        }
    }
}
