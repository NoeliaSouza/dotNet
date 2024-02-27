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
    public class UsuarioRepositorio : Repositorio<Usuario>, IUsuarioRepositorio
    {
        private readonly ApplicationDbContext _db;
        public UsuarioRepositorio(ApplicationDbContext db) : base(db) { // Le pasamos el db a la clase padre 
            _db = db;
        }

    }
}
