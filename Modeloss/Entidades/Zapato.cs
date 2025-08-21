using Modelos.Conexion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;

namespace Modelos.Entidades
{
    public class Zapato
    {
        private int zapatoId;
        private int categoriaId;
        private string nombre;
        private double precio;
        private string imagenURL;
        private DateTime fechaCreacion;

        public int IdZapato { get => zapatoId; set => zapatoId = value; }

        public string Nombre { get => nombre; set => nombre = value; }
        public double Precio { get => precio; set => precio = value; }
        public string ImagenURL { get => imagenURL; set => imagenURL = value; }
        public DateTime FechaCreacion { get => fechaCreacion; set => fechaCreacion = value; }
        public int CategoriaId { get => categoriaId; set => categoriaId = value; }

        public static DataTable cargarZapato()
        {
            try
            {
                SqlConnection conexion = ConexionDB.Conectar();
                string consultaQuery = "SELECT * FROM vistaCategoria";
                SqlCommand comando = new SqlCommand(consultaQuery, conexion);
                SqlDataAdapter add = new SqlDataAdapter(consultaQuery, conexion);
                DataTable tablaVirtual = new DataTable();
                add.Fill(tablaVirtual);
                return tablaVirtual;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los datos" + ex);
                return null;
            }


        }

        public bool insertarZapato()
        {
            try
            {
                SqlConnection conexion = ConexionDB.Conectar();
                string consultaQuery = "INSERT INTO Zapatos (CategoriaId, Nombre, Precio, ImagenURL, FechaCreacion) VALUES (@CategoriaId, @Nombre, @Precio, @ImagenURL, @FechaCreacion);";
                SqlCommand insertar = new SqlCommand(consultaQuery, conexion);

                insertar.Parameters.AddWithValue("@CategoriaId", categoriaId);
                insertar.Parameters.AddWithValue("@Nombre", nombre);
                insertar.Parameters.AddWithValue("@Precio", precio);
                insertar.Parameters.AddWithValue("@ImagenURL", imagenURL);
                insertar.Parameters.AddWithValue("@FechaCreacion", fechaCreacion);
                insertar.ExecuteNonQuery();
                MessageBox.Show("Zapato registrado exitosamente.", "Registro Completado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Verifica si la consulta de insertar esta correcta " + ex, "Error");
                return false;
            }
        }
        public bool actualizarZapato()
        {

            try
            {
                SqlConnection conexion = ConexionDB.Conectar();
                string consultaUpdate = "update zapatos set CategoriaId=@CategoriaId,nombre=@Nombre,precio=@Precio,ImagenURL=@imagen,FechaCreacion=@fecha where Id=@Idzapato";
                SqlCommand update = new SqlCommand(consultaUpdate, conexion);

                update.Parameters.AddWithValue("@CategoriaId", categoriaId);
                update.Parameters.AddWithValue("@Nombre", nombre);
                update.Parameters.AddWithValue("@Precio", precio);
                update.Parameters.AddWithValue("@imagen", imagenURL);
                update.Parameters.AddWithValue("@fecha", fechaCreacion);
                update.Parameters.AddWithValue("@Idzapato", zapatoId);

                update.ExecuteNonQuery();
                MessageBox.Show("Zapato actualizado exitosamente.", "Actualización Completada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Verifica si la consulta de actualizar esta correcta " + ex, "Error");
                return false;
            }
        }

        public bool eliminarZapato(int id)
        {
            try
            {
                SqlConnection conexion = ConexionDB.Conectar();
                string consultaDelete = "DELETE FROM Zapatos WHERE Id = @id;";
                SqlCommand delete = new SqlCommand(consultaDelete, conexion);
                delete.Parameters.AddWithValue("@id", id);
                delete.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool buscarZapato(int id)
        {
            try
            {
                SqlConnection conexion = ConexionDB.Conectar();
                string consultaQuery = "SELECT * FROM Zapatos WHERE Id = @id;";
                SqlCommand buscar = new SqlCommand(consultaQuery, conexion);
                buscar.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = buscar.ExecuteReader();

                if (reader.Read())
                {
                    zapatoId = Convert.ToInt32(reader["Id"]);
                    categoriaId = Convert.ToInt32(reader["CategoriaId"]);
                    nombre = reader["Nombre"].ToString();
                    precio = Convert.ToDouble(reader["Precio"]);
                    imagenURL = reader["ImagenURL"].ToString();
                    fechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]);
                    return true;
                }
                else
                {
                    MessageBox.Show("Zapato no encontrado.", "Búsqueda Fallida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar el zapato: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public DataTable buscarZapatoPorId(int id)
        {
            try
            {
                SqlConnection conexion = ConexionDB.Conectar();
                string consultaQuery = "SELECT * FROM Zapatos WHERE Id = @id;";
                SqlCommand buscar = new SqlCommand(consultaQuery, conexion);
                buscar.Parameters.AddWithValue("@id", id);
                SqlDataAdapter add = new SqlDataAdapter(buscar);
                DataTable tablaCarga = new DataTable();
                add.Fill(tablaCarga);
                return tablaCarga;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar el zapato: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }

}