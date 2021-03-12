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
using System.Text.RegularExpressions;

namespace TextilGyC
{
    /// <summary>
    /// Lógica de interacción para UserControlDañadosPerdidos.xaml
    /// </summary>
    public partial class UserControlDañadosPerdidos : UserControl
    {
        ManejadorProduccionDañada proddañadaManager;
        ManejadorProductoPerdido prodperdiManager;
        ManejadorProducto productoManager;
        ManejadorEmpleados empleadosManager;
        Producto producto = new Producto();
        public UserControlDañadosPerdidos()
        {
            InitializeComponent();
            proddañadaManager = new ManejadorProduccionDañada();
            prodperdiManager = new ManejadorProductoPerdido();
            productoManager = new ManejadorProducto();
            empleadosManager = new ManejadorEmpleados();
            cboNombreEmp.ItemsSource = empleadosManager.traerNombresEmpleados.ToList();
            producto = null;
            CancelarTodo();
        }

        private void CancelarTodo()
        {
            txtCantidad.Clear();
            txtIdProd.Clear();
            txtObservaciones.Clear();
            txtRegistradoPor.Clear();
            cboNombreEmp.SelectedItem = null;
            txtObservaciones.IsEnabled = false;
            txtRegistradoPor.IsEnabled = false;
            cboNombreEmp.IsEnabled = false;
            producto = null;
            infoStock.Visibility = Visibility.Hidden;
            InfoProducto.Visibility = Visibility.Hidden;
            infoStock.Text = "Stock";
            InfoProducto.Text = "Producto";
        }

        private void CambiarModo(string modo)
        {
            if (modo == "Perdido")
            {
                txtObservaciones.IsEnabled = true;
                txtRegistradoPor.IsEnabled = true;
                cboNombreEmp.IsEnabled = false;
                txtIdProd.Focus();
            }else if (modo == "Dañado")
            {
                txtObservaciones.IsEnabled = false;
                txtRegistradoPor.IsEnabled = false;
                cboNombreEmp.IsEnabled = true;
                txtIdProd.Focus();
            }

        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if(txtIdProd.Text!=null || txtIdProd.Text != "")
            {
                if (txtIdProd.Text != "")
                {
                    producto = productoManager.BuscarPorId(txtIdProd.Text);
                    if (producto.descripcion != null)
                    {
                        infoStock.Text = "Stock";
                        InfoProducto.Text = "Producto";
                        InfoProducto.Text += ": " + producto.descripcion.ToString();
                        infoStock.Text += ": " + producto.cantidad.ToString();
                        infoStock.Visibility = Visibility.Visible;
                        InfoProducto.Visibility = Visibility.Visible;
                    }
                }
            }
        }
        private void chkDañado_Checked(object sender, RoutedEventArgs e)
        {
            CambiarModo("Dañado");
        }

        private void chkPerdido_Checked(object sender, RoutedEventArgs e)
        {
            CambiarModo("Perdido");
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            CancelarTodo();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (producto != null)
            {
                if (txtCantidad.Text != "")
                {
                    if (chkDañado.IsChecked == true)
                    {
                        Produccion_Dañada produccion_dañada = new Produccion_Dañada()
                        {
                            id_prod = producto.id,
                            nombre_emp = cboNombreEmp.SelectedItem.ToString(),
                            fecha = fecha.DisplayDate,
                            cantidad = Convert.ToInt16(txtCantidad.Text)
                        };
                        if (proddañadaManager.crear(produccion_dañada))
                        {
                            MessageBox.Show("Registrado");
                            CancelarTodo();
                        }
                        else
                        {
                            MessageBox.Show("Ha ocurrido un error " + proddañadaManager.Error.ToLower());
                        }
                    }else if (chkPerdido.IsChecked == true)
                    {
                        Producto_Perdido producto_perdido = new Producto_Perdido()
                        {
                            cantidad = Convert.ToInt32(txtCantidad.Text),
                            fecha = fecha.DisplayDate,
                            id_prod = producto.id,
                            observaciones = txtObservaciones.Text,
                            registrado_por = txtRegistradoPor.Text
                        };
                        if (prodperdiManager.crear(producto_perdido))
                        {
                            MessageBox.Show("Registrado");
                            CancelarTodo();
                        }
                        else
                        {
                            MessageBox.Show("Ha ocurrido un error " + proddañadaManager.Error.ToLower());
                        }
                    }
                }
            }
        }
        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
        private void txtIdProd_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void txtCantidad_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
    }
}
