using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
    /// Lógica de interacción para UserControlAdminClientes.xaml
    /// </summary>
    public partial class UserControlAdminClientes : UserControl
    {
        ManejadorCliente clienteManager;
        enum Accion
        {
            Nuevo,
            Editar,
            Nulo
        };
        Accion accion = new Accion();
        public UserControlAdminClientes()
        {
            InitializeComponent();
            clienteManager = new ManejadorCliente();
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
            dtgClientes.ItemsSource = null;
            dtgClientes.ItemsSource = clienteManager.leer;
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
                if (dtgClientes.SelectedItem != null)
                {
                    accion = Accion.Editar;
                    Cliente cliente = dtgClientes.SelectedItem as Cliente;
                    txtDireccion.Text = cliente.direccion_cli.ToString();
                    txtNombre.Text = cliente.nombre_cli.ToString();
                    txtRuc.Text = cliente.ruc_cli.ToString();
                    txtTelefono.Text = cliente.telefono_cli.ToString();
                }
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (accion == Accion.Nuevo)
            {
                if(txtRuc.Text !="" & txtNombre.Text!="" & txtDireccion.Text!="")
                {
                    Cliente cliente = new Cliente()
                    {
                        direccion_cli = txtDireccion.Text,
                        nombre_cli = txtNombre.Text,
                        ruc_cli = txtRuc.Text,
                        telefono_cli = txtTelefono.Text
                    };
                    if (clienteManager.crear(cliente))
                    {
                        MessageBox.Show("Realizado");
                        LimpiarTodo();
                    }
                    else
                    {
                        MessageBox.Show("Ha ocurrido un error " + clienteManager.Error.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Existen campos obligatorios vacíos");
                }
                
            }
            else if (accion == Accion.Editar)
            {
                Cliente cliente = dtgClientes.SelectedItem as Cliente;
                cliente.direccion_cli = txtDireccion.Text;
                cliente.nombre_cli = txtNombre.Text;
                cliente.ruc_cli = txtRuc.Text;
                cliente.telefono_cli = txtTelefono.Text;
                if (clienteManager.editar(cliente, cliente))
                {
                    MessageBox.Show("Realizado");
                    LimpiarTodo();
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error " + clienteManager.Error.ToString());
                }
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (accion == Accion.Nulo)
            {
                if (dtgClientes.SelectedItem != null)
                {
                    Cliente cliente = dtgClientes.SelectedItem as Cliente;
                    if (clienteManager.eliminar(cliente))
                    {
                        MessageBox.Show("Realizado");
                        LimpiarTodo();
                    }
                    else
                    {
                        MessageBox.Show("Ha ocurrido un error " + clienteManager.Error.ToString());
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

            dtgClientes.SelectAllCells();
            dtgClientes.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, dtgClientes);
            dtgClientes.UnselectAllCells();

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
