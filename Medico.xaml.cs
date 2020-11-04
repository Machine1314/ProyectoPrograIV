using MySql.Data.MySqlClient;
using Renci.SshNet.Messages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
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
    public sealed partial class BlankPage6 : Page
    {

        private static DataBase db = new DataBase();
        private MySqlConnection baseDatos = db.ConectionDB();
        public BlankPage6()
        {
            this.InitializeComponent();
            baseDatos.Open();
            String comando = $"select nombre, apellido, id_medico from medico where email='{Sesion.Mail}'";
            MySqlCommand cmd = db.CommandDB(comando, baseDatos);
            MySqlDataReader mysqlread = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (mysqlread.Read())
            {
                saludoMsg.Text = $"Bienvenido, {mysqlread.GetString(0)} {mysqlread.GetString(1)}";
                Sesion.Id_medico = int.Parse(mysqlread.GetString(2));
            }
            else
            {
                saludoMsg.Text = "Bienvenido";
            }
            baseDatos.Close();

        }
 
      
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void CalendarDatePicker_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            int dia = picker.Date.Value.Day;
            int anio = picker.Date.Value.Year;
            int mes = picker.Date.Value.Month;
            Debug.WriteLine(anio + ":" + mes + ":" + dia );
            string comandoCita = $"select id_cita, us.name, us.edad, hora from citas join usersxd us where us.user_id=id_usuario and fecha='{anio}-{mes}-{dia}' " +
                $"and id_medico=(select id_medico from medico where email='{Sesion.Mail}') ";

            baseDatos.Open();
            try
            {
                MySqlCommand cmd = db.CommandDB(comandoCita, baseDatos);
                MySqlDataReader mysqlread = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (mysqlread.Read())
                {
                    fechas.Text = $@"Id de la cita: {mysqlread.GetString(0)}  Nombre del Paciente: {mysqlread.GetString(1)}
Edad del Paciente: {mysqlread.GetString(2)}
Hora: {mysqlread.GetString(3)}";
                }
                else
                {
                    fechas.Text = "Sin citas para este dia";
                }
            }catch(MySqlException mse)
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
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BlankPage5));
        }
    }
}
