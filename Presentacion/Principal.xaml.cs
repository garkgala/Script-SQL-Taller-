using MaterialDesignThemes.Wpf;
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
using System.Windows.Shapes;

namespace TextilGyC
{
    /// <summary>
    /// Lógica de interacción para Principal.xaml
    /// </summary>
    public partial class Principal : Window
    {
        public Principal(int rol)
        {
            InitializeComponent();

            var menuAdministracion = new List<SubItem>();
            menuAdministracion.Add(new SubItem("Asistencias", new UserControlAdminAsistencias()));
            menuAdministracion.Add(new SubItem("Producción", new UserControlAdminProduccion()));
            menuAdministracion.Add(new SubItem("Administrar consumos", new UserControlConsultaConsumoRollo()));
            menuAdministracion.Add(new SubItem("Tipos de produccion", new UserControlTipoProduccion()));
            menuAdministracion.Add(new SubItem("Calcular produccion (pago)", new UserControlPagoPorProduccion()));
            var item1 = new ItemMenu("Administración", menuAdministracion, PackIconKind.Account);

            var MenuRegistro = new List<SubItem>();
            MenuRegistro.Add(new SubItem("Asistencia", new UserControlAsistencias()));
            MenuRegistro.Add(new SubItem("Administrar productos", new UserControlAdminProductos()));
            MenuRegistro.Add(new SubItem("Registro de Producción", new UserControlProduccion()));
            MenuRegistro.Add(new SubItem("Clientes", new UserControlAdminClientes()));
            MenuRegistro.Add(new SubItem("Proveedores", new UserControlAdminProveedores()));
            MenuRegistro.Add(new SubItem("Empleados", new UserControlEmpleados()));
            MenuRegistro.Add(new SubItem("Consumir Rollo", new UserControlConsumoRollo()));
            MenuRegistro.Add(new SubItem("Dañados y Perdidos", new UserControlDañadosPerdidos()));
            MenuRegistro.Add(new SubItem("Recepción", new UserControlRecepcion()));
            MenuRegistro.Add(new SubItem("Venta", new UserControlVenta()));
            var item2 = new ItemMenu("Registro", MenuRegistro, PackIconKind.TshirtV);

            var menuConsultas = new List<SubItem>();
            menuConsultas.Add(new SubItem("Consultar Empleado", new UserControlConsultarEmpleado()));
            menuConsultas.Add(new SubItem("Consultar Asistencias", new UserControlConsultaAsistencia()));
            menuConsultas.Add(new SubItem("Consultar producto", new UserControlConsultaProducto()));
            menuConsultas.Add(new SubItem("Consulta de Producción", new UserControlConsultaProduccion()));
            menuConsultas.Add(new SubItem("Consultar ventas", new UserControlConsultarVentas()));
            menuConsultas.Add(new SubItem("Consultar compras", new UserControlConsultaRecepcion()));
            menuConsultas.Add(new SubItem("Consulta de Consumos", new UserControlConsultaConsumoRollo()));
            menuConsultas.Add(new SubItem("Consulta Dañados y Perdidos", new UserControlConsultaDañadosYPerdidos()));    
            var item3 = new ItemMenu("Consultas", menuConsultas, PackIconKind.CartVariant);
            
            if (rol == 0)
            {
                Menu.Children.Add(new UserControlMenuItem(item1, this));
            }
            
            Menu.Children.Add(new UserControlMenuItem(item2, this));
            Menu.Children.Add(new UserControlMenuItem(item3, this));

        }

        internal void SwitchScreen(object sender)
        {
            var screen = ((UserControl)sender);

            if (screen != null)
            {
                StackPanelMain.Children.Clear();
                StackPanelMain.Children.Add(screen);
            }
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
