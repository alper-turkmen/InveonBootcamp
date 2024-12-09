using System;
using System.Collections.Generic;
using System.IO;

namespace Bolum1.SingleResponsibility
{
    internal class EmployeeWrongUse
    {
        public string Name { get; set; }
        public string Position { get; set; }

        public void SaveToDatabase()
        {
            Console.WriteLine($"{Name} ({Position}) kaydedildi");
        }

        public void GenerateReport()
        {
            Console.WriteLine($"{Name} ({Position}) icin rapor olusturuldu");
        }


        public void SendEmail(string email, string message)
        {
            Console.WriteLine($"Email gonderildi: {email} - Mesaj: {message}");
        }

        public double CalculateTaxes()
        {
            double taxRate = 0.2;
            double salary = 5000;
            double taxes = salary * taxRate;
            Console.WriteLine($"Vergi hesaplandi: {taxes} TL");
            return taxes;
        }

        public void BackupEmployeeFiles()
        {
            Console.WriteLine($"Dosyalar yedeklendi");
        }

        public void ShareOnSocialMedia()
        {
            Console.WriteLine($"{Name} sosyal medyada paylasim yaptı");
        }
    }
}