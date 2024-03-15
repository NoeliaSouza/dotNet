using Ecommerce.AccesoDatos.Data;
using Ecommerce.AccesoDatos.Repositorio.IRepositorio;
using Ecommerce.Modelos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.AccesoDatos.Repositorio
{
    public class BodegaProductoRepositorio : Repositorio<BodegaProducto>, IBodegaProductoRepositorio
    {
        private readonly ApplicationDbContext _db;
        public BodegaProductoRepositorio(ApplicationDbContext db) : base(db) { // Le pasamos el db a la clase padre 
            _db = db;
        }

        public void Actualizar(BodegaProducto bodegaProducto)
        {
            var bodegaProductoBD = _db.BodegasProductos.FirstOrDefault(b => b.Id == bodegaProducto.Id);
            if (bodegaProductoBD != null)
            {
                bodegaProductoBD.Cantidad = bodegaProducto.Cantidad;
                _db.SaveChanges();
            }
        }

       
    }
}
