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
        public string User { get; set; }
        public string Token { get; set; }

        // Constructor privado para evitar instancias directas
        private Singleton() { }

        // Método para establecer los datos del usuario
        public async Task SetSession(string user, string token)
        {
            User = user;
            Token = token;

            // Guardar los datos de sesión en almacenamiento persistente
            await SecureStorage.SetAsync("User", user);
            await SecureStorage.SetAsync("Token", token);
        }

        // Método para cargar la sesión desde el almacenamiento
        public async Task LoadSessionAsync()
        {
            User = await SecureStorage.GetAsync("User") ?? string.Empty;
            Token = await SecureStorage.GetAsync("Token") ?? string.Empty;
        }

        // Método para limpiar la sesión (logout)
        public void Clear()
        {
            User = string.Empty;
            Token = string.Empty;

            // Eliminar los datos persistentes
            SecureStorage.Remove("User");
            SecureStorage.Remove("Token");
        }
    }
}
