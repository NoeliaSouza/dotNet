using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Modelos.Especificaciones
{
    public class ListaPaginada<T>: List<T>
    {
        public MetaData MetaData { get; set; }
        public ListaPaginada(List<T> items, int count, 
            int numeroPagina, int tamañoPagina)
        {
            MetaData = new MetaData
            {
                TotalCount=count,
                TamañoPagina=tamañoPagina,
                PaginasTotales=(int)Math.Ceiling(count/(double)tamañoPagina)
            };
            AddRange(items); 
        }

        public static ListaPaginada<T> AListaPaginada(IEnumerable<T> entidad, int numeroPagina, int tamañoPagina)
        {
            var count = entidad.Count();
            var items = entidad.Skip((numeroPagina - 1) * tamañoPagina).Take(tamañoPagina).ToList();
            return new ListaPaginada<T>(items, count, numeroPagina, tamañoPagina);
        }

    }
}
