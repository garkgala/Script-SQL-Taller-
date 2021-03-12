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
    /// Lógica de interacción para UserControlConsultaRecepcion.xaml
    /// </summary>
    public partial class UserControlConsultaRecepcion : UserControl
    {
        ManejadorRecepcion recepcionManager;
        public UserControlConsultaRecepcion()
        {
            InitializeComponent();
            recepcionManager = new ManejadorRecepcion();
            ActualizarTabla();
        }

        private void ActualizarTabla()
        {
            dtgConsultaCompras.ItemsSource = null;
            dtgConsultaCompras.ItemsSource = recepcionManager.consultarCompras;
        }

        private void txtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            dtgConsultaCompras.ItemsSource = null;
            dtgConsultaCompras.ItemsSource = recepcionManager.consultarCompras.Where(p => p.descripcion.ToLower().Contains(txtBuscar.Text.ToLower()));
        }

        private void Exportar_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dtgConsultaCompras.SelectAllCells();
            dtgConsultaCompras.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, this.dtgConsultaCompras);
            dtgConsultaCompras.UnselectAllCells();

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

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if(dpFechaInicio.SelectedDate!=null & dpFechaFin.SelectedDate != null)
            {
                dtgConsultaCompras.ItemsSource = null;
                dtgConsultaCompras.ItemsSource = recepcionManager.ComprasPorFechas(dpFechaInicio.SelectedDate.Value, dpFechaFin.SelectedDate.Value);
            }
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            txtBuscar.Clear();
            dpFechaFin.SelectedDate = null;
            dpFechaInicio.SelectedDate = null;
            ActualizarTabla();
        }
    }
}
