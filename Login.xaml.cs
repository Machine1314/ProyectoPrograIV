using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
        //Botones
        //Este metodo ingresa informacion a la base de datos
        private void cargar_Btn_Click(object sender, RoutedEventArgs e)
        {
            string mail = nombre.Text;
            string comandoMedico = $"select * from medico where email='{nombre.Text}' and contrasena='{contraseña_txt.Password}'";
            string comandoUsuario = $"select * from usersxd where email='{nombre.Text}' and password='{contraseña_txt.Password}'";
            //string comando = "insert into usersxd(name, email, password) values(" + "'" + nombre.Text + "'" + "," + "'" + correo.Text + "'" + "," + "'" + contrasena.Text + "'" + ")";
            try
            {
                baseDatos.Open();
                if (mail.Contains("@to-do/list.com"))
                {
                    MySqlCommand cmd2 = db.CommandDB(comandoMedico, baseDatos);
                    MySqlDataReader mysqlread2 = cmd2.ExecuteReader(CommandBehavior.CloseConnection);
                    if (mysqlread2.Read())
                    {
                      
                            this.Frame.Navigate(typeof(BlankPage6), nombre.Text);
                   
                    }
                    else
                    {
                        DisplayDialog("Contraseña incorrecta", "Ingreso de nuevo");
                    }
                }
                else
                {
                    MySqlCommand cmd = db.CommandDB(comandoUsuario, baseDatos);
                    MySqlDataReader mysqlread = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (mysqlread.Read())
                    {
                            this.Frame.Navigate(typeof(BlankPage1), nombre.Text);
                    }
                    else
                    {
                        DisplayDialog("Contraseña incorrecta", "Ingreso de nuevo");
                    }
                }
                baseDatos.Close();
            }
            catch (MySqlException mse)
            {
                DisplayDialog("Error al cargar datos", "Intente de nuevo\nError:" + mse.Message);
            }
   
        }
        //Este metodo envia una consulta a la base de datos y regresa la informacion
        /*
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
    
            string comando2 = "select * from usersxd";
             MySqlCommand comando = db.CommandDB(comando2, baseDatos);
            try
            {

                baseDatos.Open();
                MySqlDataReader mysqlread = comando.ExecuteReader(CommandBehavior.CloseConnection);

                string datos = "Indice \t Nombre \t Email \t\t Contraseña\n";
                //Mientras se tenga datos continua la lectura
                while (mysqlread.Read())
                {
                    
                    datos += mysqlread.GetString(0) + " \t" + mysqlread.GetString(1) + " \t" + mysqlread.GetString(2) + " \t" + mysqlread.GetString(3) + "\n";

                }
                datos_box.Text = datos;
                baseDatos.Close();
            }
            catch (MySqlException)
            {
                DisplayDialog("Error al conectar con la base de datos", "Intente de nuevo");
            }
            
            MediaElement mediaElement = new MediaElement();
            var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();
            Windows.Media.SpeechSynthesis.SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Datos Cargados a la aplicacion");
            mediaElement.SetSource(stream, stream.ContentType);
            mediaElement.Play();
        }

        */

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BlankPage2));
        }
    }
}
