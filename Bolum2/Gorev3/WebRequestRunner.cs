using System;
using System.Threading.Tasks;

namespace Gorev3
{
    public class WebRequestRunner
    {
        private readonly WebRequestHandler _webRequestHandler;

        public WebRequestRunner()
        {
            _webRequestHandler = new WebRequestHandler();
        }

        public async Task Run()
        {
            string url = "https://google.com";

            Console.WriteLine("Web istegi baslatiliyor...");
            string data = await _webRequestHandler.FetchDataAsync(url);

            if (data != null)
            {
                Console.WriteLine("Veri alindi:");            }
            else
            {
                Console.WriteLine("Veri alinamadi.");
            }
        }
    }
}