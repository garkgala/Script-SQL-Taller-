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
    /// Lógica de interacción para UserControlAdminProductos.xaml
    /// </summary>
    public partial class UserControlAdminProductos : UserControl
    {
        ManejadorProducto productoManager;
        enum Accion
        {
            Nuevo,
            editar,
            nulo
        };
        Accion accion = new Accion();
        public UserControlAdminProductos()
        {
            InitializeComponent();
            productoManager = new ManejadorProducto();
            ActualizarGrid();
            accion = Accion.nulo;
        }

        private void ActualizarGrid()
        {
            dtgProductos.ItemsSource = null;
            dtgProductos.ItemsSource = productoManager.leer;
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (accion == Accion.nulo) {
                if (dtgProductos.SelectedItem != null)
                {
                    Producto producto = dtgProductos.SelectedItem as Producto;
                    txtDescripcion.Text = producto.descripcion.ToString();
                    txtTipo_producto.Text = producto.tipo_producto.ToString();
                    txtCantidad.Text = producto.cantidad.ToString();
                    txtPrecio.Text = producto.precio.ToString();
                    accion = Accion.editar;
                }
            }
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
            txtDescripcion.Focus();
            accion = Accion.Nuevo;
        }

        private void LimpiarCampos()
        {
            txtBuscar.Clear();
            txtCantidad.Clear();
            txtDescripcion.Clear();
            txtPrecio.Clear();
            txtTipo_producto.Clear();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (accion == Accion.Nuevo)
            {
                Producto producto = new Producto()
                {
                    descripcion = txtDescripcion.Text,
                    tipo_producto = txtTipo_producto.Text,
                    cantidad = Convert.ToInt16(txtCantidad.Text),
                    precio = Convert.ToDouble(txtPrecio.Text)
                };
                if (productoManager.crear(producto))
                {
                    MessageBox.Show("Producto agregado correctamente");
                    LimpiarCampos();
                    accion = Accion.nulo;
                    txtBuscar.Focus();
                    ActualizarGrid();
                }
            }
            else if(accion == Accion.editar){
                Producto producto = dtgProductos.SelectedItem as Producto;
                producto.cantidad = Convert.ToInt64(txtCantidad.Text);
                producto.descripcion = txtDescripcion.Text;
                producto.tipo_producto = txtTipo_producto.Text;
                producto.precio = Convert.ToDouble(txtPrecio.Text.ToString());
                if (productoManager.editar(producto, producto))
                {
                    MessageBox.Show("Producto actualizado correctamente");
                    accion = Accion.nulo;
                    LimpiarCampos();
                    ActualizarGrid();
                    txtBuscar.Focus();
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar, error: " + productoManager.Error.ToString());
                }
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (accion == Accion.nulo)
            {
                if (dtgProductos.SelectedItem != null)
                {
                    Producto producto = dtgProductos.SelectedItem as Producto;
                    if (productoManager.eliminar(producto))
                    {
                        ActualizarGrid();
                    }
                    else
                    {
                        MessageBox.Show("Ha ocurrido un error: " + productoManager.Error.ToString());
                    }
                }
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
            txtBuscar.Focus();
            accion = Accion.nulo;
        }


        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
            accion = Accion.nulo;
            ActualizarGrid();
        }

        private void txtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtBuscar.Template != null)
            {
                dtgProductos.ItemsSource = productoManager.BuscarProductoPorNombre(txtBuscar.Text);
            }
        }
    }
}
