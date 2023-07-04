using EmpleadosEntidad;
using EmpleadosDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpleadosNegocio
{
    public class EmpleadoNegocio
    {
        public static DataTable ListarEmpleados(string NombreCompleto)
        {
            return EmpleadoDAL.ListarEmpleados(NombreCompleto);
        }

        public static Empleado AltaEmpleado(Empleado empleado)
        {
            return EmpleadoDAL.AltaEmpleado(empleado);
        }
        
        public static Empleado ObtenerEmpleado(int Id)
        {
            return EmpleadoDAL.ObtenerEmpleado(Id);
        }

        public static Empleado ModificaEmpleado(Empleado empleado)
        {
            return EmpleadoDAL.ModificaEmpleado(empleado);
        }

        public static void DeleteEmpleado(int id)
        {
            EmpleadoDAL.DeleteEmpleado(id);
        }


    }
}
