using Ecommerce.AccesoDatos.Repositorio.IRepositorio;
using Ecommerce.Modelos;
using Ecommerce.Utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductoController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        public ProductoController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            
            return View();
        }






        #region API

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Producto.ObtenerTodos(incluirPropiedades:"Categoria,Marca");
            return Json(new { data = todos });
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var producto = await _unidadTrabajo.Producto.Obtener(id);
            if (producto == null)
            {
                return Json(new { success = false, message = "Error al eliminar Producto" });
            }
            _unidadTrabajo.Producto.Remover(producto);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Producto eliminada exitosamente" });
        }


        [ActionName("ValidarCodigo")]
        public async Task<IActionResult> ValidarCodigo(string codigo, int id = 0)
        {
            bool valor = false;
            var lista = await _unidadTrabajo.Producto.ObtenerTodos();

            if (!codigo.IsNullOrEmpty())
            {
                if (id == 0)
                {
                    valor = lista.Any(b => b.Codigo.ToLower().Trim() == codigo.ToLower().Trim());
                }
                else
                {
                    valor = lista.Any(b => b.Codigo.ToLower().Trim() == codigo.ToLower().Trim()
                    && b.Id != id);

                }
                if (valor)
                {
                    return Json(new { data = true });
                }
            }
                
            return Json(new { data = false });
        }


        #endregion

    }
}
