using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Tls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
using ProyectoPrograIV;
using System.Globalization;
using System.Collections.ObjectModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProyectoPrograIV
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BlankPage1 : Page
    {
 
  
        public BlankPage1()
        {
            this.InitializeComponent();
            Lista.ItemsSource = GetCitas();
            DataBase.Db.Open();
            String comando = $"select name from usersxd where email='{Sesion.Mail}'";
            MySqlCommand cmd = DataBase.CommandDB(comando, DataBase.Db);
            MySqlDataReader mysqlread = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (mysqlread.Read())
            {

                Sesion.Nombre = mysqlread.GetString(0);
                txt_bnd.Text = $"Bienvenido, {Sesion.Nombre}";
            }
            else
            {
                txt_bnd.Text = "Bienvenido Sin Parametro";
            }
            DataBase.Db.Close();
            
            /*
            DataBase.Db.Open();
            string comando2 = $"select id_cita, hora, fecha, medico.nombre, medico.apellido  from citas join medico on medico.id_medico=citas.id_medico where fecha BETWEEN now() and DATE_ADD(now(), INTERVAL 1 YEAR) and citas.id_usuario = (SELECT user_id from usersxd where email='{Sesion.Mail}')";
            try
            {
                MySqlCommand cmd2 = DataBase.CommandDB(comando2, DataBase.Db);
                MySqlDataReader mysqlread2 = cmd2.ExecuteReader(CommandBehavior.CloseConnection);
                while (mysqlread2.Read())
                {
                    TextBlock textoBlock = new TextBlock
                    {
                        Text = $@"{mysqlread2.GetString(0)}          {mysqlread2.GetString(1)}          {mysqlread2.GetMySqlDateTime(2)}       {mysqlread2.GetString(3)} {mysqlread2.GetString(4)}",
                        FontSize = 32
                    };
                    citas_list.Items.Add(textoBlock);
                }
            }
            catch (MySqlException mse)
            {


                DisplayDialog("Error", mse.Message);
            }

            DataBase.Db.Close();

            */
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
        public ObservableCollection<Cita> GetCitas()
        {
            string GetCitas = $"select id_cita, hora, fecha, medico.nombre, medico.apellido  from citas join medico on medico.id_medico=citas.id_medico where fecha BETWEEN now() and DATE_ADD(now(), INTERVAL 1 YEAR) and citas.id_usuario = (SELECT user_id from usersxd where email='{Sesion.Mail}')";

            var CitasList = new ObservableCollection<Cita>();
            
            DataBase.Db.Open();

            try
            {
                MySqlCommand cmd2 = DataBase.CommandDB(GetCitas, DataBase.Db);
                MySqlDataReader mysqlread2 = cmd2.ExecuteReader(CommandBehavior.CloseConnection);
                while (mysqlread2.Read())
                {
                    var CitasInfo = new Cita
                    {
                        Id_cita1 = mysqlread2.GetInt32(0),
                        Tiempo1 = mysqlread2.GetTimeSpan(1),
                        Fecha1 = mysqlread2.GetMySqlDateTime(2),
                        Nombre_Medico1 = mysqlread2.GetString(3) + " " + mysqlread2.GetString(4)
                    };
                    CitasList.Add(CitasInfo);

                }
            }
           
            catch (MySqlException mse)
            {
                DisplayDialog("Error", mse.Message);
                return null;
            }
            DataBase.Db.Close();
            return CitasList;


        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void NuevaCita_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BlankPage3));
        }
    }
}

