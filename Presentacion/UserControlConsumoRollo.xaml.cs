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
    /// Lógica de interacción para UserControlConsumoRollo.xaml
    /// </summary>
    public partial class UserControlConsumoRollo    : UserControl
    {
        ManejadorConsumoRollo consumo_RolloManager;
        ManejadorProducto productoManager;
        public UserControlConsumoRollo()
        {
            InitializeComponent();
            consumo_RolloManager = new ManejadorConsumoRollo();
            productoManager = new ManejadorProducto();
            btnConsumir.IsEnabled = false;
        }
        Producto producto;
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (txtId.Text != "")
            {
                producto = productoManager.BuscarPorId(txtId.Text);
                if (producto.descripcion != null)
                {
                    txtDescripcion.Text = producto.descripcion.ToString();
                    txtCantidad.Text = producto.cantidad.ToString();
                    txtTipoProducto.Text = producto.tipo_producto.ToString();
                    txtPrecio.Text = producto.precio.ToString();
                    btnConsumir.IsEnabled = true;
                    txtCantidadConsumir.Focus();
                }
                else
                {
                    MessageBox.Show("No se encontro el producto");
                }
            }
        }

        private void btnConsumir_Click(object sender, RoutedEventArgs e)
        {
            if (txtCantidadConsumir.Text != "")
            {
                if (Convert.ToInt32(txtCantidad.Text) >= Convert.ToInt32(txtCantidadConsumir.Text)) 
                { 
                    if (consumo_RolloManager.consumir_rollo(producto, Convert.ToInt32(txtCantidadConsumir.Text)))
                    {
                        MessageBox.Show("Realizado");
                        LimpiarTodo();
                    }
                    else
                    {
                        MessageBox.Show("Ha ocurrido un error: " + consumo_RolloManager.Error.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("La cantidad a consumir no puede ser mayor al stock");
                    LimpiarTodo();
                }
            }
        }

        private void LimpiarTodo()
        {
            txtCantidad.Clear();
            txtCantidadConsumir.Clear();
            txtDescripcion.Clear();
            txtId.Clear();
            txtPrecio.Clear();
            txtTipoProducto.Clear();
            btnConsumir.IsEnabled = false;
        }
        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
        private void txtCantidadConsumir_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
    }
}
