using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Modelos
{
    public class Marca
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage="Nombre es requerido")]
        [MaxLength(60, ErrorMessage ="Nombre debe tener como maximo 60 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage ="Descripcion es requerido")]
        [MaxLength(100, ErrorMessage ="Descripción debe tener como maximo 100 caracteres")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage ="Estado es requerido")]
        public bool Estado { get; set; }
    }
}
