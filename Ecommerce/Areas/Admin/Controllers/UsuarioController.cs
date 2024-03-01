using Ecommerce.AccesoDatos.Data;
using Ecommerce.AccesoDatos.Repositorio;
using Ecommerce.AccesoDatos.Repositorio.IRepositorio;
using Ecommerce.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = DS.RolAdmin)]
    public class UsuarioController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly ApplicationDbContext _db;

        public UsuarioController(IUnidadTrabajo unidadTrabajo,
            ApplicationDbContext db)
        {
            _unidadTrabajo = unidadTrabajo;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
        #region API
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var usuarioLista = await _unidadTrabajo.Usuario.ObtenerTodos();
            var usuarioRol = await _db.UserRoles.ToListAsync();
            var roles = await _db.Roles.ToListAsync();
            foreach(var usuario in usuarioLista)
            {
                var idRol = usuarioRol.FirstOrDefault(u => u.UserId == usuario.Id).RoleId;
                usuario.Rol = roles.FirstOrDefault(r => r.Id == idRol).Name;
            }
            return Json(new { data = usuarioLista });
        }
        [HttpPost]
        public async Task<IActionResult> BloquearDesbloquear([FromBody] string id)
        {
            var usuario = await _unidadTrabajo.Usuario.ObtenerPrimero(u => u.Id == id);
            if (usuario == null)
            {
                return Json(new { success = false, message = "Error de usuario" });
            }
            if(usuario.LockoutEnd !=null && usuario.LockoutEnd > DateTime.Now)
            {
                //Bloqueado
                usuario.LockoutEnd= DateTime.Now;
            }
            else
            {
                usuario.LockoutEnd = DateTime.Now.AddYears(100);
            }
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Operacion exitosa" });
        }


        #endregion
    }

}
