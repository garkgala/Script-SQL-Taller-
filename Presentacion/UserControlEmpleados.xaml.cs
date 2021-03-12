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
    /// Lógica de interacción para UserControlAsistencias.xaml
    /// </summary>
    public partial class UserControlEmpleados : UserControl
    {
        ManejadorEmpleados empleadosManager;
        enum accion
        {
            nuevo,
            editar,
            nulo
        }
        accion accionEmpleado;
        public UserControlEmpleados()
        {
            InitializeComponent();
            empleadosManager = new ManejadorEmpleados();
            dtgEmpleados.ItemsSource = empleadosManager.leer;
            ActivarCampos(false);
            cboTipoPago.Items.Add("Semanal");
            cboTipoPago.Items.Add("Mensual");
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            {
                accionEmpleado = accion.editar;
                limpiarCampos();
                Empleado emp = dtgEmpleados.SelectedItem as Empleado;
                if (emp != null)
                {
                    txtCargo.Text = emp.cargo.ToString();
                    txtDireccion.Text = emp.direccion_emp.ToString();
                    txtDni.Text = emp.dni.ToString();
                    txtNombre.Text = emp.nombre_emp.ToString();
                    txtSueldo.Text = emp.sueldo.ToString();
                    txtTelefono.Text = emp.telefono_emp.ToString();
                    dpFecha.SelectedDate = Convert.ToDateTime(emp.fecha_ingreso);
                    txtTipoCargo.Text = emp.tipo_cargo.ToString();
                    cboTipoPago.SelectedItem = emp.tipo_pago.ToString();
                    ActivarCampos(true);
                    accionEmpleado = accion.editar;
                }
            }
        }

        private void limpiarCampos()
        {
            txtCargo.Clear();
            txtDireccion.Clear();
            txtDni.Clear();
            txtNombre.Clear();
            txtSueldo.Clear();
            txtTelefono.Clear();
            txtTipoCargo.Text = null;
            cboTipoPago.SelectedItem = null;
            dpFecha.SelectedDate = null;
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            accionEmpleado = accion.nuevo;
            limpiarCampos();
            ActivarCampos(true);
            txtDni.Focus();
        }

        private void ActivarCampos(bool v)
        {
            txtCargo.IsEnabled = v;
            txtDireccion.IsEnabled = v;
            txtDni.IsEnabled = v;
            txtNombre.IsEnabled = v;
            txtSueldo.IsEnabled = v;
            txtTelefono.IsEnabled = v;
            txtTipoCargo.IsEnabled = v;
            cboTipoPago.IsEnabled = v;
            dpFecha.IsEnabled = v;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (accionEmpleado == accion.nuevo)
            {
                if(txtDni.Text!="" & txtNombre.Text!="" & cboTipoPago.SelectedItem!=null& dpFecha.SelectedDate!=null & txtDireccion.Text!="" & txtCargo.Text!="" & txtSueldo.Text!="" & txtTipoCargo.Text != "")
                {
                    Empleado emp = new Empleado();
                    emp.dni = txtDni.Text;
                    emp.nombre_emp = txtNombre.Text;
                    emp.direccion_emp = txtDireccion.Text;
                    emp.fecha_ingreso = dpFecha.SelectedDate.Value;
                    emp.tipo_cargo = txtTipoCargo.Text;
                    emp.tipo_pago = cboTipoPago.Text;
                    emp.telefono_emp = txtTelefono.Text;
                    emp.sueldo = Convert.ToDouble(txtSueldo.Text);
                    emp.cargo = txtCargo.Text;
                    if (empleadosManager.crear(emp) == true)
                    {
                        MessageBox.Show("Registrado correctamente");
                        limpiarCampos();
                        ActivarCampos(false);
                        accionEmpleado = accion.nulo;
                        dtgEmpleados.ItemsSource = null;
                        dtgEmpleados.ItemsSource = empleadosManager.leer;
                    }
                }else
                {
                    MessageBox.Show("Existen campos obligatorios vacíos");
                }
                
            }else if (accionEmpleado == accion.editar)
            {
                if (txtDni.Text != "" & txtNombre.Text != "" & txtDireccion.Text != "" & txtCargo.Text != "" & txtSueldo.Text != "" & txtTipoCargo.Text != "")
                {
                    Empleado emp = dtgEmpleados.SelectedItem as Empleado;
                    emp.dni = txtDni.Text;
                    emp.nombre_emp = txtNombre.Text;
                    emp.direccion_emp = txtDireccion.Text;
                    emp.fecha_ingreso = dpFecha.SelectedDate.Value;
                    emp.tipo_cargo = txtTipoCargo.Text;
                    emp.tipo_pago = cboTipoPago.Text;
                    emp.telefono_emp = txtTelefono.Text;
                    emp.sueldo = Convert.ToDouble(txtSueldo.Text);
                    emp.cargo = txtCargo.Text;
                    if (empleadosManager.editar(emp, emp) == true)
                    {
                        MessageBox.Show("Actualizado correctamente");
                        limpiarCampos();
                        ActivarCampos(false);
                        accionEmpleado = accion.nulo;
                        dtgEmpleados.ItemsSource = null;
                        dtgEmpleados.ItemsSource = empleadosManager.leer;
                    }
                }else
                {
                    MessageBox.Show("Existen campos obligatorios vacíos");
                }

            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Empleado emp = dtgEmpleados.SelectedItem as Empleado;
            if (emp != null)
            {
                empleadosManager.eliminar(emp);
                dtgEmpleados.ItemsSource = null;
                dtgEmpleados.ItemsSource = empleadosManager.leer;
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            ActivarCampos(false);
            limpiarCampos();
            accionEmpleado = accion.nulo;
        }
    }
}
