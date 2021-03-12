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
    /// Lógica de interacción para UserControlConsultaConsumoRollo.xaml
    /// </summary>
    public partial class UserControlConsultaConsumoRollo : UserControl
    {
        ManejadorConsumoRollo consumoManager;

        public UserControlConsultaConsumoRollo()
        {
            InitializeComponent();
            consumoManager = new ManejadorConsumoRollo();
            ActualizarTabla();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (dtgConsumosRollos.SelectedItem != null)
            {
                VistaConsumos vc = dtgConsumosRollos.SelectedItem as VistaConsumos;
                Consumo_rollo consumo = new Consumo_rollo()
                {
                    id = vc.id,
                    cantidad = vc.cantidad
                };
                if (consumoManager.eliminar(consumo))
                {
                    MessageBox.Show("Consumo eliminado");
                    ActualizarTabla();
                }else
                {
                    MessageBox.Show("Ha ocurrido un error " + consumoManager.Error.ToString());
                }
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            dpFecha.SelectedDate = null;
            ActualizarTabla();
            dpFecha.Focus();
        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            ActualizarTabla();
        }

        private void ActualizarTabla()
        {
            dtgConsumosRollos.ItemsSource = null;
            dtgConsumosRollos.ItemsSource = consumoManager.visualizarConsumos;
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            dtgConsumosRollos.ItemsSource = null;
            dtgConsumosRollos.ItemsSource = consumoManager.BuscarConsumoPorFecha(dpFecha.SelectedDate.Value);
        }

        private void Exportar_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dtgConsumosRollos.SelectAllCells();
            dtgConsumosRollos.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, this.dtgConsumosRollos);
            dtgConsumosRollos.UnselectAllCells();

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
