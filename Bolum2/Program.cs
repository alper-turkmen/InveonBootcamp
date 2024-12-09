using System;
using System.Diagnostics;
using System.Threading.Tasks;

using Gorev2.TaskMethods;
using Gorev2.MethodRunner;
using Gorev1.RaporOlusturRunner;
using Gorev3;



class Program
{
    static async Task Main(string[] args)
    {

        Console.WriteLine("GOREV 1");
        var raporOlusturRunner = new RaporOlusturRunner();
        await raporOlusturRunner.RunAll();

        Console.WriteLine("GOREV 2\n");

        var runner = new TaskExamplesRunner(); 
        await runner.RunAllExamples();

        Console.WriteLine("GOREV 3\n");


        var webRequestRunner = new WebRequestRunner();
        await webRequestRunner.Run();

        Console.WriteLine("Bir tusa basarak cikabilirsiniz");
        Console.ReadLine();
    }
}