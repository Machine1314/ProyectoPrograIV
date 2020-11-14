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
using System.Data.SqlClient;
using System.Data.SqlTypes;

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
            String comando = $"select name from misc.usersxd where email='{Sesion.Mail}'";
            SqlCommand cmd = DataBase.CommandDB(comando, DataBase.Db);
            SqlDataReader sqlread = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (sqlread.Read())
            {

                Sesion.Nombre = sqlread.GetString(0);
                txt_bnd.Text = $"Bienvenido, {Sesion.Nombre}";
            }
            else
            {
                txt_bnd.Text = "Bienvenido Sin Parametro";
            }
            DataBase.Db.Close();

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
        public ObservableCollection<Cita> GetCitas()
        {
            string GetCitas = $"select id_cita, hora, fecha, medico.nombre, medico.apellido  from misc.citas " +
                $"join misc.medico on medico.id_medico=citas.id_medico where fecha BETWEEN GETDATE() " +
                $"and DATEADD(year, 1, GETDATE()) and citas.id_usuario = " +
                $"(SELECT user_id from misc.usersxd where email='{Sesion.Mail}') AND pagado=0";

            var CitasList = new ObservableCollection<Cita>();
            
            DataBase.Db.Open();

            try
            {
                SqlCommand cmd2 = DataBase.CommandDB(GetCitas, DataBase.Db);
                SqlDataReader sqlread2 = cmd2.ExecuteReader(CommandBehavior.CloseConnection);
                while (sqlread2.Read())
                {
                    var CitasInfo = new Cita
                    {
                        Id_cita1 = sqlread2.GetInt32(0),
                        Tiempo1 = sqlread2.GetTimeSpan(1),
                        Fecha1 =  sqlread2.GetDateTime(2).ToString("dd/MM/yyyy"),
                        Nombre_Medico1 = sqlread2.GetString(3) + " " + sqlread2.GetString(4)
                    };
                    CitasList.Add(CitasInfo);

                }
            }
           
            catch (SqlException se)
            {
                DisplayDialog("Error", se.Message);
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

