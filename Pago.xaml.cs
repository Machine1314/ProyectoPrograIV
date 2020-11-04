using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
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
    public sealed partial class BlankPage4 : Page
    {
        public BlankPage4()
        {
            this.InitializeComponent();
        }

        private void Regresar_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BlankPage3));
        }

        private void Pago_btn_Click(object sender, RoutedEventArgs e)
        {

            if (ValidarTJ()==true&&num_Tarjeta.Text!=null)
            {
                DisplayDialog("Exito", "Número de tarjeta correcto");
                this.Frame.Navigate(typeof(BlankPage1));
            }
            else
            {
                DisplayDialog("Error", "Número de tarjeta incorrecto");
            }
            
        }
        private void PagoEfect_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BlankPage1));
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
        private bool ValidarTJ()
        {
            string tarjeta = num_Tarjeta.Text;
            int suma = 0;
            bool flag = true;

            for (int i = tarjeta.Length - 1; i >= 0; i--)
            {
                if (!flag)
                {
                    int tmp = (tarjeta[i] - '0') << 1;
                    suma += tmp >= 10 ? tmp - 9 : tmp;
                }
                else
                {
                    suma += (tarjeta[i] - '0');
                }
                flag = !flag;
            }
            return suma % 10 == 0;
        }


    }
}
