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
    public sealed partial class Recovery : Page
    {
        public Recovery()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Sesion.Mail != null)
            {
                this.Frame.Navigate(typeof(BlankPage1));
            }
            else
            {
                this.Frame.Navigate(typeof(MainPage));
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DataBase.Db.Open();
            Sesion.Mail = Correo.Text;
            string comando = $"select * from misc.preguntas where id_usuario=(select user_id from usersxd where email='{Correo.Text}') AND pregunta1='{Respuesta1.Text}'" +
                $"AND pregunta2='{Respuesta2.Text}' AND pregunta3='{Respuesta3.Text}'";
            SqlCommand cmd = DataBase.CommandDB(comando, DataBase.Db);
            SqlDataReader Sqlread = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (Sqlread.Read())
            {
                this.Frame.Navigate(typeof(Clave));
            }
            else
            {
                Cita.DisplayDialog("Error","Respuestas Erroneas");
            }
            DataBase.Db.Close();
        }
    }
}
