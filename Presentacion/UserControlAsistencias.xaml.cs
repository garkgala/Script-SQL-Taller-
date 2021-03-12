using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class UserControlAsistencias : UserControl
    {
        ManejadorAsistencias asistenciasManager;
        ManejadorEmpleados empleadosManager;
        public UserControlAsistencias()
        {
            InitializeComponent();
            asistenciasManager = new ManejadorAsistencias();
            empleadosManager = new ManejadorEmpleados();
        }
        public Empleado emp = null;
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtDni.Text != "")
                {
                    emp = empleadosManager.BuscarPorId(txtDni.Text);
                    if (emp.nombre_emp != null)
                    {
                        txtNombre.Text = emp.nombre_emp.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Empleado no encontrado");
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            
            
        }

        private void btnEntrada_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (emp != null)
                {
                    if (asistenciasManager.registrar_asistencia(emp, "Entrada"))
                    {
                        MessageBox.Show("Entrada registrada");
                        limpiarcampos();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo registrar la asistencia, valide que no tenga una asistencia activa");
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show("Error" + ex.Message);
            }
        }

        private void limpiarcampos()
        {
            emp = null;
            txtDni.Clear();
            txtNombre.Clear();
            txtDni.Focus();
        }

        private void btnSalida_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (emp != null)
                {
                    if (asistenciasManager.registrar_asistencia(emp, "Salida"))
                    {
                        MessageBox.Show("Salida registrada");
                        limpiarcampos();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo registrar la salida, verifique que tenga una entrada registrada");
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            
            
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            limpiarcampos();
        }
        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        private void txtDni_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
    }
}
