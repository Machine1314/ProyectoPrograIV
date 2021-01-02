using System;
using System.Data.SqlClient;
using Windows.UI.Xaml.Controls;

namespace ProyectoPrograIV
{
    public class Cita
    {
        int Id_cita;
        int Id_medico;
        string Nombre_Usuario;
        string Nombre_Medico;
        string Fecha;
        TimeSpan Tiempo;

        public int Id_cita1 { get => Id_cita; set => Id_cita = value; }
        public int Id_medico1 { get => Id_medico; set => Id_medico = value; }
        public string Nombre_Usuario1 { get => Nombre_Usuario; set => Nombre_Usuario = value; }
        public string Nombre_Medico1 { get => Nombre_Medico; set => Nombre_Medico = value; }

        public TimeSpan Tiempo1 { get => Tiempo; set => Tiempo = value; }
        public string Fecha1 { get => Fecha; set => Fecha = value; }
        public static async void DisplayDialog(string titulo, string contenido)
        {
            ContentDialog noWifiDialog = new ContentDialog
            {
                Title = titulo,
                Content = contenido,
                CloseButtonText = "Ok"
            };
            _ = await noWifiDialog.ShowAsync();
        }
        public static async void DisplayCitaConf(int id, Frame frame)
        {
            ContentDialog Dialog = new ContentDialog
            {
                Title = "Cita",
                Content = "¿Quiere cancelar esta cita?",
                PrimaryButtonText = "Sí",
                CloseButtonText = "Cancelar"
            };

            ContentDialogResult result = await Dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                DataBase.Db.Open();
                string comando = $"DELETE FROM misc.citas WHERE id_cita={id}";
                SqlCommand cmd = DataBase.CommandDB(comando, DataBase.Db);
                cmd.ExecuteNonQuery();
                DataBase.Db.Close();
                frame.Navigate(typeof(BlankPage1));
            }
        }
    }
}
