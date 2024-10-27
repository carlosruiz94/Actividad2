using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;//Clase necesaria para la conexión de base de datos
using System.Data.SqlClient;
using System.Web; //Clase necesaria para conectar a SQLServer

namespace ISNP142324ISNP094824_Bloque2
{
    internal class db_conexion
    {
        SqlConnection miConexion = new SqlConnection();//Para conectarnos a la base de datos
        SqlCommand miComando = new SqlCommand();//para ejecutar comandos dentro de la base de datos
        SqlDataAdapter miAdaptador = new SqlDataAdapter();//Traductor entre base de datos y aplicación
        DataSet ds = new DataSet();//Arquitectura de la base de datos en memoria RAM

        public db_conexion()
        {
            miConexion.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\db_peliculas.mdf;Integrated Security=True";
            miConexion.Open();//Apertura de Conexión
        }
        public DataSet obtenerdatos()
        {
            ds.Clear();//limpieza del dataset
            miComando.Connection = miConexion;//asignamos la conexión al comando para ejecutar las consultas
            miComando.CommandText = "SELECT * FROM peliculas";//Consulta los datos de la tabla
            miAdaptador.SelectCommand = miComando;//Asignamos el comando al adaptador
            miAdaptador.Fill(ds, "peliculas");
            return ds;
        }
        public String administrarPeliculas(String[] peliculas)
        {
            String sql = "";
            if (peliculas[0] == "nuevo")
            {
                sql = "INSERT INTO peliculas(titulo, autor, sinopsis, duración, clasificación) VALUES(" +
                    "'" + peliculas[2] + "'," +
                    "'" + peliculas[3] + "'," +
                    "'" + peliculas[4] + "'," +
                    "'" + peliculas[5] + "'," +
                    "'" + peliculas[6] + "')";
            }
            else if (peliculas[0] == "modificar")
            {
                sql = "UPDATE peliculas SET titulo='" + peliculas[2] + "', autor='" + peliculas[3] + "', " +
                    "sinopsis='" + peliculas[4] + "', duración='" + peliculas[5] + "', clasificación='" + peliculas[6] + "' WHERE idPelicula=" + peliculas[1];

            }
            else if (peliculas[0] == "eliminar")
            {
                sql = "DELETE FROM peliculas WHERE idPelicula='" + peliculas[1] + "'";
            }
            return ejecutarSQL(sql);
        }
        private string ejecutarSQL(String sql)
        {
            try
            {
                miComando.Connection = miConexion;
                miComando.CommandText = sql;
                return miComando.ExecuteNonQuery().ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
