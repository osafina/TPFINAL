using EmpleadosNegocio;
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

namespace EmpleadoPresentacion
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CargarEmpleados();
            dgEmpleados.SelectionChanged += dgEmpleados_SelectionChanged;
        }

        private int selectedRowId;
        
        private void dgEmpleados_SelectionChanged(object sender, EventArgs e)
        {
            if (dgEmpleados.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgEmpleados.SelectedRows[0];
                selectedRowId = Convert.ToInt32(row.Cells["Id"].Value);
            }
            else
            {
                selectedRowId = 0;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            DataTable dataTable = EmpleadoNegocio.ListarEmpleados(txtNombre.Text);
            dgEmpleados.DataSource = dataTable;
            dgEmpleados.Columns["Id"].Visible = false;
        }


        private void Empleado_FormClosed(object sender, FormClosedEventArgs e)
        {
            CargarEmpleados();
        }

        private void CargarEmpleados()
        {
            DataTable dataTable = EmpleadoNegocio.ListarEmpleados("");
            dgEmpleados.DataSource = dataTable;
            dgEmpleados.Columns["Id"].Visible = false;

        }

        private void btnModificar_Click_1(object sender, EventArgs e)
        {
            
                if (selectedRowId != 0)
                {
                    AMEmpleados empleado = new AMEmpleados();
                    empleado.FormClosed += new System.Windows.Forms.FormClosedEventHandler(Empleado_FormClosed);

                    empleado.SelectedEmployeeId = selectedRowId;
                    empleado.ShowDialog();
                
                }
                else
                {
                    MessageBox.Show("No se ha seleccionado ningún empleado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            if (selectedRowId != 0)
            {

                DialogResult result = MessageBox.Show("¿Estás seguro de que deseas eliminar este empleado?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        EmpleadoNegocio.DeleteEmpleado(selectedRowId);
                        MessageBox.Show("El empleado se eliminó correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Se produjo un error al eliminar el empleado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
            else
            {
                MessageBox.Show("No se ha seleccionado ningún empleado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
            CargarEmpleados();
        }

        private void btnNuevo_Click_1(object sender, EventArgs e)
        {
            AMEmpleados empleado = new AMEmpleados();
            empleado.FormClosed += new System.Windows.Forms.FormClosedEventHandler(Empleado_FormClosed);

            empleado.ShowDialog();
        }

       
    }
}
