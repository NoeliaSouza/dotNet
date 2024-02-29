using Ecommerce.AccesoDatos.Data;
using Ecommerce.AccesoDatos.Repositorio;
using Ecommerce.AccesoDatos.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
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
        #endregion
    }

}
