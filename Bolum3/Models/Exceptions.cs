namespace Bolum3.Models.Exceptions
{
    public class CustomBookNotFoundException : Exception
    {
        public CustomBookNotFoundException(string message) : base(message) { }
    }
}