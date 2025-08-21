using Modelos.Conexion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.ComponentModel;

namespace Modelos.Entidades
{
    public class Categoria
    {
        private int idCategoria;
        private string nombreCategoria;

        public int IdCategoria { get => idCategoria; set => idCategoria = value; }
        public string NombreCategoria { get => nombreCategoria; set => nombreCategoria = value; }

        public static DataTable cargarCategoria()
        {
            SqlConnection conexion = ConexionDB.Conectar();
            string consultaQuery = "SELECT Id, Nombre FROM Categorias";
            SqlDataAdapter add = new SqlDataAdapter(consultaQuery, conexion);
            DataTable tablaCarga = new DataTable();
            add.Fill(tablaCarga);
            return tablaCarga;
        }
    }
}
