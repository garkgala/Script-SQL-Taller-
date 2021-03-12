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
using Negocio;
using Comun.Entidades;
using System.IO;
using System.Diagnostics;

namespace TextilGyC
{
    /// <summary>
    /// Lógica de interacción para UserControlAdminProveedores.xaml
    /// </summary>
    public partial class UserControlAdminProveedores : UserControl
    {
        ManejadorProveedor proveedorManager;
        enum Accion
        {
            Nuevo,
            Editar,
            Nulo
        };
        Accion accion = new Accion();
        public UserControlAdminProveedores()
        {
            InitializeComponent();
            proveedorManager = new ManejadorProveedor();
            LimpiarTodo();
        }

        private void LimpiarTodo()
        {
            txtDireccion.Clear();
            txtNombre.Clear();
            txtRuc.Clear();
            txtTelefono.Clear();
            ActualizarTabla();
            accion = Accion.Nulo;
        }

        private void ActualizarTabla()
        {
            dtgProveedores.ItemsSource = null;
            dtgProveedores.ItemsSource = proveedorManager.leer;
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            if (accion == Accion.Nulo)
            {
                accion = Accion.Nuevo;
                txtRuc.Focus();
            }
        }
        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (accion == Accion.Nulo)
            {
                if (dtgProveedores.SelectedItem != null)
                {
                    accion = Accion.Editar;
                    Proveedor proveedor = dtgProveedores.SelectedItem as Proveedor;
                    txtDireccion.Text = proveedor.direccion_prov.ToString();
                    txtNombre.Text = proveedor.nombre_prov.ToString();
                    txtRuc.Text = proveedor.ruc_prov.ToString();
                    txtTelefono.Text = proveedor.telefono_prov.ToString();
                }
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (accion == Accion.Nuevo)
            {
                if ( txtNombre.Text!="" & txtDireccion.Text!="" & txtRuc.Text != "")
                {
                    Proveedor proveedor = new Proveedor()
                    {
                        direccion_prov = txtDireccion.Text,
                        nombre_prov = txtNombre.Text,
                        ruc_prov = txtRuc.Text,
                        telefono_prov = txtTelefono.Text
                    };
                    if (proveedorManager.crear(proveedor))
                    {
                        MessageBox.Show("Realizado");
                        LimpiarTodo();
                    }
                    else
                    {
                        MessageBox.Show("Ha ocurrido un error " + proveedorManager.Error.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Existen campos obligatorios vacíos");
                }
                
            }else if(accion == Accion.Editar)
            {
                Proveedor proveedor = dtgProveedores.SelectedItem as Proveedor;
                proveedor.direccion_prov = txtDireccion.Text;
                proveedor.nombre_prov = txtNombre.Text;
                proveedor.ruc_prov = txtRuc.Text;
                proveedor.telefono_prov = txtTelefono.Text;
                if(proveedorManager.editar(proveedor, proveedor))
                {
                    MessageBox.Show("Realizado");
                    LimpiarTodo();
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error " + proveedorManager.Error.ToString());
                }
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if(accion == Accion.Nulo)
            {
                if (dtgProveedores.SelectedItem != null)
                {
                    Proveedor proveedor = dtgProveedores.SelectedItem as Proveedor;
                    if (proveedorManager.eliminar(proveedor))
                    {
                        MessageBox.Show("Realizado");
                        LimpiarTodo();
                    }else
                    {
                        MessageBox.Show("Ha ocurrido un error " + proveedorManager.Error.ToString());
                    }
                }
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarTodo();
        }

        private void PackIcon_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dtgProveedores.SelectAllCells();
            dtgProveedores.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, dtgProveedores);
            dtgProveedores.UnselectAllCells();

            //El siguiente fragmento de código ayuda a recuperar datos del portapapeles y luego los coloca en un archivo csv.
            String result = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            try
            {
                StreamWriter sw = new StreamWriter("wpfdata.csv");
                sw.WriteLine(result);
                sw.Close();
                Process.Start("wpfdata.csv");
            }
            catch (Exception ex)
            {
                MessageBox.Show("ah ocurrido un error " + ex.ToString());
            }
        }
    }
}
