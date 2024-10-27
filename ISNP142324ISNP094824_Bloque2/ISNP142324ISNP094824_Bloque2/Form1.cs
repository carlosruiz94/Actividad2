using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISNP142324ISNP094824_Bloque2
{
    public partial class Form1 : Form
    {
        db_conexion objConexion = new db_conexion();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        public int posicion = 0;
        string accion = "nuevo";


        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtAutor_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            obtenerDatos();
        }

        private void obtenerDatos()
        {
            ds = objConexion.obtenerdatos();
            dt = ds.Tables["peliculas"];
            dt.PrimaryKey = new DataColumn[] { dt.Columns["idPelicula"] };
            mostrarDatos();
        }
        private void mostrarDatos()
        {
            txtTitulo.Text = dt.Rows[posicion].ItemArray[1].ToString();
            txtAutor.Text = dt.Rows[posicion].ItemArray[2].ToString();
            txtSinopsis.Text = dt.Rows[posicion].ItemArray[3].ToString();
            txtDuracion.Text = dt.Rows[posicion].ItemArray[4].ToString();
            txtClasificacion.Text = dt.Rows[posicion].ItemArray[5].ToString();
            lblRegistro.Text = (posicion + 1) + " de " + dt.Rows.Count;
        }
        private void limpiarCajas()
        {
            txtTitulo.Text = "";
            txtAutor.Text = "";
            txtSinopsis.Text = "";
            txtDuracion.Text = "";
            txtClasificacion.Text = "";
        }
        private void habdescontroles(Boolean estado)
        {
            grbNavegacion.Enabled = !estado;
            btnEliminar.Enabled = !estado;
        }
        private void txtClasificacion_Click(object sender, EventArgs e)
        {

        }

        private void grbDatos_Enter(object sender, EventArgs e)
        {

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (btnModificar.Text == "Modificar")//funcionamiento de boton modificar
            {
                btnModificar.Text = "Cancelar";
                btnNuevo.Text = "Guardar";
                accion = "modificar";
                habdescontroles(true);
            }
            else//cancelar de boton modificar
            {
                mostrarDatos();
                habdescontroles(false);
                btnModificar.Text = "Modificar";
                btnNuevo.Text = "Nuevo";
            }

        }

        private void txtSinopsis_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtClasificacion_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (posicion < dt.Rows.Count - 1)
            {

                posicion += 1;
                mostrarDatos();
                btnAnterior.Enabled = true;
                btnPrimero.Enabled = true;
            }
            else
            {
                btnSiguiente.Enabled = false;
                btnUltimo.Enabled = false;


            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {

        }

        private void btnPrimero_Click(object sender, EventArgs e)
        {
            posicion = 0;
            mostrarDatos();

            btnSiguiente.Enabled = true;
            btnUltimo.Enabled = true;
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (posicion > 0)
            {
                posicion -= 1;
                mostrarDatos();
                btnSiguiente.Enabled = true;
                btnUltimo.Enabled = true;
            }
            else
            {
                btnAnterior.Enabled = false;
                btnPrimero.Enabled = false;
            }
        }

        private void btnUltimo_Click_1(object sender, EventArgs e)
        {
            posicion = dt.Rows.Count - 1;
            mostrarDatos();
            btnPrimero.Enabled = true;
            btnAnterior.Enabled = true;
        }

        private void btnAnterior_Click_1(object sender, EventArgs e)
        {
            if (posicion > 0)
            {
                posicion -= 1;
                mostrarDatos();
                btnSiguiente.Enabled = true;
                btnUltimo.Enabled = true;
            }
            else
            {
                btnAnterior.Enabled = false;
                btnPrimero.Enabled = false;
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (btnNuevo.Text == "Nuevo")
            {
                btnNuevo.Text = "Guardar";
                btnModificar.Text = "Cancelar";
                limpiarCajas();
                accion = "nuevo";
                habdescontroles(true);
            }
            else//guardar
            {
                string[] datos = {
                    accion,
                    dt.Rows[posicion].ItemArray[0].ToString(),//id pelicula
                    txtTitulo.Text,
                    txtAutor.Text,
                    txtSinopsis.Text,
                    txtDuracion.Text,
                    txtClasificacion.Text,
                };
                String response = objConexion.administrarPeliculas(datos);
                if (response != "1")
                {
                    MessageBox.Show("Error: " + response, "Registrando datos de pelicula", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else
                {
                    obtenerDatos();
                    habdescontroles(false);
                    btnNuevo.Text = "Nuevo";
                    btnModificar.Text = "Modificar";
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e){
            if (MessageBox.Show("Esta seguro de eliminar a: " + txtTitulo.Text, "Eliminando peliculas",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes){
                String[] datos = {
                    "eliminar", dt.Rows[posicion].ItemArray[0].ToString(),
                };
                String response = objConexion.administrarPeliculas(datos);
                if (response != "1")
                {
                    MessageBox.Show("Error: " + response, "Eliminando datos de pelicula", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                else
                {
                    obtenerDatos();
                }

            }
        }
    }
}