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
    /// Lógica de interacción para UserControlPagoPorProduccion.xaml
    /// </summary>
    public partial class UserControlPagoPorProduccion : UserControl
    {
        ManejadorProduccion produccionManager;
        public UserControlPagoPorProduccion()
        {
            InitializeComponent();
            produccionManager = new ManejadorProduccion();
            ActualizarTabla();
        }

        private void ActualizarTabla()
        {
            dtgCalcularProduccion.ItemsSource = null;
            dtgCalcularProduccion.ItemsSource = produccionManager.CalcularProduccion;
        }

        private void Exportar_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dtgCalcularProduccion.SelectAllCells();
            dtgCalcularProduccion.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, this.dtgCalcularProduccion);
            dtgCalcularProduccion.UnselectAllCells();

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
                MessageBox.Show("ah ocurrido un error " + ex.Message);
            }
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            dpFechaFin.SelectedDate = null;
            dpFechaInicio.SelectedDate = null;
            ActualizarTabla();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dpFechaInicio.SelectedDate != null || dpFechaFin.SelectedDate != null)
                {
                    dtgCalcularProduccion.ItemsSource = null;
                    dtgCalcularProduccion.ItemsSource = produccionManager.CalcularProduccionporfechas(dpFechaInicio.SelectedDate.Value, dpFechaFin.SelectedDate.Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error (ambas fechas deben tener valores): " + ex.ToString());
            }
        }
    }
}
