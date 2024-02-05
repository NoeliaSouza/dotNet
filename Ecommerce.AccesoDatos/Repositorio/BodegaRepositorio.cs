using Ecommerce.AccesoDatos.Data;
using Ecommerce.AccesoDatos.Repositorio.IRepositorio;
using Ecommerce.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.AccesoDatos.Repositorio
{
    public class BodegaRepositorio : Repositorio<Bodega>, IBodegaRepositorio
    {
        private readonly ApplicationDbContext _db;
        public BodegaRepositorio(ApplicationDbContext db) : base(db) { // Le pasamos el db a la clase padre 
            _db = db;
        }

        public void Actualizar(Bodega bodega)
        {
            var bodegaDB = _db.Bodegas.FirstOrDefault(b => b.Id == bodega.Id);
            if (bodegaDB != null)
            {
                bodegaDB.Nombre = bodega.Nombre;
                bodegaDB.Descripcion = bodega.Descripcion;
                bodegaDB.Estado = bodega.Estado;
                _db.SaveChanges();
            }
        }
    }
}
