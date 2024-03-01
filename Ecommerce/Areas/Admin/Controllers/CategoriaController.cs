using Ecommerce.AccesoDatos.Repositorio.IRepositorio;
using Ecommerce.Modelos;
using Ecommerce.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = DS.RolAdmin)]
    public class CategoriaController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        public CategoriaController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Categoria categoria = new Categoria();

            if (id == null)
            {   //Creamos
                categoria.Estado = true;
                return View(categoria);
            }
            //Actualizamos
            categoria = await _unidadTrabajo.Categoria.Obtener(id.GetValueOrDefault());
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                if (categoria.Id == 0)
                {
                    await _unidadTrabajo.Categoria.Agregar(categoria);
                    TempData[DS.Exitosa] = "Categoria creada exitosamente";
                }
                else
                {
                    _unidadTrabajo.Categoria.Actualizar(categoria);
                    TempData[DS.Exitosa] = "Categoria actualizada exitosamente";
                }
                await _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            TempData[DS.Error] = "No se pudo realizar la operación";
            return View(categoria);
        }




        #region API

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var categoria = await _unidadTrabajo.Categoria.Obtener(id);
            if (categoria == null)
            {
                return Json(new { success = false, message = "Error al eliminar Categoria" });
            }
            _unidadTrabajo.Categoria.Remover(categoria);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Categoria eliminada exitosamente" });
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Categoria.ObtenerTodos();
            return Json(new { data = todos });
        }

        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string nombre, int id = 0)
        {
            bool valor = false;
            var lista = await _unidadTrabajo.Categoria.ObtenerTodos();

            if (!nombre.IsNullOrEmpty())
            {
                if (id == 0)
                {
                    valor = lista.Any(b => b.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
                }
                else
                {
                    valor = lista.Any(b => b.Nombre.ToLower().Trim() == nombre.ToLower().Trim()
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
