using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

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
        private async void DisplayDialog(string titulo, string contenido)
        {
            ContentDialog noWifiDialog = new ContentDialog
            {
                Title = titulo,
                Content = contenido,
                CloseButtonText = "Ok"
            };

            _ = await noWifiDialog.ShowAsync();
        }
        private void ContinuarBtn_Click(object sender, RoutedEventArgs e)
        {
            //Comprueba si los campos estan vacios
            if (!ContrasenaNueva.Password.Equals("") || !ConfContraNueva.Password.Equals(""))
            {
                //Comprueba si los campos coinciden
                if (ConfContraNueva.Password.Equals(ContrasenaNueva.Password))
                {
                    DataBase.Db.Open();
                    string comando = $"UPDATE usersxd set password='{ConfContraNueva.Password}' where email='{Sesion.Mail}'";
                    MySqlCommand cmd = DataBase.CommandDB(comando, DataBase.Db);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (MySqlException mse)
                    {
                        DisplayDialog("Error", mse.Message);
                    }
                    this.Frame.Navigate(typeof(MainPage));
                    DataBase.Db.Close();
                }
                else
                {
                    DisplayDialog("Error", "Las contraseñas no coinciden.\nIntente de nuevo.");
                }
            }
            else
            {
                DisplayDialog("Error", "Uno o más campos vacios.");
            }

        }
    }
}
