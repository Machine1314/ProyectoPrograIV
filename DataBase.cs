﻿using System;
using System.Data.SqlClient;

namespace ProyectoPrograIV
{
    public class DataBase
    {

        public static readonly SqlConnection Db = ConectionDB();



        private static SqlConnection ConectionDB()
        {
            try
            {
                string M_str_sqlcon = "Data Source=VIVOBOOK;Initial Catalog=ToDoList;" +
                    "Integrated Security=True;Connect Timeout=30;Encrypt=False;" +
                    "TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                SqlConnection sqlcon = new SqlConnection(M_str_sqlcon);
                return sqlcon;
            } catch (SqlException se)
            { 
            Console.WriteLine("Intente de nuevo\nError:" + se.Message);
                return null;
            }

        }
        public static SqlCommand CommandDB(string comando, SqlConnection conexion)
        {
           
            try
            {
                SqlCommand mysqlcom = new SqlCommand(comando, conexion);
                return mysqlcom;
            }
            catch (SqlException)
            {
                Console.WriteLine("Error al conectar con la base de datos");
                return null;
            }
            

        }
    }
}
