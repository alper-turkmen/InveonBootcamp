using Core.Entities;

public class Order
{
    public int Id { get; set; } 
    public string UserId { get; set; } 
    public User User { get; set; }

    public int CourseId { get; set; } 
    public Course Course { get; set; } 

    public DateTime OrderDate { get; set; } 
    public decimal Price { get; set; } 

    public string PaymentStatus { get; set; } 
}