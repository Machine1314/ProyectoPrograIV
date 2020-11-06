using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoPrograIV
{
    class DataBase
    {
 
      
        
        public MySqlConnection ConectionDB()
        {
            try
            {
                string M_str_sqlcon = "server = 127.0.0.1; user id = root; database = misc; Convert Zero Datetime=True; Allow Zero Datetime=True";
                MySqlConnection mysqlcon = new MySqlConnection(M_str_sqlcon);
                return mysqlcon;
            } catch (MySqlException mse)
            { 
            Console.WriteLine("Intente de nuevo\nError:" + mse.Message);
                return null;
            }

        }
        public  MySqlCommand CommandDB(string comando, MySqlConnection conexion)
        {
           
            try
            {
                MySqlCommand mysqlcom = new MySqlCommand(comando, conexion);
                return mysqlcom;
            }
            catch (MySqlException)
            {
                Console.WriteLine("Error al conectar con la base de datos");
                return null;
            }
            

        }
    }
}
