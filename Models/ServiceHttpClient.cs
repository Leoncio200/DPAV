using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DPAV.Models
{
    public class ServiceHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _path = "http://192.168.252.242:8000/api/";
        public ServiceHttpClient()
        {
            _httpClient = new HttpClient();
        }

        // Método GET
        public async Task<String> GetAsync(string url, object data)
        {
            var response = await _httpClient.GetAsync(_path + url);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        // Método POST
        public async Task<string> PostAsync(string url, object data)
        {
            var body = JsonConvert.SerializeObject(data);
            var content = new StringContent(body, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_path + url, content);

            //response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        // Método PUT
        public async Task<string> PutAsync(string url, object data)
        {
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(_path + url, content);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        // Método DELETE
        public async Task<string> DeleteAsync(string url)
        {
            var response = await _httpClient.DeleteAsync(_path + url);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
