using System.Data;
using System.Data.SqlClient;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ProyectoPrograIV
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        //Botones
        //Este metodo ingresa informacion a la base de datos
        private void Login_Btn_Click(object sender, RoutedEventArgs e)
        {
            Sesion.Mail = nombre.Text;
            string comandoMedico = $"select * from misc.medico where email='{nombre.Text}'  COLLATE Latin1_General_CS_AS  and contrasena='{contraseña_txt.Password}'  COLLATE Latin1_General_CS_AS ";
            string comandoUsuario = $"select * from misc.usersxd where email='{nombre.Text}'  COLLATE Latin1_General_CS_AS  and password='{contraseña_txt.Password}'  COLLATE Latin1_General_CS_AS ";
            DataBase.Db.Open();
            try
            {
                if (nombre.Text.Contains("@to-do/list.com"))
                {
                    SqlCommand cmd2 = DataBase.CommandDB(comandoMedico, DataBase.Db);
                    SqlDataReader Sqlread2 = cmd2.ExecuteReader(CommandBehavior.CloseConnection);
                    if (Sqlread2.Read())
                    {
                        Sesion.Id_especialidad = Sqlread2.GetInt32(3);
                        Sesion.Id_medico = Sqlread2.GetInt32(0);
                        DataBase.Db.Close();
                        this.Frame.Navigate(typeof(BlankPage6));
                    }
                    else
                    {
                        Cita.DisplayDialog("Contraseña incorrecta", "Ingreso de nuevo");
                    }
                }
                else
                {
                    SqlCommand cmd = DataBase.CommandDB(comandoUsuario, DataBase.Db);
                    SqlDataReader Sqlread = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (Sqlread.Read())
                    {
                        
                        Sesion.Id_user = Sqlread.GetInt32(0);
                        DataBase.Db.Close();
                        this.Frame.Navigate(typeof(BlankPage1));
                    }
                    else
                    {
                        Cita.DisplayDialog("Contraseña incorrecta", "Intente de nuevo");
                    }
                }
                DataBase.Db.Close();
            }
            catch (SqlException mse)
            {
                Cita.DisplayDialog("Error al cargar datos", "Intente de nuevo\nError:" + mse.Message);
            }
           
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BlankPage2));
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Recovery));
        }
    }
}
