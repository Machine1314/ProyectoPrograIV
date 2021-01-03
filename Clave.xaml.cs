using System.Data.SqlClient;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProyectoPrograIV
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Clave : Page
    {
        public Clave()
        {
            this.InitializeComponent();
        }
        private void ContinuarBtn_Click(object sender, RoutedEventArgs e)
        {
            //Comprueba si los campos estan vacios
            if (!ContrasenaNueva.Password.Equals("") || !ConfContraNueva.Password.Equals(""))
            {
                //Comprueba si los campos coinciden
                if (ConfContraNueva.Password.Equals(ContrasenaNueva.Password))
                {
                    string contra = DataBase.Encrypt(ConfContraNueva.Password);
                    DataBase.Db.Open();
                    string comando = $"UPDATE misc.usersxd set password='{contra}' where email='{Sesion.Mail}'";
                    SqlCommand cmd = DataBase.CommandDB(comando, DataBase.Db);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException se)
                    {
                        Cita.DisplayDialog("Error", se.Message);
                    }
                    this.Frame.Navigate(typeof(MainPage));
                    DataBase.Db.Close();
                }
                else
                {
                    Cita.DisplayDialog("Error", "Las contraseñas no coinciden.\nIntente de nuevo.");
                }
            }
            else
            {
                Cita.DisplayDialog("Error", "Uno o más campos vacios.");
            }
        }
        private void CancelarBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
