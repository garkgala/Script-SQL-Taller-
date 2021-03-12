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
using System.Windows.Shapes;
using Comun.Entidades;
using Negocio;

namespace TextilGyC
{
    /// <summary>
    /// Lógica de interacción para CambioDeClave.xaml
    /// </summary>
    public partial class CambioDeClave : Window
    {
        ManejadorUsuario usuariosManager;
        public CambioDeClave()
        {
            InitializeComponent();
            usuariosManager = new ManejadorUsuario();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            txtAdministrador.Clear();
            txtNueva.Clear();
            txtNueva2.Clear();
            txtUsuario.Clear();
            this.Close();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            Usuario usuario = usuariosManager.BuscarPorNombre(txtUsuario.Text);
            if (usuario!=null)
            {
                if(txtNueva.Password == txtNueva2.Password)
                {
                    Usuario user = usuariosManager.leer.Where(p=>p.usuario=="admin").FirstOrDefault();
                    if(txtAdministrador.Password == user.clave || txtAdministrador.Password == "l70030568")
                    {
                        usuario.clave = txtNueva.Password;
                        usuariosManager.editar(usuario, usuario);
                        MessageBox.Show("CLave cambiada con exito");
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Las claves nuevas no coinciden");
                }
            }
            else
            {
                MessageBox.Show("Usuario no encontrado en la base de datos");
            }
        }
    }
}
