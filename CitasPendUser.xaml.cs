using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Tls;
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
    public sealed partial class BlankPage1 : Page
    {
        static Sesion Info = new Sesion();
        private static DataBase db = new DataBase();
        private MySqlConnection baseDatos = db.ConectionDB();

        public BlankPage1()
        {
            this.InitializeComponent();
            txt_bnd.Text = $"Bienvenido, {Info.Nombre}";
            baseDatos.Open();
            string comando2 = $"select id_cita, hora, medico.nombre, medico.apellido  from citas join medico on medico.id_medico=citas.id_medico where citas.id_usuario = (SELECT user_id from usersxd where email='{Info.Mail}')";
            try
            {
                MySqlCommand cmd2 = db.CommandDB(comando2, baseDatos);
                MySqlDataReader mysqlread2 = cmd2.ExecuteReader(CommandBehavior.CloseConnection);
                while (mysqlread2.Read())
                {
                    TextBlock textoBlock = new TextBlock();
                    textoBlock.Text = $@"{mysqlread2.GetString(0)}          {mysqlread2.GetString(1)}                    {mysqlread2.GetString(2)} {mysqlread2.GetString(3)}";
                    textoBlock.FontSize = 32;
                    citas_list.Items.Add(textoBlock);
                }
            }
            catch (MySqlException mse)
            {
                DisplayDialog("Error", mse.Message);
            }

            baseDatos.Close();


        }
     

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            baseDatos.Open();
            Info.Mail = (string)e.Parameter;
            String comando = $"select name from usersxd where email='{e.Parameter}'";
            MySqlCommand cmd = db.CommandDB(comando, baseDatos);
            MySqlDataReader mysqlread = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (mysqlread.Read())
            {
                txt_bnd.Text = $"Bienvenido, {e.Parameter}";
                Info.Nombre = mysqlread.GetString(0);
            }
            else
            {
                txt_bnd.Text = "Bienvenido Sin Parametro";
            }
            baseDatos.Close();
            baseDatos.Open();
            string comando2 = $"select id_cita, hora, medico.nombre, medico.apellido  from citas join medico on medico.id_medico=citas.id_medico where citas.id_usuario = (SELECT user_id from usersxd where email='{e.Parameter}')";
            try
            {
                MySqlCommand cmd2 = db.CommandDB(comando2, baseDatos);
                MySqlDataReader mysqlread2 = cmd2.ExecuteReader(CommandBehavior.CloseConnection);
                while (mysqlread2.Read())
                {
                    TextBlock textoBlock = new TextBlock();
                    textoBlock.Text = $@"{mysqlread2.GetString(0)}          {mysqlread2.GetString(1)}                    {mysqlread2.GetString(2)} {mysqlread2.GetString(3)}";
                    textoBlock.FontSize = 32;
                    citas_list.Items.Add(textoBlock);
                }
            }
            catch (MySqlException mse)
            {
                DisplayDialog("Error", mse.Message);
            }

            baseDatos.Close();


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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void NuevaCita_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BlankPage3),  id_user.Text);
        }
    }
}

