using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPAV.Models
{
    public class Singleton
    {
        // Propiedad estática para el acceso al Singleton
        private static Singleton _instance;

        public static Singleton Instance => _instance ??= new Singleton();

        // Datos de sesión (puedes agregar más según sea necesario)
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }

        // Constructor privado para evitar instancias directas
        private Singleton() { }

        // Método para limpiar la sesión (logout)
        public void Clear()
        {
            UserId = string.Empty;
            UserName = string.Empty;
            Token = string.Empty;
        }
    }
}
