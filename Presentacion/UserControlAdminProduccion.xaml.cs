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
using Comun.Entidades;
using Negocio;

namespace TextilGyC
{
    /// <summary>
    /// Lógica de interacción para UserControlAdminProduccion.xaml
    /// </summary>
    public partial class UserControlAdminProduccion : UserControl
    {
        ManejadorProduccion produccionManager;
        public UserControlAdminProduccion()
        {
            InitializeComponent();
            produccionManager = new ManejadorProduccion();
            ActualizarTabla();
        }
        private void ActualizarTabla()
        {
            dtgAdminProduccion.ItemsSource = null;
            dtgAdminProduccion.ItemsSource = produccionManager.ListarProduccion;
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (dtgAdminProduccion.SelectedItem != null)
            {
                VerProduccion vp = dtgAdminProduccion.SelectedItem as VerProduccion;
                Produccion produccion = new Produccion()
                {
                    id = vp.id,
                    cantidad = vp.Cantidad,
                    fecha_produccion = vp.fecha,
                    id_emp = 0,
                    id_prod_ant = 0,
                    id_prod_nuevo = 0,
                    tipo_produccion = vp.TipoProduccion,
                    observaciones = vp.Observaciones
                };
                if (produccionManager.eliminar(produccion))
                {
                    MessageBox.Show("Produccion Eliminada");
                    ActualizarTabla();
                    dpFechaFin.SelectedDate = null;
                    dpFechaInicio.SelectedDate = null;
                }else
                {
                    MessageBox.Show("Ha ocurrido un error " + produccionManager.Error.ToString());
                }
            }
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dpFechaInicio.SelectedDate != null || dpFechaFin.SelectedDate != null)
                {
                    dtgAdminProduccion.ItemsSource = null;
                    dtgAdminProduccion.ItemsSource = produccionManager.ListarProduccion.Where(p => p.fecha >= dpFechaInicio.SelectedDate.Value & p.fecha <= dpFechaFin.SelectedDate.Value);

                }
            }catch(Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);
            }
            
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            ActualizarTabla();
            dpFechaFin.SelectedDate = null;
            dpFechaInicio.SelectedDate = null;
        }
    }
}
