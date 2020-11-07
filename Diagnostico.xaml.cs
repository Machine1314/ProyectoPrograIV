using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
            string comando = $"select nombre from especialidad";
            MySqlCommand cmd = DataBase.CommandDB(comando, DataBase.Db);
            MySqlDataReader mysqlread = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (mysqlread.Read())
            {
                procedimiento_list.Items.Add(mysqlread.GetString(0));
            }
            DataBase.Db.Close();
        }

        private void cobrar_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BlankPage6));
        }
    }
}
