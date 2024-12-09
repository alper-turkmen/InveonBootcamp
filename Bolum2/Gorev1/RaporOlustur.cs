using System;
using System.Threading;
using System.Threading.Tasks;

namespace Gorev1.RaporOlustur
{
    public class RaporIslemleri
    {
        public static void RaporOlusturSync()
        {
            Console.WriteLine("Senkron rapor olusturuluyor.");
            Thread.Sleep(5000);
            Console.WriteLine("Senkron rapor olusturma islemi tamamlandi");
        }

        public static async Task RaporOlusturAsync()
        {
            Console.WriteLine("Asenkron rapor olusturuluyor");
            await Task.Delay(5000);
            Console.WriteLine("Asenkron rapor olusturma islemi tamamlandi");
        }
    }
}