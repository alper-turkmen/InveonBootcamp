public class OrderCreateDto
{
    public List<int> CourseIds { get; set; }
}

public class OrderDto
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public int CourseId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal Price { get; set; }
    public string PaymentStatus { get; set; }

    public string CourseTitle { get; set; } 
    public string CourseCoverImage { get; set; }

    public string CourseDescription { get; set; }


public static OrderDto FromOrder(Order order)
{
    return new OrderDto
    {
        Id = order.Id,
        UserId = order.UserId,
        CourseId = order.CourseId,
        OrderDate = order.OrderDate,
        Price = order.Price,
        PaymentStatus = order.PaymentStatus,
        CourseTitle = order.Course?.Title,
        CourseCoverImage = order.Course?.CoverImage,
        CourseDescription = order.Course?.Description
    };
}
}