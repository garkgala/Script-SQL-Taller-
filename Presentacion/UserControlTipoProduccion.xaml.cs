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
using Negocio;

namespace TextilGyC
{
    /// <summary>
    /// Lógica de interacción para UserControlTipoProduccion.xaml
    /// </summary>
    public partial class UserControlTipoProduccion : UserControl
    {
        ManejadorTiposProduccion manager;
        enum Accion
        {
            Nuevo,
            Nulo
        };
        Accion accion = new Accion();
        public UserControlTipoProduccion()
        {
            InitializeComponent();
            manager = new ManejadorTiposProduccion();
            ActualizarTabla();
            accion = Accion.Nulo;
        }

        private void ActualizarTabla()
        {
            dtgTipoProduccion.ItemsSource = null;
            dtgTipoProduccion.ItemsSource = manager.leer;
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            accion = Accion.Nuevo;
            txtInfo.Visibility = Visibility.Visible;
            iconInfo.Visibility = Visibility.Visible;
            txtTipo.Focus();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (accion == Accion.Nulo)
            {
                if (dtgTipoProduccion.SelectedItem != null)
                {
                    Tipos_Produccion tipos_Produccion = dtgTipoProduccion.SelectedItem as Tipos_Produccion;
                    if (manager.eliminar(tipos_Produccion))
                    {
                        MessageBox.Show("Eliminado");
                        ActualizarTabla();
                        limpiarTodo();
                    }
                    else
                    {
                        MessageBox.Show("Ha oocurrido un error " + manager.Error.ToString());
                    }
                }
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (accion == Accion.Nuevo)
            {
                if (txtTipo.Text != null || txtTipo.Text!="")
                {
                    Tipos_Produccion tipo = new Tipos_Produccion()
                    {
                        tipo_produccion = txtTipo.Text
                    };
                    if (manager.crear(tipo))
                    {
                        MessageBox.Show("Realizado!");
                        ActualizarTabla();
                        limpiarTodo();
                    }
                    else
                    {
                        MessageBox.Show("Ha oocurrido un error " + manager.Error.ToString());
                    }
                }
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            limpiarTodo();
        }

        private void limpiarTodo()
        {
            txtInfo.Visibility = Visibility.Hidden;
            iconInfo.Visibility = Visibility.Hidden;
            txtTipo.Clear();
            accion = Accion.Nulo;
            txtTipo.Focus();
        }
    }
}
