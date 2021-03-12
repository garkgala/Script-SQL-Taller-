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
    /// Lógica de interacción para UserControlConsultarEmpleado.xaml
    /// </summary>
    public partial class UserControlConsultarEmpleado : UserControl
    {
        ManejadorEmpleados empleadosManager;
        public UserControlConsultarEmpleado()
        {
            InitializeComponent();
            empleadosManager = new ManejadorEmpleados();
            ActualizarTabla();
        }

        private void ActualizarTabla()
        {
            dtgConsultaEmpleados.ItemsSource = null;
            dtgConsultaEmpleados.ItemsSource = empleadosManager.leer;
        }

        private void Exportar_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dtgConsultaEmpleados.SelectAllCells();
            dtgConsultaEmpleados.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, this.dtgConsultaEmpleados);
            dtgConsultaEmpleados.UnselectAllCells();

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
            dtgConsultaEmpleados.ItemsSource = null;
            dtgConsultaEmpleados.ItemsSource = empleadosManager.leer.Where(p => p.nombre_emp.ToLower().Contains(txtBuscar.Text.ToLower()));
        }
    }
}
