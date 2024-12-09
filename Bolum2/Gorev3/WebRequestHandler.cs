using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Gorev3
{
    public class WebRequestHandler
    {
        private readonly HttpClient _httpClient;

        public WebRequestHandler()
        {
            _httpClient = new HttpClient(); 
        }

        public async Task<string> FetchDataAsync(string url)
        {
            try
            {
                Console.WriteLine($"GET istegi yapiliyor: {url}");

                HttpResponseMessage response = await _httpClient.GetAsync(url);

                
                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();
                Console.WriteLine("GET istegi basarili.");
                return content;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP hatasi: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Bilinmeyen hata: {ex.Message}");
                return null;
            }
        }
    }
}