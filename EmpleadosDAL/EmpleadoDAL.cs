using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmpleadosEntidad;
using System.Configuration;

namespace EmpleadosDAL
{
    public class EmpleadoDAL
    {
    public static DataTable ListarEmpleados(string NombreCompleto)
        {
            DataTable table = new DataTable();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Cnn"].ToString()))
            {
                conn.Open();

                string sql = @"ListarEmpleados";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@NombreCompleto", SqlDbType.VarChar).Value = NombreCompleto;

                SqlDataReader reader = cmd.ExecuteReader();

                // Agregar las columnas al DataTable
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    table.Columns.Add(reader.GetName(i));
                }

                // Agregar los datos al DataTable
                while (reader.Read())
                {
                    DataRow row = table.NewRow();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        row[i] = reader[i];
                    }
                    table.Rows.Add(row);
                }
            }

            return table;
        }
    

    public static Empleado ObtenerEmpleado(SqlDataReader reader)
    {
        Empleado empleado = new Empleado();

        empleado.Id = Convert.ToInt32(reader["Id"]);
        empleado.NombreCompleto = Convert.ToString(reader["NombreCompleto"]);
        empleado.DNI = Convert.ToString(reader["DNI"]);
        empleado.Edad = Convert.ToInt32(reader["Edad"]);
        empleado.Casado = Convert.ToBoolean(reader["Casado"]);
        empleado.Salario = Convert.ToDouble(reader["Salario"]);


        return empleado;
    }

    public static Empleado AltaEmpleado(Empleado empleado)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Cnn"].ToString()))
            {
                conn.Open();
                string sql = string.Empty;

                sql = @"AltaEmpleado";


                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                if (empleado.Id > 0)
                {
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = empleado.Id;
                }

                cmd.Parameters.Add("@NombreCompleto", SqlDbType.VarChar).Value = empleado.NombreCompleto;
                cmd.Parameters.Add("@DNI", SqlDbType.VarChar).Value = empleado.DNI;
                cmd.Parameters.Add("@Edad", SqlDbType.VarChar).Value = empleado.Edad;
                cmd.Parameters.Add("@Casado", SqlDbType.Bit).Value = empleado.Casado;
                cmd.Parameters.Add("@Salario", SqlDbType.Float).Value = empleado.Salario;

                cmd.ExecuteNonQuery();

                return empleado;
            }
        }
        catch (Exception ex)
        {
            // Manejo de la excepción
            Console.WriteLine("Error en AltaEmpleado: " + ex.Message);
            throw; // Relanza la excepción para propagarla adecuadamente
        }
    }

        public static Empleado ModificaEmpleado(Empleado empleado)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Cnn"].ToString()))
                {
                    conn.Open();
                    string sql = @"ModificarEmpleado";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (empleado.Id > 0)
                    {
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = empleado.Id;
                    }

                    cmd.Parameters.Add("@NombreCompleto", SqlDbType.VarChar).Value = empleado.NombreCompleto;
                    cmd.Parameters.Add("@DNI", SqlDbType.VarChar).Value = empleado.DNI;
                    cmd.Parameters.Add("@Edad", SqlDbType.Int).Value = empleado.Edad;
                    cmd.Parameters.Add("@Casado", SqlDbType.Bit).Value = empleado.Casado;
                    cmd.Parameters.Add("@Salario", SqlDbType.Float).Value = empleado.Salario;

                    cmd.ExecuteNonQuery();

                    return empleado;
                }
            }
            catch (Exception ex)
            {
                // Manejo de la excepción
                Console.WriteLine("Error en ModificarEmpleado: " + ex.Message);
                
                throw;
            }
        }

        public static Empleado ObtenerEmpleado(int Id)
    {

        Empleado empleado = new Empleado();

        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Cnn"].ToString()))
        {
            conn.Open();

            string sql = @"ObtenerEmpleados";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id", SqlDbType.VarChar).Value = Id;

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                empleado = ObtenerEmpleado(reader);
            }
        }

        return empleado;


    }

    public static void DeleteEmpleado(int id)
        {
        try
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Cnn"].ToString()))
                {
                    conn.Open();
                    string sql = @"EliminarEmpleado";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                    cmd.ExecuteNonQuery();

                }



            }
            catch (Exception Ex)
            {
                throw new Exception("Se produjo un error al eliminar el empleado: " + Ex.Message);
            }
        }
    }
}

