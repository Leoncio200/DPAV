using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPAV.Models
{
    public class Perro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(255)]
        public string Raza { get; set; }

        [Required]
        [Range(0, 30)]
        public int Edad { get; set; }

        [Required]
        [StringLength(50)]
        public string Color { get; set; }

        [Required]
        [Range(0, 3)]
        public double Altura { get; set; }

        [Required]
        [StringLength(50)]
        public string Tamaño { get; set; }

        [Required]
        [Range(0, 100)]
        public double Peso { get; set; }

        [Required]
        [StringLength(10)]
        public string? Sexo { get; set; }

        [Required]
        [StringLength(50)]
        public string? Estatus { get; set; }

        public DateTime? FechaNacimiento { get; set; }

        [StringLength(50)]
        public string? Chip { get; set; }

        public bool Vacunado { get; set; }
        public bool Esterilizado { get; set; }


        public Usuario? User { get; set; }
    }
}
