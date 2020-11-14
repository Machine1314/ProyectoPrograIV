using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
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

       
        public BlankPage3()
        {
            this.InitializeComponent();
            hora_picker.MinuteIncrement = 30;
            //especialidades
            DataBase.Db.Open();
            string comando = $"select nombre from misc.especialidad";
            SqlCommand cmd = DataBase.CommandDB(comando, DataBase.Db);
            SqlDataReader Sqlread = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (Sqlread.Read())
            {
                especialidad_lista.Items.Add(Sqlread.GetString(0));
            }
            DataBase.Db.Close();
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
            _ = await noWifiDialog.ShowAsync();
        }
        
  
        private void Especialidad_lista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            doctor_lista.SelectedValue = null;
            doctor_lista.Items.Clear();

            DataBase.Db.Open();
            string comando = $"select nombre, apellido, id_medico from misc.medico where id_especialidad=(select id_especialidad from misc.especialidad where nombre='{especialidad_lista.SelectedValue}')";
            SqlCommand cmd = DataBase.CommandDB(comando, DataBase.Db);
            SqlDataReader Sqlread = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (Sqlread.Read())
            {
                string objeto = $"{Sqlread.GetString(0)} {Sqlread.GetString(1)} {Sqlread.GetInt32(2)}";
                doctor_lista.Items.Add(objeto);
            }
            DataBase.Db.Close();

        }

        private void Doctor_lista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                string phrase = doctor_lista.SelectedValue?.ToString();
            if (phrase != null)
            {
                string[] words = phrase.Split(' ');
                Sesion.Id_medico = int.Parse(words[2]);
            }
        
        }

        private void Regresar_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BlankPage1));
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
                DataBase.Db.Open();
                try
                {
                    string comando = $"insert into misc.citas(id_usuario, id_medico, fecha, hora) values ({Sesion.Id_user}, {Sesion.Id_medico}, '{año}-{mes}-{dia}', '{hora}:{minuto}')";
                    SqlCommand cmd = DataBase.CommandDB(comando, DataBase.Db);

                    cmd.ExecuteNonQuery();
                }
                catch (SqlException mse)
                {
                    DisplayDialog("Error", mse.Message);
                }

                DataBase.Db.Close();
                this.Frame.Navigate(typeof(BlankPage1));
            }

        }

        private void Fecha_picker_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
 
        }

        private void Hora_picker_SelectedTimeChanged(TimePicker sender, TimePickerSelectedValueChangedEventArgs args)
        {
 
        }
    }
}
