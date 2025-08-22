using Modelos.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace frmMantenimientoZapatos
{
    public partial class frmMantenimientoZapatos : Form
    {
        public frmMantenimientoZapatos()
        {
            InitializeComponent();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void frmMantenimientoZapatos_Load(object sender, EventArgs e)
        {
            caragarEspecialidad();
            cargarZapatos();
        }

        private void cargarZapatos()
        {


            dgvProductos1.DataSource = null;
            dgvProductos1.DataSource = Zapato.cargarZapato();


            dtgProductos2.DataSource = null;
            dtgProductos2.DataSource = Zapato.cargarZapato();
        }


        private void caragarEspecialidad()
        {
           

            cmbCategoria.DataSource = null;
            cmbCategoria.DataSource = Categoria.cargarCategoria();



            cmbCategoria.DisplayMember = "Nombre";
            cmbCategoria.ValueMember = "Id";
            cmbCategoria.SelectedIndex = -1; 

            

            cmbCategoria2.DataSource = null;
            cmbCategoria2.DataSource = Categoria.cargarCategoria();

            

            cmbCategoria2.DisplayMember = "Nombre";
            cmbCategoria2.ValueMember = "Id";
            cmbCategoria2.SelectedIndex = -1; 
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                Zapato zapato = new Zapato();
                zapato.Nombre = txtNombreZapato.Text;
                zapato.Precio = double.Parse(txtPrecio.Text);
                zapato.FechaCreacion = dtpFechaRegistro.Value;
                zapato.CategoriaId = Convert.ToInt32(cmbCategoria.SelectedValue);
                zapato.ImagenURL = "";

                zapato.insertarZapato();
                cargarZapatos();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar el zapato: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            bool resultado = false;
            string connectionString = "Data Source=LAB03-DS-EQ11\\SQLEXPRESS;Initial Catalog=ZapatosDB;Integrated Security=True;";
            int idZapatoAEliminar = 1; 

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Zapatos WHERE IdZapato = @IdZapato"; 
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdZapato", idZapatoAEliminar);
                    int filas = cmd.ExecuteNonQuery();
                    if (filas > 0)
                    {
                        resultado = true;
                    }
                }
            }
           
        }





        private void btnActualizar_Click(object sender, EventArgs e)
        {
            
        
            // 1. Get the values from the form's controls
            int idZapato = int.Parse(txtNombreZapato.Text); // Assuming you have a hidden or visible textbox for the ID
            string nombre = txtNombreZapato2.Text;
            decimal precio = decimal.Parse(txtPrecio.Text);
            DateTime fechaRegistro = dtpFechaRegistro.Value; // Assuming this is a DateTimePicker control
            int idCategoria = (int)cmbCategoria.SelectedValue; // Assuming this is a ComboBox with the category ID

            bool resultado = false;
            string connectionString = "Data Source=LAB03-DS-EQ11\\SQLEXPRESS;Initial Catalog=ZapatosDB;Integrated Security=True;";

            // 2. SQL UPDATE query
            string query = "UPDATE Zapatos SET Nombre = @Nombre, Precio = @Precio, FechaRegistro = @FechaRegistro, Categoria = @Categoria WHERE IdZapato = @IdZapato";

            // 3. Use 'using' blocks for proper resource management
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // 4. Add parameters to the command
                        cmd.Parameters.AddWithValue("@IdZapato", idZapato);
                        cmd.Parameters.AddWithValue("@Nombre", nombre);
                        cmd.Parameters.AddWithValue("@Precio", precio);
                        cmd.Parameters.AddWithValue("@FechaRegistro", fechaRegistro);
                        cmd.Parameters.AddWithValue("@Categoria", idCategoria);

                        // 5. Execute the query
                        int filas = cmd.ExecuteNonQuery();

                        if (filas > 0)
                        {
                            resultado = true;
                            MessageBox.Show("Registro actualizado correctamente.");
                        }
                        else
                        {
                            MessageBox.Show("No se encontró el registro para actualizar o no se realizaron cambios.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar el registro: " + ex.Message);
                }
            }
        }






        

        private void cmbCategoria2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtPrecio2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNombreZapato2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtpFechaRegistro2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtgProductos2_DoubleClick(object sender, EventArgs e)
        {
            txtNombreZapato2.Text = dtgProductos2.CurrentRow.Cells[1].Value.ToString();
            txtPrecio2.Text = dtgProductos2.CurrentRow.Cells[2].Value.ToString();
            //dtpFechaRegistro2.Value = DateTime.Parse(dtgProductos2.CurrentRow.Cells[3].Value.ToString());

        }

        private void dtgProductos2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtBuscar.Text))
                {
                    // Si el campo de búsqueda está vacío, recargar todos los zapatos
                    cargarZapatos();
                }
                else
                {
                    // Filtrar los zapatos por el texto ingresado
                    int id;
                    if (int.TryParse(txtBuscar.Text.Trim(), out id))
                    {
                        Zapato zapato = new Zapato();
                        DataTable dt = zapato.buscarZapatoPorId(id);
                        if (dt.Rows.Count > 0)
                        {
                            dtgProductos2.DataSource = dt;
                        }
                        else
                        {
                            MessageBox.Show("No se encontraron zapatos con ese ID.", "Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cargarZapatos(); // Recargar todos los zapatos si no se encuentra ninguno
                        }
                    }
                    else
                    {
                        MessageBox.Show("Por favor, ingrese un ID válido.", "Error de Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cargarZapatos(); // Recargar todos los zapatos si el ID no es válido
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            
                try
                {
                    Zapato zapato = new Zapato();
                    int id = int.Parse(txtBuscar.Text.Trim());

                    if (zapato.buscarZapato(id))
                    {
                        txtNombreZapato2.Text = zapato.Nombre;
                        txtPrecio2.Text = zapato.Precio.ToString();
                        dtpFechaRegistro2.Value = zapato.FechaCreacion;
                        cmbCategoria2.SelectedValue = zapato.CategoriaId;
                    }
                    else
                    {
                        MessageBox.Show("Zapato no encontrado.", "Búsqueda Fallida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al buscar el zapato: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            

        }
    }
}