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
    /// Lógica de interacción para UserControlRecepcion.xaml
    /// </summary>
    public partial class UserControlRecepcion : UserControl
    {
        ManejadorProveedor proveedorManager;
        ManejadorProducto productoManager;
        ManejadorRecepcion recepcionManager;
        public UserControlRecepcion()
        {
            InitializeComponent();
            proveedorManager = new ManejadorProveedor();
            productoManager = new ManejadorProducto();
            recepcionManager = new ManejadorRecepcion();
        }

        private void btnBuscarProveedor_Click(object sender, RoutedEventArgs e)
        {
            if (txtIdProv.Text != "")
            {
                Proveedor proveedor = proveedorManager.BuscarPorId(txtIdProv.Text);
                if (proveedor.nombre_prov != null)
                {
                    txtRuc.Text = proveedor.ruc_prov;
                    txtNombre.Text = proveedor.nombre_prov;
                    txtTelefono.Text = proveedor.telefono_prov;
                    txtDireccion.Text = proveedor.direccion_prov;
                    txtIdProd.Focus();
                }
                else
                {
                    MessageBox.Show("No se encuentra el proveedor");
                    txtIdProv.Focus();
                }
            }
        }

        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            if(txtCantidadRec.Text!="" & txtIdProd.Text!="" & txtIdProv.Text != "")
            {
                Recepcion recepcion = new Recepcion()
                {
                    cantidad = Convert.ToInt32(txtCantidadRec.Text),
                    fecha = DateTime.Now,
                    id_prod = Convert.ToInt64(txtIdProd.Text),
                    id_proveedor = Convert.ToInt64(txtIdProv.Text)
                };
                if (recepcionManager.crear(recepcion))
                {
                    MessageBox.Show("Realizado con exito");
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error " + recepcionManager.Error.ToString());
                }
            }
            
        }

        private void LimpiarCampos()
        {
            txtCantidad.Clear();
            txtCantidadRec.Clear();
            txtDescripcion.Clear();
            txtDireccion.Clear();
            txtIdProd.Clear();
            txtIdProv.Clear();
            txtNombre.Clear();
            txtPrecio.Clear();
            txtRuc.Clear();
            txtTelefono.Clear();
            txtTipo_producto.Clear();
            txtIdProv.Focus();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
        }

        private void btnBuscarProducto_Click(object sender, RoutedEventArgs e)
        {
            if (txtIdProd.Text != "")
            {
                Producto producto = productoManager.BuscarPorId(txtIdProd.Text);
                if (producto.descripcion != null)
                {
                    txtDescripcion.Text = producto.descripcion;
                    txtTipo_producto.Text = producto.tipo_producto;
                    txtCantidad.Text = producto.cantidad.ToString();
                    txtPrecio.Text = producto.precio.ToString();
                    txtCantidadRec.Focus();
                }
                else
                {
                    MessageBox.Show("Producto no encontrado");
                }
            }
        }
        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
        private void txtCantidadRec_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void txtIdProv_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void txtIdProd_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
    }
}
