using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Comun.Entidades;
using Comun.Interfaces;
using Negocio;

namespace TextilGyC
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ManejadorUsuario usuarioManager;
        public MainWindow()
        {
            InitializeComponent();
            usuarioManager = new ManejadorUsuario();
            txtUsuario.Focus();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnIniciar_Click(object sender, RoutedEventArgs e)
        {
            int r = 1;
            Usuario user = new Usuario();
            user = usuarioManager.login(txtUsuario.Text, txtclave.Password.ToString());
            if (user != null)
            {
                if (user.rol == 0)
                {
                    r = 0;
                }
                else if (user.rol == 1)
                {
                    r = 1;
                }
                Principal ventana = new Principal(r);
                ventana.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo iniciar sesion." + usuarioManager.Error.ToString());
            }

        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            CambioDeClave cambio = new CambioDeClave();
            cambio.Show();
        }
    }
}
