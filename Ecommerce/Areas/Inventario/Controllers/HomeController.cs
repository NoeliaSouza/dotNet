using Ecommerce.AccesoDatos.Repositorio.IRepositorio;
using Ecommerce.Modelos;
using Ecommerce.Modelos.Especificaciones;
using Ecommerce.Modelos.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Ecommerce.Areas.Inventario.Controllers
{
    [Area("Inventario")]

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnidadTrabajo _unidadTrabajo;

        public HomeController(ILogger<HomeController> logger, IUnidadTrabajo unidadTrabajo)
        {
            _logger = logger;
            _unidadTrabajo = unidadTrabajo;
        }

        public IActionResult Index(int numeroPagina=1, string busqueda="", string busquedaActual="")
        {

            if (!String.IsNullOrEmpty(busqueda))
            {
                numeroPagina = 1;
            }
            else
            {
                busqueda = busquedaActual;
            }
            ViewData["BusquedaActual"] = busqueda;
            
            if (numeroPagina < 1) { numeroPagina = 1; }

            Parametros parametros = new Parametros()
            {
                NumeroPagina=numeroPagina,
                TamañoPagina=4
            };
            var resultado = _unidadTrabajo.Producto.ObtenerTodosPaginado(parametros);

            //Agregamos filtro
            if (!String.IsNullOrEmpty(busqueda))
            {
                resultado = _unidadTrabajo.Producto.ObtenerTodosPaginado(parametros, p => p.Descripcion.Contains(busqueda));
            }

            ViewData["TotalPaginas"] = resultado.MetaData.PaginasTotales;
            ViewData["TotalRegistros"] = resultado.MetaData.TotalCount;
            ViewData["TamañoPagina"] = resultado.MetaData.TamañoPagina;
            ViewData["NumeroPagina"] = numeroPagina;
            ViewData["Previo"] = "disabled";
            ViewData["Siguiente"] = "";

            if (numeroPagina > 1) { ViewData["Previo"] = ""; }
            if (resultado.MetaData.PaginasTotales <= numeroPagina) { ViewData["Siguiente"] = "disabled"; }

            return View(resultado);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}