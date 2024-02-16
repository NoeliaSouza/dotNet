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
    public class ProductoRepositorio : Repositorio<Producto>, IProductoRepositorio
    {
        private readonly ApplicationDbContext _db;
        public ProductoRepositorio(ApplicationDbContext db) : base(db) { // Le pasamos el db a la clase padre 
            _db = db;
        }

        public void Actualizar(Producto producto)
        {
            var productoDB = _db.Productos.FirstOrDefault(b => b.Id == producto.Id);
            if (productoDB != null)
            {   
                if (producto.ImagenUrl != null)
                {
                    productoDB.ImagenUrl=producto.ImagenUrl;
                }

                productoDB.Codigo=producto.Codigo;
                productoDB.Descripcion=producto.Descripcion;
                productoDB.Precio=producto.Precio;
                productoDB.Costo=producto.Costo;
                productoDB.CategoriaId=producto.CategoriaId;
                productoDB.MarcaId=producto.MarcaId;
                productoDB.PadreId=producto.PadreId;
                productoDB.Estado=producto.Estado;
               

                _db.SaveChanges();
            }
        }
    }
}
