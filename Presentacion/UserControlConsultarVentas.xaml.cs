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
    /// Lógica de interacción para UserControlConsultarVentas.xaml
    /// </summary>
    public partial class UserControlConsultarVentas : UserControl
    {
        ManejadorVentas ventaManager;
        public UserControlConsultarVentas()
        {
            InitializeComponent();
            ventaManager = new ManejadorVentas();
            ActualizarTabla();
        }

        private void ActualizarTabla()
        {
            dtgVentas.ItemsSource = null;
            dtgVentas.ItemsSource = ventaManager.VentasEnDetalle;
        }

        private void PackIcon_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dtgVentas.SelectAllCells();
            dtgVentas.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, this.dtgVentas);
            dtgVentas.UnselectAllCells();

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

        private void txtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            dtgVentas.ItemsSource = null;
            dtgVentas.ItemsSource = ventaManager.VentasEnDetalle.Where(pr => pr.nombre_cli.ToLower().Contains(txtBuscar.Text.ToLower()));
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            dtgVentas.ItemsSource = null;
            dtgVentas.ItemsSource= ventaManager.VentasEnIntervalo(dpFechaInicio.SelectedDate.Value, dpFechaFin.SelectedDate.Value);
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            dpFechaFin.SelectedDate = null;
            dpFechaInicio.SelectedDate = null;
            txtBuscar.Clear();
            ActualizarTabla();
        }
    }
}
