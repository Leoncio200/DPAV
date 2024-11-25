using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPAV.Models
{
    public class ServiceHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _path =  "";
        public ServiceHttpClient()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> GetDataAsync(string ruta)
        {
            var response = await _httpClient.GetAsync(_path + ruta);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new HttpRequestException($"Error en la solicitud: {response.StatusCode}");
            }
        }
    }
}
