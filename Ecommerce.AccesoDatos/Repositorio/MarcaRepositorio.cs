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
    public class MarcaRepositorio : Repositorio<Marca>, IMarcaRepositorio
    {
        private readonly ApplicationDbContext _db;
        public MarcaRepositorio(ApplicationDbContext db) : base(db) { // Le pasamos el db a la clase padre 
            _db = db;
        }

        public void Actualizar(Marca marca)
        {
            var marcaDB = _db.Categorias.FirstOrDefault(b => b.Id == marca.Id);
            if (marcaDB != null)
            {
                marcaDB.Nombre = marca.Nombre;
                marcaDB.Descripcion = marca.Descripcion;
                marcaDB.Estado = marca.Estado;
                _db.SaveChanges();
            }
        }
    }
}
