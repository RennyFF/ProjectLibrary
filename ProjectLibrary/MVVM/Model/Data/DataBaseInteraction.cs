using Npgsql;
using ProjectLibrary.Utils.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.MVVM.Model.Data
{
    public static class DataBaseInteraction
    {
        public static List<User> GetUsers(string PSQL_Text)
        {
            NpgsqlConnection cn_connection = DataBaseConfig.GetDBConnection();
            List<User> Users = new List<User>();
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(PSQL_Text, cn_connection);
            //adapter.Fill(Users);
            return Users;
        }
    }
}
