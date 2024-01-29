using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace TP3AURASOFT.Controladores
{
    class Conexion
    {
        public static string cadena = "Data Source=tp3AURASOFT.db;";
        private static SQLiteConnection conexion = new SQLiteConnection(cadena);


        public static void OpenConexion()
        {
            if (conexion.State == System.Data.ConnectionState.Closed)
                conexion.Open();
            //Activamos la gestión de llaves foráneas
            using (SQLiteCommand command = new SQLiteCommand("PRAGMA foreign_keys = ON;", conexion))
            {
                command.ExecuteNonQuery();
            }
        }

        public static void CloseConexion()
        {
            conexion.Close();
        }
        public static SQLiteConnection Connection
        {
            set { conexion = value; }
            get { return conexion; }
        }
    }
}
