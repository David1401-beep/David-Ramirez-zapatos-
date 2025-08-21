using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace Modelos.Conexion
{
    public class ConexionDB
    {
        private static string servidor = "LAB03-DS-EQ11\\SQLEXPRESS";
        private static string dbData = "ZapatosDB";

        public static SqlConnection Conectar()
        {
            try
            {
                string cadena = $"Data Source={servidor};Initial Catalog={dbData};Integrated Security=True";
                SqlConnection conexion = new SqlConnection(cadena);
                conexion.Open();
                return conexion;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al conectar a la base de datos: {ex.Message}", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}
