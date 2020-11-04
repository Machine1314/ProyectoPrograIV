using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
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
    public sealed partial class BlankPage2 : Page
    {
        public BlankPage2()
        {
            this.InitializeComponent();
        }
        private static DataBase db = new DataBase();
        private MySqlConnection baseDatos = db.ConectionDB();
        private async void DisplayDialog(string titulo, string contenido)
        {
            ContentDialog noWifiDialog = new ContentDialog
            {
                Title = titulo,
                Content = contenido,
                CloseButtonText = "Ok"
            };

            ContentDialogResult result = await noWifiDialog.ShowAsync();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //comando que ingresa los datos a la base
            if (string.IsNullOrEmpty(cedula_input.Text) || string.IsNullOrEmpty(cel_input.Text)|| string.IsNullOrEmpty(nombre_input.Text) || string.IsNullOrEmpty(contra_input.Text)
                || string.IsNullOrEmpty(contra_input.Text)
                || string.IsNullOrEmpty(contra_input.Text) || string.IsNullOrEmpty(correo_input.Text))
            {
                DisplayDialog("Error", "Uno o mas campos vacios");
            }
            else
            {
                String comprobacion = $"SELECT email, cedula from usersxd where email='{correo_input.Text}' AND cedula={Int64.Parse(cedula_input.Text)}";
                try
                {
                    baseDatos.Open();
                    MySqlCommand cmd2 = db.CommandDB(comprobacion, baseDatos);
                    MySqlDataReader mysqlread = cmd2.ExecuteReader(CommandBehavior.CloseConnection);
                    if (mysqlread.Read())
                    {
                        baseDatos.Close();
                        DisplayDialog("Error", "Datos ingresados previamente.");
                    }
                    else
                    {
                        baseDatos.Close();
                        if (confContra_input.Text.Equals(contra_input.Text))
                        {
                            String comando = $"INSERT INTO usersxd (name, email, password, cedula, celular, edad) values ('{nombre_input.Text}','{correo_input.Text}'" +
                            $",'{contra_input.Text}','{ Int64.Parse(cedula_input.Text)}','{Int64.Parse(cel_input.Text)}', '{ Int64.Parse(edad_input.Text) }')";

                            try
                            {
                                baseDatos.Open();
                                MySqlCommand cmd = db.CommandDB(comando, baseDatos);
                                cmd.ExecuteNonQuery();
                                DisplayDialog("Exito", "Cuenta creada con exito.");
                                baseDatos.Close();
                            }
                            catch (MySqlException mse)
                            {
                                DisplayDialog("Error", "Datos no ingresados");
                            }

                            this.Frame.Navigate(typeof(MainPage));
                        }
                        else
                        {
                            DisplayDialog("Error", "La contraseña no coincide, intente de nuevo.");
                        }
                    }
                    //Fin de la comprobacion
                }
                catch (MySqlException mse)
                {
                    DisplayDialog("Error", "Un error ocurrio en la aplicacion.\n" + mse.Message
                        );
                }
            }

        }
    }
}
