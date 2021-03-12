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
    /// Lógica de interacción para UserControlConsultaProduccion.xaml
    /// </summary>
    public partial class UserControlConsultaProduccion : UserControl
    {
        ManejadorProduccion produccionManager;
        public UserControlConsultaProduccion()
        {
            InitializeComponent();
            produccionManager = new ManejadorProduccion();
            ActualizarTabla();
        }

        private void ActualizarTabla()
        {
            dtgConsultaProduccion.ItemsSource = null;
            dtgConsultaProduccion.ItemsSource = produccionManager.ListarProduccion;
        }

        private void Exportar_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dtgConsultaProduccion.SelectAllCells();
            dtgConsultaProduccion.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, this.dtgConsultaProduccion);
            dtgConsultaProduccion.UnselectAllCells();

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
            dtgConsultaProduccion.ItemsSource = null;
            dtgConsultaProduccion.ItemsSource = produccionManager.ListarProduccion.Where(p => p.Empleado.ToLower().Contains(txtBuscar.Text.ToLower()));
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            dtgConsultaProduccion.ItemsSource = null;
            dtgConsultaProduccion.ItemsSource = produccionManager.ListarProduccion.Where(p => p.fecha >= dpFechaInicio.SelectedDate.Value & p.fecha <=dpFechaFin.SelectedDate.Value);
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            txtBuscar.Clear();
            ActualizarTabla();
            dpFechaFin.SelectedDate = null;
            dpFechaInicio.SelectedDate = null;
        }
    }
}
