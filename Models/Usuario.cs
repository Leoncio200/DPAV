using Newtonsoft.Json;
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

        [JsonProperty("nombre")]
        [Required]
        public string? Nombre { get; set; }

        [JsonProperty("apellido_paterno")]
        [Required]
        public string? ApellidoPaterno { get; set; }

        [JsonProperty("telefono")]
        [MaxLength(10)]
        public string? Telefono { get; set; }

        [MaxLength(4)]
        public string? Codigo { get; set; }
        [MaxLength(50)]
        public string? Estado { get; set; }

        [JsonProperty("email")]
        [Required, MaxLength(50)]
        public string? Email { get; set; }
        [Required]
        public string? Direccion { get; set; }
        [Required]
        public string? Ciudad { get; set; }
        [Required]
        public string? CodigoPostal { get; set; }
        [Required]
        public bool Activo { get; set; }
        [JsonProperty("role_id")]
        [Required] 
        public int RoleId { get; set; }
        [Required]
        public int EstadoId { get; set; }

        [JsonProperty("password")]
        [Required]
        public string Password { get; set; }

        public Usuario()
        {
            Nombre = string.Empty;
            ApellidoPaterno = string.Empty;
            Telefono = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
        }
    }
}
