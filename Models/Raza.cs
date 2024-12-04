using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPAV.Models
{
    public class Raza
    {
        [Key]
        public int Id { get; set; }

        [JsonProperty("nombre")]
        public string? Nombre { get; set; }
        
        [JsonProperty("estado")]
        [Required]
        public bool Estado { get; set; }
    }
}
