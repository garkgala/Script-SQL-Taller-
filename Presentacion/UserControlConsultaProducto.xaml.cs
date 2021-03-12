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
    /// Lógica de interacción para UserControlConsultaProducto.xaml
    /// </summary>
    public partial class UserControlConsultaProducto : UserControl
    {
        ManejadorProducto productoManager;
        
        public UserControlConsultaProducto()
        {
            InitializeComponent();
            productoManager = new ManejadorProducto();
            ActualizarTabla();
        }

        private void ActualizarTabla()
        {
            dtgProductos.ItemsSource = null;
            dtgProductos.ItemsSource = productoManager.leer;
        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            ActualizarTabla();
        }

        private void txtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtBuscar.Text != "")
            {
               dtgProductos.ItemsSource= productoManager.BuscarProductoPorNombre(txtBuscar.Text);
            }
            else
            {
                ActualizarTabla();
            }
        }

        private void PackIcon_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dtgProductos.SelectAllCells();
            dtgProductos.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, dtgProductos);
            dtgProductos.UnselectAllCells();

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
