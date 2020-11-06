using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProyectoPrograIV
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BlankPage3 : Page
    {
        private static DataBase db = new DataBase();
        private MySqlConnection baseDatos = db.ConectionDB();
        public BlankPage3()
        {
            this.InitializeComponent();
            hora_picker.MinuteIncrement = 30;
            //especialidades
            baseDatos.Open();
            string comando = $"select nombre from especialidad";
            MySqlCommand cmd = db.CommandDB(comando, baseDatos);
            MySqlDataReader mysqlread = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (mysqlread.Read())
            {
                especialidad_lista.Items.Add(mysqlread.GetString(0));
            }
            baseDatos.Close();
            //doctores
      

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
        
  
        private void especialidad_lista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            doctor_lista.Items.Clear();
            baseDatos.Open();
            string comando = $"select nombre, apellido, id_medico from medico where id_especialidad=(select id_especialidad from especialidad where nombre='{especialidad_lista.SelectedValue}')";
            MySqlCommand cmd = db.CommandDB(comando, baseDatos);
            MySqlDataReader mysqlread = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (mysqlread.Read())
            {
                string objeto = $"{mysqlread.GetString(0)} {mysqlread.GetString(1)} {int.Parse(mysqlread.GetString(2))}";
                doctor_lista.Items.Add(objeto);
            }
            baseDatos.Close();

        }
        //Mateo
        //Metodo que actualizara los limites de hora segun el doctor
        private void doctor_lista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string phrase = doctor_lista.SelectedValue.ToString();
            string[] words = phrase.Split(' ');
            Sesion.Id_medico = int.Parse(words[2]);
        }

        private void regresar_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BlankPage1), id_user.Text);
        }

        private void Agendar_btn_Click(object sender, RoutedEventArgs e)
        {
            if (fecha_header.SelectedText==null || hora_picker.SelectedTime == null 
                || especialidad.SelectedText == null || doctor_lista.SelectedItem == null)
            {
                DisplayDialog("Error","Uno o más campos no han sido ingresados");
            }
            else
            {
                int dia, mes, año;
                dia = fecha_picker.Date.Value.Day;
                mes = fecha_picker.Date.Value.Month;
                año = fecha_picker.Date.Value.Year;
                int hora, minuto;
                hora = hora_picker.SelectedTime.Value.Hours;
                minuto = hora_picker.SelectedTime.Value.Minutes;
                baseDatos.Open();
                try
                {
                    string comando = $"insert into citas(id_usuario, id_medico, fecha, hora) values ((select user_id from usersxd where email='{Sesion.Mail}'), {Sesion.Id_medico}, '{año}-{dia}-{mes}', '{hora}:{minuto}')";
                    MySqlCommand cmd = db.CommandDB(comando, baseDatos);

                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException mse)
                {
                    DisplayDialog("Error", mse.Message);
                }

                baseDatos.Close();
                this.Frame.Navigate(typeof(BlankPage1));
            }

        }

        private void fecha_picker_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
 
        }

        private void hora_picker_SelectedTimeChanged(TimePicker sender, TimePickerSelectedValueChangedEventArgs args)
        {
 
        }
    }
}
