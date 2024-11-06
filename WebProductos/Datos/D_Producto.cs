using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebProductos.Models;

namespace WebProductos.Datos
{
    public class D_Producto
    {

        //Metodo para recuperar los productos de la BD
        public List<E_Producto> ObtenerProductos()
        {
            //Crear una lista de productos vacia
            List<E_Producto> productos = new List<E_Producto>();

            //Cadena de conexion
            string cadenaConexion = "server=localhost;database=generacion30;user=sa;password=devo123";
            //Crear el objeto para conectarme a la BD
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            //Abrir conexion
            conexion.Open();
            //Query a ejecutar
            string query = "SELECT idProducto,descripcion,precio,fechaIngreso,disponible FROM Productos";
            //Crear un objeto para ejectuar el query
            SqlCommand comando = new SqlCommand(query,conexion);
            //Crear un objeto para almacenar los resultados del query (SqlDataReader)
            //Ejecutar el query con el metodo ExecuteReader();
            SqlDataReader reader = comando.ExecuteReader();
            //Recorrer el conjunto de resultados
            while (reader.Read())
            {
                //Crear un objeto de la clase E_Producto
                E_Producto producto = new E_Producto();
                //Asignamos valores a las propiedades del objeto
                //Convertir al tipo de dato correspondiente;
                producto.IdProducto = Convert.ToInt32(reader["idProducto"]);
                producto.Descripcion = Convert.ToString(reader["descripcion"]);
                producto.Precio = Convert.ToDecimal(reader["precio"]);
                producto.FechaIngreso = Convert.ToDateTime(reader["fechaIngreso"]);
                producto.Disponible = Convert.ToBoolean(reader["disponible"]);

                //Agregar el objeto producto a la lista
                productos.Add(producto);
            }

            //Cerrar conexion
            conexion.Close();

            //Termino el metodo devolviendo la lista de productos
            return productos;
        }

        public void AgregarProducto(E_Producto producto)
        {
            //Cadena de conexion
            string cadenaConexion = "server=localhost;database=generacion30;user=sa;password=devo123";
            //Crear el objeto para conectarme a la BD
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            //Abrir conexion
            conexion.Open();
            //creamos el query a ejecutar utilizando parametros(@)
            string query = "INSERT INTO Productos(descripcion,precio,fechaIngreso,disponible) " +
                                        "VALUES(@descripcion,@precio,@fechaIngreso,@disponible)";

            //Objeto para ejecutar el query
            SqlCommand comando = new SqlCommand(query, conexion);

            //Antes de ejecutar el query hay que pasarle los valores a los parametros query
            comando.Parameters.AddWithValue("@descripcion", producto.Descripcion);
            comando.Parameters.AddWithValue("@precio", producto.Precio);
            comando.Parameters.AddWithValue("@fechaIngreso", producto.FechaIngreso);
            comando.Parameters.AddWithValue("@disponible", producto.Disponible);

            //Ejecutar el query
            comando.ExecuteNonQuery();

            //Cerrar la conexion
            conexion.Close();
        }

        public E_Producto ObtenerProductoPorId(int idProducto)
        {
            //Cadena de conexion
            string cadenaConexion = "server=localhost;database=generacion30;user=sa;password=devo123";
            //Crear el objeto para conectarme a la BD
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            //Abrir conexion
            conexion.Open();
            //Query a ejecutar
            string query = "SELECT idProducto,descripcion,precio,fechaIngreso,disponible " +
                            "FROM Productos WHERE idProducto = @idProducto";

            //Objeto para ejecutar el query
            SqlCommand comando = new SqlCommand(query, conexion);
            //Asignamos valor al parametro
            comando.Parameters.AddWithValue("@idProducto", idProducto);
            //Ejecutamos el query y guardamos el resultado en un SqlDataReader
            SqlDataReader reader = comando.ExecuteReader();

            //Creamos objeto Producto
            E_Producto producto = new E_Producto();

            if (reader.Read())
            {
                //Asignamos valores a las propiedades del objeto
                //Convertir al tipo de dato correspondiente;
                producto.IdProducto = Convert.ToInt32(reader["idProducto"]);
                producto.Descripcion = Convert.ToString(reader["descripcion"]);
                producto.Precio = Convert.ToDecimal(reader["precio"]);
                producto.FechaIngreso = Convert.ToDateTime(reader["fechaIngreso"]);
                producto.Disponible = Convert.ToBoolean(reader["disponible"]);
            }

            //Cerar la conexion
            conexion.Close();
            return producto;
        }

        public void EditarProducto(E_Producto producto)
        {
            //Cadena de conexion
            string cadenaConexion = "server=localhost;database=generacion30;user=sa;password=devo123";
            //Crear el objeto para conectarme a la BD
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            //Abrir conexion
            conexion.Open();
            //query a ejecutar
            string query = "UPDATE Productos SET descripcion=@descripcion,precio=@precio, " +
                "fechaIngreso=@fechaIngreso, disponible=@disponible WHERE idProducto = @idProducto";
            //Objeto para ejecutar el query
            SqlCommand comando = new SqlCommand(query, conexion);
            //Asignamos valores a los parametros del query
            comando.Parameters.AddWithValue("@descripcion", producto.Descripcion);
            comando.Parameters.AddWithValue("@precio", producto.Precio);
            comando.Parameters.AddWithValue("@fechaIngreso", producto.FechaIngreso);
            comando.Parameters.AddWithValue("@disponible", producto.Disponible);
            comando.Parameters.AddWithValue("@idProducto", producto.IdProducto);
            //Ejecutamos el query
            comando.ExecuteNonQuery();

            //Cerrar conexion
            conexion.Close();
        }

        public void EliminarProducto(int idProducto)
        {
            string cadenaConexion = "server=localhost;database=generacion30;user=sa;password=devo123";
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            conexion.Open();

            string query = "DELETE Productos WHERE idProducto = @idProducto";

            SqlCommand comando = new SqlCommand(query, conexion);

            comando.Parameters.AddWithValue("@idProducto", idProducto);

            comando.ExecuteNonQuery();

            conexion.Close();
        }

    }
}