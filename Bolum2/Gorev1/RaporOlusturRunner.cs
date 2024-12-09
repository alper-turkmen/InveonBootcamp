using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Gorev1.RaporOlustur;

namespace Gorev1.RaporOlusturRunner
{
    public class RaporOlusturRunner
    {
        public async Task RunAll()
        {
            Console.WriteLine("Senkron rapor baslatiliyor..");
            var stopwatch = Stopwatch.StartNew();
            RaporIslemleri.RaporOlusturSync();
            stopwatch.Stop();
            Console.WriteLine($"Senkron rapor tamamlandi. Gecen sure: {stopwatch.ElapsedMilliseconds} ms\n");

            Console.WriteLine("Asenkron rapor baslatiliyor...");
            stopwatch.Restart();
            await RaporIslemleri.RaporOlusturAsync(); 
            stopwatch.Stop();
            Console.WriteLine($"Asenkron rapor tamamlandi. Gecen sure: {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}