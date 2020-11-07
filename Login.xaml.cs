using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ProyectoPrograIV
{
    
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
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

            ContentDialogResult result = await noWifiDialog.ShowAsync();
        }
        //Botones
        //Este metodo ingresa informacion a la base de datos
        private void Login_Btn_Click(object sender, RoutedEventArgs e)
        {
            Sesion.Mail = nombre.Text;
            string comandoMedico = $"select * from medico where BINARY email='{nombre.Text}' and BINARY contrasena='{contraseña_txt.Password}'";
            string comandoUsuario = $"select * from usersxd where BINARY email='{nombre.Text}' and BINARY password='{contraseña_txt.Password}'";
            DataBase.Db.Open();
            try
            {
                if (nombre.Text.Contains("@to-do/list.com"))
                {
                    MySqlCommand cmd2 = DataBase.CommandDB(comandoMedico, DataBase.Db);
                    MySqlDataReader mysqlread2 = cmd2.ExecuteReader(CommandBehavior.CloseConnection);
                    if (mysqlread2.Read())
                    {
                       
                        Sesion.Id_medico = int.Parse(mysqlread2.GetString(0));
                        DataBase.Db.Close();
                        this.Frame.Navigate(typeof(BlankPage6));
                    }
                    else
                    {
                        DisplayDialog("Contraseña incorrecta", "Ingreso de nuevo");
                    }
                }
                else
                {
                    MySqlCommand cmd = DataBase.CommandDB(comandoUsuario, DataBase.Db);
                    MySqlDataReader mysqlread = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (mysqlread.Read())
                    {
                        
                        Sesion.Id_user = int.Parse(mysqlread.GetString(0));
                        DataBase.Db.Close();
                        this.Frame.Navigate(typeof(BlankPage1));
                    }
                    else
                    {
                        DisplayDialog("Contraseña incorrecta", "Ingreso de nuevo");
                    }
                }
                DataBase.Db.Close();
            }
            catch (MySqlException mse)
            {
                DisplayDialog("Error al cargar datos", "Intente de nuevo\nError:" + mse.Message);
            }
           
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BlankPage2));
        }
    }
}
