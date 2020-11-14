
using Renci.SshNet.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
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

        public BlankPage6()
        {
            this.InitializeComponent();
            picker.Date = DateTime.Now;
            DataBase.Db.Open();
            String comando = $"select nombre, apellido, id_medico from misc.medico where email='{Sesion.Mail}'";
            SqlCommand cmd = DataBase.CommandDB(comando, DataBase.Db);
            SqlDataReader Sqlread = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (Sqlread.Read())
            {
                saludoMsg.Text = $"Bienvenido, {Sqlread.GetString(0)} {Sqlread.GetString(1)}";
                Sesion.Id_medico = Sqlread.GetInt32(2);
            }
            else
            {
                saludoMsg.Text = "Bienvenido";
            }
            DataBase.Db.Close();

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
        public ObservableCollection<Cita> GetCitas()
        {
            int dia = picker.Date.Value.Day;
            int anio = picker.Date.Value.Year;
            int mes = picker.Date.Value.Month;
            string GetCitas = $"select id_cita, misc.usersxd.name, hora from misc.citas join misc.usersxd on misc.usersxd.user_id=id_usuario where fecha='{anio}-{mes}-{dia}' and pagado=0 " +
             $"and id_medico={Sesion.Id_medico}";

            var CitasList = new ObservableCollection<Cita>();

            DataBase.Db.Open();

            try
            {
                SqlCommand cmd2 = DataBase.CommandDB(GetCitas, DataBase.Db);
                SqlDataReader Sqlread2 = cmd2.ExecuteReader(CommandBehavior.CloseConnection);
                while (Sqlread2.Read())
                {
                    var CitasInfo = new Cita
                    {
                        Id_cita1 = Sqlread2.GetInt32(0),
                        Tiempo1 = Sqlread2.GetTimeSpan(2),
                        Nombre_Usuario1 = Sqlread2.GetString(1)
                    };
                    CitasList.Add(CitasInfo);
                }
            }
            catch (SqlException mse)
            {
                DisplayDialog("Error", mse.Message);
                return null;
            }
                DataBase.Db.Close();
                return CitasList;
            }
        private void CalendarDatePicker_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            Lista.ItemsSource = GetCitas();
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
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BlankPage5));
        }
    }
}
