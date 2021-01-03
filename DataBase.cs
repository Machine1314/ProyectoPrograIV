using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using Windows.Security.Cryptography.Core;

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
            }
            catch (SqlException se)
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
        public static string Encrypt(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            return Convert.ToBase64String(hashBytes);
        }
        public static bool Verify(string savedPasswordHash, string password)
        {
            /* Fetch the stored value */
            /* Extract the bytes */
            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
            /* Get the salt */
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            /* Compute the hash on the password the user entered */
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            /* Compare the results */
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
