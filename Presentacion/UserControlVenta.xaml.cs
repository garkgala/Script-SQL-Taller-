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
    /// Lógica de interacción para UserControlVenta.xaml
    /// </summary>
    public partial class UserControlVenta : UserControl
    {
        ManejadorCliente clienteManager;
        ManejadorProducto productoManager;
        ManejadorVentas ventaManager;

        public UserControlVenta()
        {
            InitializeComponent();
            clienteManager = new ManejadorCliente();
            productoManager = new ManejadorProducto();
            ventaManager = new ManejadorVentas();
            LimpiarTodo();
            ActualizarTablaProductos();
        }

        private void LimpiarTodo()
        {
            txtId.Clear();
            txtBuscarProducto.Clear();
            txtCantidad.Clear();
            txtDireccion.Clear();
            txtIgv.Clear();
            txtNfactura.Clear();
            txtNombre.Clear();
            txtRuc.Clear();
            txtSubTotal.Clear();
            txtTelefono.Clear();
            txtTotal.Clear();
            dtgVenta.Items.Clear();
            txtId.IsEnabled = true;
            ActualizarTablaProductos();
            txtId.Focus();
            btnAgregar.IsEnabled = false;
        }

        private void ActualizarTablaProductos()
        {
            dtgProductos.ItemsSource = null;
            dtgProductos.ItemsSource = productoManager.leer;
        }

        private void txtBuscarProducto_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtBuscarProducto.Text != "")
            {
                dtgProductos.ItemsSource = null;
                dtgProductos.ItemsSource = productoManager.BuscarProductoPorNombre(txtBuscarProducto.Text);
            }
            else
            {
                ActualizarTablaProductos();
            }
        }

        private void btnBuscarCliente_Click(object sender, RoutedEventArgs e)
        {
            if (txtId.Text != "")
            {
                Cliente cliente = new Cliente();
                cliente = clienteManager.BuscarPorId(txtId.Text);
                if (cliente.nombre_cli != null)
                {
                    txtNombre.Text = cliente.nombre_cli.ToString();
                    txtDireccion.Text = cliente.direccion_cli.ToString();
                    txtRuc.Text = cliente.ruc_cli.ToString();
                    txtTelefono.Text = cliente.telefono_cli.ToString();
                    txtNfactura.Focus();
                    txtId.IsEnabled = false;
                }
                else
                {
                    MessageBox.Show("Cliente no encontrado");
                    txtId.Focus();
                    txtId.Select(0, txtId.Text.Length);
                }
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarTodo();
        }

        private void txtNfactura_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtNfactura.Text != "")
            {
                if (ventaManager.validar_factura(txtNfactura.Text))
                {
                    MessageBox.Show("Ya existe ese numero de factura en la Base de datos");
                    LimpiarTodo();
                    return;
                }
                else
                {
                    btnAgregar.IsEnabled = true;
                }
            }
        }
        public class productoVenta
        {
            public int id { get; set; }
            public string descripcion { get; set; }
            public int cantidad { get; set; }
            public double precio { get; set; }
            public double total { get; set; }
        }
        public List<productoVenta> listadeventa = new List<productoVenta>();
        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
                        
            if (dtgProductos.SelectedItem != null)
            {
                if (txtCantidad.Text != "")
                {
                    Producto producto = dtgProductos.SelectedItem as Producto;
                    if (producto.cantidad < Convert.ToInt32(txtCantidad.Text))
                    {
                        MessageBox.Show("La cantidad no puede ser menor al stock disponible.");
                    }
                    else
                    {
                        productoVenta prod = new productoVenta
                        {
                            id = producto.id,
                            descripcion = producto.descripcion,
                            cantidad = Convert.ToInt32(txtCantidad.Text),
                            precio = producto.precio,
                            total = (Convert.ToInt32(txtCantidad.Text) * producto.precio)
                        };
                        dtgVenta.Items.Add(prod);
                        actualizarTotales();
                    }
                }
            }
        }

        private void btnQuitar_Click(object sender, RoutedEventArgs e)
        {
            if (dtgVenta.SelectedItem != null)
            {
                dtgVenta.Items.RemoveAt(dtgVenta.SelectedIndex);
                actualizarTotales();
            }
        }

        private void actualizarTotales()
        {
            if (dtgVenta.Items.Count > 0)
            {
                double total = 0;
                foreach (productoVenta item in dtgVenta.Items){
                    total += item.total;
                    txtTotal.Text = total.ToString();
                    txtSubTotal.Text = total.ToString();
                    txtIgv.Text = "0";
                }
            }else
            {
                txtTotal.Text = "";
                txtSubTotal.Text = "";
                txtIgv.Text = "";
            }
        }

        private void btnFinalizar_Click(object sender, RoutedEventArgs e)
        {
            if (dtgVenta.Items.Count > 0)
            {
                Venta venta = new Venta();
                  foreach (productoVenta item in dtgVenta.Items) 
                {
                    venta.id_cli = Convert.ToInt64(txtId.Text);
                    venta.id_prod = item.id;
                    venta.n_factura = txtNfactura.Text;
                    venta.precio_venta = item.precio;
                    venta.fecha_venta = DateTime.Now;
                    venta.cantidad = item.cantidad;

                    ventaManager.crear(venta);
                }
                MessageBox.Show("venta realizada");
                LimpiarTodo();
            }
        }
        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
        private void txtId_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void txtCantidad_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
    }
}
