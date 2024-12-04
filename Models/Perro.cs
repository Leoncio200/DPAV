using Newtonsoft.Json;
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

        [JsonProperty("nombre")]
        [StringLength(255)]
        public string? Nombre { get; set; }

        [JsonProperty("raza")]
        public Raza? Raza { get; set; }

        [JsonProperty("edad")]
        [Range(0, 30)]
        public int Edad { get; set; }

        [JsonProperty("color")]
        [StringLength(50)]
        public string? Color { get; set; }

        [JsonProperty("altura")]
        [Range(0, 3)]
        public double Altura { get; set; }

        [JsonProperty("tamaño")]
        [StringLength(50)]
        public string? Tamaño { get; set; }

        [JsonProperty("peso")]
        [Range(0, 100)]
        public double Peso { get; set; }

        [JsonProperty("sexo")]
        [StringLength(10)]
        public string? Sexo { get; set; }

        [Required]
        public int Estatus { get; set; }

        [JsonProperty("fecha_nacimiento")]
        public DateTime? FechaNacimiento { get; set; }
        
        [JsonProperty("chip")]
        [StringLength(50)]
        public string? Chip { get; set; }

        [JsonProperty("vacunado")]
        public bool Vacunado { get; set; }
        
        [JsonProperty("esterilizado")]
        public string? Esterilizado { get; set; }
        
        [JsonProperty("id_raza")]
        public int IDRaza { get; set; }

        public Usuario? User { get; set; }
    }
}
