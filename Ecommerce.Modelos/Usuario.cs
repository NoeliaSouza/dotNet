using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Modelos
{
    public class Usuario: IdentityUser
    {
        [Required(ErrorMessage ="Nombre es requerido")]
        [MaxLength(50)]
        public string Nombres { get; set; }
        [Required(ErrorMessage = "Apellido es requerido")]
        [MaxLength(50)]
        public string Apellidos { get; set; }
        [Required(ErrorMessage = "Domicilio es requerido")]
        [MaxLength(50)]
        public string Domicilio { get; set; }

        [Required(ErrorMessage = "Domicilio es requerido")]
        [MaxLength(50)]
        public string Ciudad { get; set; }

        [Required(ErrorMessage = "Domicilio es requerido")]
        [MaxLength(50)]
        public string Pais { get; set; }

        [NotMapped]
        public string Rol { get; set; }
    }
}
