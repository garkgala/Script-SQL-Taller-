using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Lógica de interacción para UserControlProduccion.xaml
    /// </summary>
    public partial class UserControlProduccion : UserControl
    {
        ManejadorProducto productosManager;
        ManejadorEmpleados empleadosManager;
        ManejadorProduccion produccionManager;
        ManejadorTiposProduccion tiposproduccionManager;
        public UserControlProduccion()
        {
            InitializeComponent();
            dpFecha.SelectedDate = System.DateTime.Now;
            produccionManager = new ManejadorProduccion();
            productosManager = new ManejadorProducto();
            empleadosManager = new ManejadorEmpleados();
            tiposproduccionManager = new ManejadorTiposProduccion();
            ActualizarTablas();
        }
        Empleado empleado = new Empleado();
        Producto productoAnterior = new Producto();
        Producto productoNuevo = new Producto();
        private void ActualizarTablas()
        {
            dtgEmpleados.ItemsSource = null;
            dtgEmpleados.ItemsSource = empleadosManager.leer;

            dtgProdAnt.ItemsSource = null;
            dtgProdAnt.ItemsSource = productosManager.leer;

            dtgProdNuevo.ItemsSource = null;
            dtgProdNuevo.ItemsSource = productosManager.leer;

            cboTipoProduccion.ItemsSource = null;
            cboTipoProduccion.ItemsSource = tiposproduccionManager.nombresTipo;
        }

        private void btnSeleccionarEmpleado_Click(object sender, RoutedEventArgs e)
        {
            if (dtgEmpleados.SelectedItem != null)
            {
                empleado = dtgEmpleados.SelectedItem as Empleado;
                dtgEmpleados.IsEnabled = false;
                btnSeleccionarEmpleado.IsEnabled = false;
                txtBuscarEmpleado.IsEnabled = false;
            }
        }

        private void btnSelProductoAnt_Click(object sender, RoutedEventArgs e)
        {
            if (dtgProdAnt.SelectedItem != null)
            {
                productoAnterior = dtgProdAnt.SelectedItem as Producto;
                dtgProdAnt.IsEnabled = false;
                btnSelProductoAnt.IsEnabled = false;
                txtbuscarProdAnterior.IsEnabled = false;
            }
        }

        private void btnSelProductoNuev_Click(object sender, RoutedEventArgs e)
        {
            if (dtgProdNuevo.SelectedItem != null)
            {
                productoNuevo = dtgProdNuevo.SelectedItem as Producto;
                dtgProdNuevo.IsEnabled = false;
                btnSelProductoNuev.IsEnabled = false;
                txtbuscarProdNuevo.IsEnabled = false;
            }
        }

        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            if(empleado.nombre_emp!=null & productoAnterior.descripcion!=null & productoNuevo.descripcion != null & txtCantidad.Text!="" & cboTipoProduccion.SelectedItem!=null)
            {
                if (Convert.ToInt32(txtCantidad.Text) > productoAnterior.cantidad)
                {
                    MessageBox.Show("No puede producir una cantidad mayor al stock disponible");
                }
                else
                {
                    Produccion produccion = new Produccion()
                    {
                        cantidad = Convert.ToInt64(txtCantidad.Text),
                        fecha_produccion = dpFecha.SelectedDate.Value,
                        id_emp = empleado.id,
                        id_prod_ant = productoAnterior.id,
                        id_prod_nuevo = productoNuevo.id,
                        tipo_produccion = cboTipoProduccion.SelectedItem.ToString(),
                        observaciones = txtObservaciones.Text
                    };
                    if (produccionManager.crear(produccion))
                    {
                        MessageBox.Show("Produccion registrada");
                        LimpiarTodo();
                    }
                    else
                    {
                        MessageBox.Show("Ha ocurrido un error " + produccionManager.Error.ToString());
                    }
                }
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarTodo();
        }

        private void LimpiarTodo()
        {
            txtBuscarEmpleado.Clear();
            txtbuscarProdAnterior.Clear();
            txtbuscarProdNuevo.Clear();
            txtCantidad.Clear();
            txtObservaciones.Clear();
            cboTipoProduccion.SelectedItem = null;
            dtgEmpleados.IsEnabled = true;
            dtgProdAnt.IsEnabled = true;
            dtgProdNuevo.IsEnabled = true;
            btnSeleccionarEmpleado.IsEnabled = true;
            btnSelProductoAnt.IsEnabled = true;
            btnSelProductoNuev.IsEnabled = true;
            txtbuscarProdAnterior.IsEnabled = true;
            txtBuscarEmpleado.IsEnabled = true;
            txtbuscarProdNuevo.IsEnabled = true;
            ActualizarTablas();
        }

        private void txtBuscarEmpleado_TextChanged(object sender, TextChangedEventArgs e)
        {
            dtgEmpleados.ItemsSource = null;
            dtgEmpleados.ItemsSource = empleadosManager.BuscarPorNombre(txtBuscarEmpleado.Text);
        }

        private void txtbuscarProdAnterior_TextChanged(object sender, TextChangedEventArgs e)
        {
            dtgProdAnt.ItemsSource = null;
            dtgProdAnt.ItemsSource = productosManager.BuscarProductoPorNombre(txtbuscarProdAnterior.Text);
        }

        private void txtbuscarProdNuevo_TextChanged(object sender, TextChangedEventArgs e)
        {
            dtgProdNuevo.ItemsSource = null;
            dtgProdNuevo.ItemsSource = productosManager.BuscarProductoPorNombre(txtbuscarProdNuevo.Text);
        }
        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        private void txtCantidad_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
    }
}
