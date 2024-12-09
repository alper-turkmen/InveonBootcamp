using System;
using System.Threading;
using System.Threading.Tasks;

namespace Gorev2.TaskMethods
{
    public class TaskExamples
    {
        public async Task DelayExample()
        {
            Console.WriteLine("Task.Delay ornegi basliyor..");
            await Task.Delay(2000); 

            Console.WriteLine("Task.Delay tamamlandi.\n");
        }



        public async Task RunExample()
        {
            Console.WriteLine("Task.Run ornegi basliyor..");
            var result = await Task.Run(() => UzunSureliHesaplama());

            Console.WriteLine($"Task.Run tamamlandi. Sonuc: {result}\n");
        }


        public Task<int> FromResultExample()
        {

            Console.WriteLine("Task.FromResult ornegi..\n");
            return Task.FromResult(12); 
        }

        public async Task WhenAllExample()
        {
            Console.WriteLine("Task.WhenAll ornegi basliyor..");

            Task t1 = DosyaIndir("Dosya1");
            Task t2 = DosyaIndir("Dosya2");

            await Task.WhenAll(t1, t2);

            Console.WriteLine("Task.WhenAll tamamlandi.\n");
        }

        public async Task WhenAnyExample()
        {
            Console.WriteLine("Task.WhenAny ornegi basliyor..");
            Task<int> t1 = Api1();
            
            Task<int> t2 = Api2();

            var completedTask = await Task.WhenAny(t1, t2);
            Console.WriteLine($"Task.WhenAny tamamlandi. Ilk tamamlanan: {await completedTask}\n");
        }

        public Task CompletedTaskExample()
        {
            Console.WriteLine("Task.CompletedTask ornegi..\n");

            return Task.CompletedTask; 
        }

        public Task FromExceptionExample()
        {
            Console.WriteLine("Task.FromException ornegi..\n");

            return Task.FromException(new InvalidOperationException("Hata yakalandi"));
        }

        public Task FromCanceledExample(CancellationToken token)
        {
            Console.WriteLine("Task.FromCanceled ornegi..\n");
            return Task.FromCanceled(token);
        }

        private int UzunSureliHesaplama()
        {
            Thread.Sleep(2000);
            return 11; 
        }

        private async Task DosyaIndir(string dosyaAdi)
        {
            Console.WriteLine($"{dosyaAdi} indiriliyor..");
            await Task.Delay(2000);

            Console.WriteLine($"{dosyaAdi} indirildi.\n");
        }

        private async Task<int> Api1()
        {
            await Task.Delay(3000);
            return 1;
        }

        private async Task<int> Api2()
        {
            await Task.Delay(1000);
            return 2;
        }
    }
}