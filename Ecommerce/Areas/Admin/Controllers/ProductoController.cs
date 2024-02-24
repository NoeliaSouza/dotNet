using Ecommerce.AccesoDatos.Repositorio.IRepositorio;
using Ecommerce.Modelos;
using Ecommerce.Modelos.ViewModels;
using Ecommerce.Utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductoController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductoController(IUnidadTrabajo unidadTrabajo, IWebHostEnvironment webHostEnvironment)
        {
            _unidadTrabajo = unidadTrabajo;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            ProductoVM productoVM = new ProductoVM()
            {
                Producto = new Producto(),
                CategoriaLista = _unidadTrabajo.Producto.ObtenerDDL("Categoria"),
                MarcaLista = _unidadTrabajo.Producto.ObtenerDDL("Marca"),
                PadreLista = _unidadTrabajo.Producto.ObtenerDDL("Producto")
            };

            if (id == null)
            {
                productoVM.Producto.Estado = true;
                return View(productoVM);
            }
            else
            {
                productoVM.Producto = await _unidadTrabajo.Producto.Obtener(id.GetValueOrDefault());
                if (productoVM.Producto == null)
                {
                    return NotFound();
                }
                return View(productoVM);
            }


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ProductoVM productoVM)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;
                if (productoVM.Producto.Id == 0)
                {   //Creamos
                    string upload = webRootPath + DS.ImagenRuta;
                    string fileName=Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension),
                        FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    productoVM.Producto.ImagenUrl = fileName + extension;
                    await _unidadTrabajo.Producto.Agregar(productoVM.Producto);
                }
                else
                {
                    //Actualizamos
                    var objProducto = await _unidadTrabajo.Producto.ObtenerPrimero(p => p.Id == productoVM.Producto.Id,
                        isTracking:false);
                    //Si se carga nueva imagen
                    if (files.Count>0)
                    {
                        string upload = webRootPath + DS.ImagenRuta;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);
                        //Borramos la anterior
                        var anteriorFile=Path.Combine(upload, objProducto.ImagenUrl);
                        if (System.IO.File.Exists(anteriorFile))
                        {
                            System.IO.File.Delete(anteriorFile);
                        }
                        //Cargamos la nueva
                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension),
                            FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }
                        productoVM.Producto.ImagenUrl = fileName + extension;
                    }
                    //No se carga imagen nueva
                    else
                    {
                        productoVM.Producto.ImagenUrl=objProducto.ImagenUrl;
                    }
                    //Actualizamos
                    _unidadTrabajo.Producto.Actualizar(productoVM.Producto);
                }
                TempData[DS.Exitosa] = "Transacción Exitosa!";
                await _unidadTrabajo.Guardar();
                return View("Index");

            }
            //Si no es valido
            productoVM.CategoriaLista = _unidadTrabajo.Producto.ObtenerDDL("Categoria");
            productoVM.MarcaLista = _unidadTrabajo.Producto.ObtenerDDL("Marca");
            productoVM.PadreLista = _unidadTrabajo.Producto.ObtenerDDL("Producto");
            return View(productoVM);
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
            //Eliminamos imagen 
            string upload = _webHostEnvironment.WebRootPath + DS.ImagenRuta;
            var anteriorFile = Path.Combine(upload, producto.ImagenUrl);
            if (System.IO.File.Exists(anteriorFile))
            {
                System.IO.File.Delete(anteriorFile);
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
