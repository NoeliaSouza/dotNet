using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Modelos
{
    public class Bodega
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="El nombre es requerido")]
        [MaxLength(60, ErrorMessage ="Nombre debe tener como máximo 60 caracteres")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "La descripcion es requerido")]
        [MaxLength(100, ErrorMessage = "descripción debe tener como máximo 100 caracteres")]
        public string Descripcion  { get; set; }
        [Required(ErrorMessage ="Estado es requerido")]
        public bool Estado { get; set; }
    }
}
