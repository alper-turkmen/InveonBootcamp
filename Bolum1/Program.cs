// See https://aka.ms/new-console-template for more information
using Bolum1.SingleResponsibility.CorrectUse;
using Bolum1.SingleResponsibility;
using Bolum1.OpenClosed.CorrectUse;
using Bolum1.OpenClosed.WrongUse;
using Bolum1.LiskovSubstitution.Correct;
using Bolum1.LiskovSubstitution.Wrong;
using Bolum1.InterfaceSegregation.Correct;
using Bolum1.InterfaceSegregation.Wrong;
using Bolum1.DependencyInversion.Correct;
using Bolum1.DependencyInversion.Wrong;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("SINGLE RESPONSIBILITY:");


        Console.WriteLine("Yanlis kullanim:");
        var wrongEmployee = new EmployeeWrongUse
        {
            Name = "Alper Turkmen",
            Position = "Software Developer"
        };
        wrongEmployee.SaveToDatabase();
        wrongEmployee.GenerateReport();

        Console.WriteLine();

        Console.WriteLine("Dogru kullanim:");
        var correctEmployee = new Employee("Alper Turkmen", "Software Developer");

        var repository = new EmployeeRepository();
        repository.Save(correctEmployee);

        var reportGenerator = new EmployeeReportGenerator();
        reportGenerator.GenerateReport(correctEmployee);



        Console.WriteLine();
        Console.WriteLine();

        /////////////////////

        Console.WriteLine("OPEN CLOSED:");
        Console.WriteLine("Yanlis kullanim:");
        var wrongCalculator = new AreaCalculatorWrong();
        Console.WriteLine($"Cember alani: {wrongCalculator.CalculateArea("Circle", 5)}");
        Console.WriteLine($"Dortgen alani: {wrongCalculator.CalculateArea("Rectangle", 4, 6)}");

        Console.WriteLine();

        Console.WriteLine("Dogru Kullanim:");
        var correctCalculator = new AreaCalculator();

        var circle = new Circle(5);
        Console.WriteLine($"Cember alani: {correctCalculator.CalculateArea(circle)}");

        var rectangle = new Rectangle(4, 6);
        Console.WriteLine($"Dorgen Alani: {correctCalculator.CalculateArea(rectangle)}");

        Console.WriteLine();
        Console.WriteLine();

        /////////////////////

        Console.WriteLine("LISKOV SUBSTITION:");
        Console.WriteLine("Yanlis kullanim:");

        VehicleWrong carWrong = new CarWrong();
        carWrong.StartEngine();

        VehicleWrong bicycleWrong = new BicycleWrong();
        try
        {
            bicycleWrong.StartEngine();
        }
        catch (NotImplementedException ex)
        {
            Console.WriteLine("Hata firlatildi");
            Console.WriteLine(ex.Message);
        }

        Console.WriteLine();

        Console.WriteLine("Dogru kullanim:");
        Vehicle car = new Car();
        car.Move();

        Vehicle bicycle = new Bicycle();
        bicycle.Move();

        Console.WriteLine();
        Console.WriteLine();

        /////////////////////

        Console.WriteLine("INTERFACE SEGREGATION:");
        Console.WriteLine("Yanlis kullanim:");
        INotifierWrong emailWrong = new EmailNotifierWrong();
        emailWrong.AddEmailSubject("E-mail basligi");
        emailWrong.SendNotification("E-mail icerigi");

        INotifierWrong smsWrong = new SMSNotifierWrong();
        try
        {
            smsWrong.AddEmailSubject("SMS basligi");
        }
        catch (NotImplementedException ex)
        {
            Console.WriteLine("Hata firlatildi");
            Console.WriteLine(ex.Message);
        }
        smsWrong.SendNotification("SMS icerigi");

        Console.WriteLine();

        Console.WriteLine("Dogru kullanim:");
        INotifier email = new EmailNotifier();
        ((IEmailNotifier)email).AddEmailSubject("E-mail basligi");
        email.SendNotification("E-mail icerigi");

        INotifier sms = new SMSNotifier();
        sms.SendNotification("SMS icerigi");

        Console.WriteLine();
        Console.WriteLine();

        /////////////////////

        Console.WriteLine("DEPENDENCY INVERSION:");
        Console.WriteLine("Yanlis kullanim:");

        
        var notificationWrong = new NotificationWrongUse();
        notificationWrong.SendEmail("Email mesaji");
        notificationWrong.SendSms("SMS Mesaji");

        Console.WriteLine();

        Console.WriteLine("Dogru kullanim:");
        INotificationSenderCorrect emailSender = new EmailSenderCorrect();
        var emailNotification = new NotificationCorrect(emailSender);
        emailNotification.Notify("Email mesaji");

        INotificationSenderCorrect smsSender =  new SmsSenderCorrectUse();
        var smsNotification = new NotificationCorrect(smsSender);
        smsNotification.Notify("SMS mseaji");

        var input = Console.ReadLine(); 

    }
}