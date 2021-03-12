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

namespace TextilGyC
{
    /// <summary>
    /// Lógica de interacción para UserControlAdminAsistencias.xaml
    /// </summary>
    public partial class UserControlAdminAsistencias : UserControl
    {
        ManejadorAsistencias asistenciasManager;
        ManejadorEmpleados empleadosManager;
        enum Accion
        {
            editar,
            nulo
        }
        Accion accion;
        public UserControlAdminAsistencias()
        {
            InitializeComponent();
            asistenciasManager = new ManejadorAsistencias();
            empleadosManager = new ManejadorEmpleados();
            ActualizarGrid();
            ActivarCampos(false);
            accion = Accion.nulo;
        }

        private void ActivarCampos(bool v)
        {
            dpFechaEntrada.IsEnabled = v;
            dpFechaSalida.IsEnabled = v;
            dpHoraEntrada.IsEnabled = v;
            dpHoraSalida.IsEnabled = v;
        }

        private void ActualizarGrid()
        {
            dtgAsistencias.ItemsSource = null;
            dtgAsistencias.ItemsSource = asistenciasManager.leer;
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (dtgAsistencias.SelectedItem != null)
            {
                Asistencia asistencia = dtgAsistencias.SelectedItem as Asistencia;
                Empleado empleado = empleadosManager.query(p=>p.id == asistencia.id_emp).SingleOrDefault() as Empleado;
                txtEstado.Text = asistencia.estado;
                txtNombre.Text = empleado.nombre_emp;
                dpFechaEntrada.SelectedDate = asistencia.fecha_entrada;
                dpFechaSalida.SelectedDate = asistencia.fecha_salida;
                dpHoraEntrada.SelectedTime = asistencia.hora_entrada;
                dpHoraSalida.SelectedTime = asistencia.hora_salida;
                ActivarCampos(true);
                accion = Accion.editar;
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (accion != Accion.nulo)
            {
                Asistencia asistencia2 = dtgAsistencias.SelectedItem as Asistencia;
                Asistencia asistencia = new Asistencia()
                {
                    id = asistencia2.id,
                    id_emp = asistencia2.id_emp,
                    estado = txtEstado.Text,
                    fecha_entrada = Convert.ToDateTime(dpFechaEntrada.SelectedDate),
                    fecha_salida = Convert.ToDateTime(dpFechaSalida.SelectedDate),
                    hora_entrada = Convert.ToDateTime(dpHoraEntrada.SelectedTime),
                    hora_salida = Convert.ToDateTime(dpHoraSalida.SelectedTime)
                };

                if (asistenciasManager.editar(asistencia2, asistencia))
                {
                    MessageBox.Show("Registro actualizado");
                    ActualizarGrid();
                    ActivarCampos(false);
                    LimpiarCampos();
                    accion = Accion.nulo;
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar");
                    ActivarCampos(false);
                    LimpiarCampos();
                    accion = Accion.nulo;
                }
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (accion == Accion.nulo)
            {
                if (dtgAsistencias.SelectedItem != null)
                {
                    Asistencia asistencia = dtgAsistencias.SelectedItem as Asistencia;
                    if (asistenciasManager.eliminar(asistencia))
                    {
                        ActualizarGrid();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar la asistencia");
                    }
                }
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
            ActivarCampos(false);
            accion = Accion.nulo;
        }

        private void LimpiarCampos()
        {
            txtEstado.Clear();
            txtNombre.Clear();
            dpFechaEntrada.SelectedDate = null;
            dpFechaSalida.SelectedDate = null;
            dpHoraEntrada.SelectedTime = null;
            dpHoraSalida.SelectedTime = null;
        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            ActualizarGrid();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IEnumerable<Asistencia> lista = asistenciasManager.buscarfecha(Convert.ToDateTime(txtBuscar.Text));
                if (lista != null)
                {
                    dtgAsistencias.ItemsSource = lista;
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
