using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web;
using System.Web.Mvc;
using WebProductos.Datos;
using WebProductos.Models;

namespace WebProductos.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        public ActionResult Index()
        {
            //Crear un objeto de la capa de datos
            D_Producto datos = new D_Producto();
            //Crear una lista de productos y la obtengo del metodo de la capa de datos
            List<E_Producto> lista = datos.ObtenerProductos();
            //Pasamos la lista como modelo a la vista
            return View("Principal", lista);
        }

        public ActionResult IrAgregar()
        {
            return View("VistaAgregar");
        }

        public ActionResult Agregar(E_Producto objProducto)
        {
            //Crear un objeto de la capa de datos
            D_Producto datos = new D_Producto();
            //Mando a llamar el metodo que ejecuta el query INSERT
            datos.AgregarProducto(objProducto);

            TempData["mensaje"] = $"El producto {objProducto.Descripcion} se registro correctamente";

            //Redirijimos a la accion Index
            return RedirectToAction("Index");
        }

        public ActionResult IrEditar(int idProducto)
        {
            //Crear un objeto de la capa de datos
            D_Producto datos = new D_Producto();

            //Crear un Producto y lo obtengo de la capa de datos
            E_Producto producto = datos.ObtenerProductoPorId(idProducto);
            //Pasar el producto como modelo a la vista
            return View("VistaEditar", producto);
        }

        public ActionResult Editar(E_Producto producto)
        {
            //Crear un objeto de la capa de datos
            D_Producto datos = new D_Producto();
            //Mandamos a llamar el metodo EditarProducto de la capa de datos
            datos.EditarProducto(producto);

            TempData["mensaje"] = $"El producto con ID:{producto.IdProducto} se modifico correctamente";

            return RedirectToAction("Index");
        }

        public ActionResult Eliminar(int idProducto)
        {
            //Crear un objeto de la capa de datos
            D_Producto datos = new D_Producto();

            datos.EliminarProducto(idProducto);

            TempData["mensaje"] = $"El producto con ID:{idProducto} se elimino correctamente";

            return RedirectToAction("Index");
        }
    }
}