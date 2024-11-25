using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPAV.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Nombre { get; set; }

        [Required]
        public required string ApellidoPaterno { get; set; }

        [MaxLength(10)]
        public required string Telefono { get; set; }

        [MaxLength(4)]
        public string Codigo { get; set; }
        [MaxLength(50)]
        public string Estado { get; set; }
        [Required, MaxLength(50)]
        public required string Email { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public string Ciudad { get; set; }
        [Required]
        public string CodigoPostal { get; set; }
        [Required]
        public bool Activo { get; set; }
        [Required]
        public int RoleId { get; set; }
        [Required]
        public int EstadoId { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
