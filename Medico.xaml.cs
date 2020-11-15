using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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
                Cita.DisplayDialog("Error", mse.Message);
                return null;
            }
                DataBase.Db.Close();
                return CitasList;
            }
        private void CalendarDatePicker_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            Lista.ItemsSource = GetCitas();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BlankPage5));
        }

        private void Ingresos_Btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double ingresos = 0;
                DataBase.Db.Open();
                String comando = $"select valor_total from misc.pagos join misc.citas on misc.pagos.id_cita = misc.citas.id_cita where misc.citas.id_medico={Sesion.Id_medico} and fecha  between GETDATE() and DATEADD(MONTH, 1, GETDATE())";
                SqlCommand cmd = DataBase.CommandDB(comando, DataBase.Db);
                SqlDataReader Sqlread = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (Sqlread.Read())
                {
                    ingresos += Sqlread.GetDouble(0);
                }
                Cita.DisplayDialog("Ingresos del mes", ingresos.ToString() + "$");
                DataBase.Db.Close();
            }
            catch (Exception ex)
            {
                Cita.DisplayDialog("Error",ex.Message);
            }

        }
    }
}
