using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProyectoPrograIV
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BlankPage2 : Page
    {
        public BlankPage2()
        {
            this.InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //comando que ingresa los datos a la base
            if (string.IsNullOrEmpty(cedula_input.Text) || string.IsNullOrEmpty(cel_input.Text)|| string.IsNullOrEmpty(nombre_input.Text) || string.IsNullOrEmpty(Contra_txt.Password)
                || string.IsNullOrEmpty(Contra_txt.Password) || string.IsNullOrEmpty(correo_input.Text))
            {
                Cita.DisplayDialog("Error", "Uno o mas campos vacios");
            }
            else
            {
                String comprobacion = $"SELECT email, cedula from misc.usersxd where email='{correo_input.Text}' AND cedula={Int64.Parse(cedula_input.Text)}";
                try
                {
                    DataBase.Db.Open();
                    SqlCommand cmd2 = DataBase.CommandDB(comprobacion, DataBase.Db);
                    SqlDataReader Sqlread = cmd2.ExecuteReader(CommandBehavior.CloseConnection);
                    if (Sqlread.Read())
                    {
                        DataBase.Db.Close();
                        Cita.DisplayDialog("Error", "Datos ingresados previamente.");
                    }
                    else
                    {
                        DataBase.Db.Close();
                        if (ConfContra_txt.Password.Equals(Contra_txt.Password))
                        {
                            string contra = DataBase.Encrypt(ConfContra_txt.Password);
                            String comando = $"INSERT INTO misc.usersxd (name, email, password, cedula, celular, edad) values ('{nombre_input.Text}','{correo_input.Text}'" +
                            $",'{contra}','{ Int64.Parse(cedula_input.Text)}','{Int64.Parse(cel_input.Text)}', '{ Int64.Parse(edad_input.Text) }');" +
                            $"INSERT INTO misc.preguntas(id_usuario, pregunta1, pregunta2, pregunta3) values ((select user_id from misc.usersxd where email='{correo_input.Text}'), '{Pregunta1.Text}','{Pregunta2.Text}', '{Pregunta3.Text}') ";
                            try
                            {
                                DataBase.Db.Open();
                                SqlCommand cmd = DataBase.CommandDB(comando, DataBase.Db);
                                cmd.ExecuteNonQuery();
                                Cita.DisplayDialog("Exito", "Cuenta creada con exito.");
                                DataBase.Db.Close();
                            }
                            catch (SqlException mse)
                            {
                                Cita.DisplayDialog("Error", mse.Message);
                            }
                            this.Frame.Navigate(typeof(MainPage));
                        }
                        else
                        {
                            Cita.DisplayDialog("Error", "La contraseña no coincide, intente de nuevo.");
                        }
                    }
                    //Fin de la comprobacion
                }
                catch (SqlException mse)
                {
                    Cita.DisplayDialog("Error", "Un error ocurrio en la aplicacion.\n" + mse.Message);
                }
            }
        }
        private void Cedula_input_LostFocus(object sender, RoutedEventArgs e)
        {
            if (cedula_input.Text != "")
            {
                char[] Cedula = cedula_input.Text.ToCharArray();
                int par = 0, impar = 0, verifi;
                int aux;
                for (int i = 0; i < 9; i += 2)
                {
                    aux = 2 * int.Parse(Cedula[i].ToString());
                    if (aux > 9)
                        aux -= 9;
                    par += aux;
                }
                for (int i = 1; i < 9; i += 2)
                {
                    impar += int.Parse(Cedula[i].ToString());
                }

                aux = par + impar;
                if (aux % 10 != 0)
                {
                    verifi = 10 - (aux % 10);
                }
                else
                    verifi = 0;
                if (verifi != int.Parse(Cedula[9].ToString()))
                {
                    Err_Num_Ced.Visibility = Visibility.Visible;
                }
                else
                {
                    Err_Num_Ced.Visibility = Visibility.Collapsed;
                }
            }
        }
        private void NumbersOnly(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }
        private void LettersOnly(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => char.IsDigit(c));
        }
        private void Cel_input_LostFocus(object sender, RoutedEventArgs e)
        {
            if (cel_input.Text != "")
            {
                char[] celular = cel_input.Text.ToCharArray();
                if (celular[0] != '0' && celular[1] != '9')
                {
                    Err_Num_Cel.Visibility = Visibility.Visible;
                }
                else
                {
                    Err_Num_Cel.Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}
