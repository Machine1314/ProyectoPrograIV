using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    public sealed partial class BlankPage5 : Page
    {
        public BlankPage5()
        {
            this.InitializeComponent();

            DataBase.Db.Open();
            string comando = $"select nombre from misc.procedimiento where id_especialidad={Sesion.Id_especialidad}";
            SqlCommand cmd = DataBase.CommandDB(comando, DataBase.Db);
            SqlDataReader Sqlread = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (Sqlread.Read())
            {
                procedimiento_list.Items.Add(Sqlread.GetString(0));
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
        private void Cobrar_Click(object sender, RoutedEventArgs e)
        {
            if (id_cita.Text == "")
            {
                DisplayDialog("Error","El campo ID Cita no puede estar vacio.");
            }
            else
            {
                DataBase.Db.Open();
                string comando = $"insert into misc.pagos(id_cita, id_procedimiento, valor_total) values({int.Parse(id_cita.Text)}, {Sesion.Id_Procedimiento}, {float.Parse(Total_Pago.Text)});" +
                    $"update misc.citas set pagado=1 where id_cita={int.Parse(id_cita.Text)} ";
                SqlCommand cmd = DataBase.CommandDB(comando, DataBase.Db);
                cmd.ExecuteNonQuery();
                DataBase.Db.Close();
                this.Frame.Navigate(typeof(BlankPage6));
            }
        }

        private void Procedimiento_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataBase.Db.Open();
            string comando = $"select precio, id_procedimiento from misc.procedimiento where nombre='{procedimiento_list.SelectedValue}'";
            SqlCommand cmd = DataBase.CommandDB(comando, DataBase.Db);
            SqlDataReader Sqlread = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (Sqlread.Read())
            {
                Sesion.Id_Procedimiento = Sqlread.GetInt32(1);
                Valor_Cita.Text = "20";
                Valor_Procedimiento.Text =  Sqlread.GetDecimal(0).ToString();
                float TotalPago;
                float ValorCita = float.Parse(Valor_Cita.Text);
                float ValorProcedimiento = float.Parse(Valor_Procedimiento.Text);
                float IVA = (float)((ValorProcedimiento + ValorCita) * 0.12);
                IVA_Valor.Text = IVA.ToString();
                TotalPago = ValorCita + ValorProcedimiento + IVA;
                Total_Pago.Text = TotalPago.ToString();
            }
            DataBase.Db.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BlankPage6));
        }
    }
}
