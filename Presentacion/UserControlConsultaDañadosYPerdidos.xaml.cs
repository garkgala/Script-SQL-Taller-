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
    /// Lógica de interacción para UserControlConsultaDañadosYPerdidos.xaml
    /// </summary>
    public partial class UserControlConsultaDañadosYPerdidos : UserControl
    {
        ManejadorProductoPerdido perdidoManager;
        ManejadorProduccionDañada dañadoManager;
        public UserControlConsultaDañadosYPerdidos()
        {
            InitializeComponent();
            perdidoManager = new ManejadorProductoPerdido();
            dañadoManager = new ManejadorProduccionDañada();
            ActualizarTablas();
        }

        private void ActualizarTablas()
        {
            dtgConsultaPerdidos.ItemsSource = null;
            dtgConsultarDañados.ItemsSource = null;
            dtgConsultaPerdidos.ItemsSource = perdidoManager.VisualizarPerdidos;
            dtgConsultarDañados.ItemsSource = dañadoManager.VisualizarDañados;
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (dpFechaInicio.SelectedDate.Value != null || dpFechaFin.SelectedDate.Value != null)
            {
                dtgConsultaPerdidos.ItemsSource = null;
                dtgConsultaPerdidos.ItemsSource = perdidoManager.VisualizarPerdidosPorFecha(dpFechaInicio.SelectedDate.Value, dpFechaFin.SelectedDate.Value);
            }
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            dtgConsultaPerdidos.ItemsSource = null;
            dtgConsultaPerdidos.ItemsSource = perdidoManager.VisualizarPerdidos;
            dpFechaInicio.SelectedDate = null;
            dpFechaFin.SelectedDate = null;
        }

        private void Exportar_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dtgConsultaPerdidos.SelectAllCells();
            dtgConsultaPerdidos.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, this.dtgConsultaPerdidos);
            dtgConsultaPerdidos.UnselectAllCells();

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

        private void ExportarDañados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dtgConsultarDañados.SelectAllCells();
            dtgConsultarDañados.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, this.dtgConsultarDañados);
            dtgConsultarDañados.UnselectAllCells();

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

        private void btnBuscarDañados_Click(object sender, RoutedEventArgs e)
        {
            if (dpFechaInicioD.SelectedDate.Value != null || dpFechaFinD.SelectedDate.Value != null)
            {
                dtgConsultarDañados.ItemsSource = null;
                dtgConsultarDañados.ItemsSource = dañadoManager.VisualizarDañadosPorFechas(dpFechaInicioD.SelectedDate.Value, dpFechaFinD.SelectedDate.Value);
            }
        }

        private void btnLimpiarDañados_Click(object sender, RoutedEventArgs e)
        {
            dpFechaFinD.SelectedDate = null;
            dpFechaInicioD.SelectedDate = null;
            dtgConsultarDañados.ItemsSource = null;
            dtgConsultarDañados.ItemsSource = dañadoManager.VisualizarDañados;
        }
    }
}
