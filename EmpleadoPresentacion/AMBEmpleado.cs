using EmpleadosEntidad;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EmpleadosNegocio;
using System.Reflection.Emit;

namespace EmpleadoPresentacion
{
    public partial class AMEmpleados : Form
    {
        public AMEmpleados()
        {
            InitializeComponent();
        }

        public int SelectedEmployeeId { get; set; }

        private Empleado empleado = new Empleado();

        public AMEmpleados(int ID)
        {
            InitializeComponent();
            SelectedEmployeeId = ID; // Asignar el ID seleccionado a la propiedad SelectedEmployeeId

            empleado = EmpleadoNegocio.ObtenerEmpleado(ID);

            txtNombreCompleto.Text = empleado.NombreCompleto;
            txtDNI.Text = empleado.DNI;
            txtEdad.Text = empleado.Edad.ToString();
            checkBox1.Checked = empleado.Casado;
            txtSalario.Text = empleado.Salario.ToString();


        }


        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click_1(object sender, EventArgs e)
        {
            empleado.NombreCompleto = txtNombreCompleto.Text;
            empleado.DNI = txtDNI.Text;
            empleado.Edad = int.Parse(txtEdad.Text);
            empleado.Casado = checkBox1.Checked;
            empleado.Salario = Double.Parse(txtSalario.Text);
            if (SelectedEmployeeId != 0) 
            {
                empleado.Id = SelectedEmployeeId;
                EmpleadoNegocio.ModificaEmpleado(empleado);
            SelectedEmployeeId = 0;
            }
            else { 
            EmpleadoNegocio.AltaEmpleado(empleado);
            }
            MessageBox.Show("Se guardo el empleado OK");

            this.Close();
        }
    }
}
