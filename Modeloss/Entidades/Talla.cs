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
    public class Talla
    {
        private int idTalla;
        private int idZapato;
        private string tallaZapato;
        private int cantidad;

        public int IdTalla { get => idTalla; set => idTalla = value; }
        public int IdZapato { get => idZapato; set => idZapato = value; }
        public string TallaZapato { get => tallaZapato; set => tallaZapato = value; }
        public int Cantidad { get => cantidad; set => cantidad = value; }

        public static DataTable cargarTalla()
        {
            SqlConnection conexion = ConexionDB.Conectar();
            string consultaQuery = "SELECT * FROM Talla";
            SqlDataAdapter add = new SqlDataAdapter(consultaQuery, conexion);
            DataTable tablaCarga = new DataTable();
            add.Fill(tablaCarga);
            return tablaCarga;
        }
    }
}
