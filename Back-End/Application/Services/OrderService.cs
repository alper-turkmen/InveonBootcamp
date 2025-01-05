using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;

public class OrderService
{
    private readonly ApplicationDbContext _context;
    private readonly CourseService _courseService;



    public OrderService(ApplicationDbContext context, CourseService courseService)
    {
        _context = context;
        _courseService = courseService;
    }
    
    public async Task<List<Order>> GetUserOrdersAsync(string userId)
    {
        return await _context.Orders
            .Where(o => o.UserId == userId)
            .Include(o => o.Course)
            .ToListAsync();
    }

    public async Task<List<Order>> GetTeacherOrdersAsync(string teacherId)
    {
        return await _context.Orders
            .Where(o => o.Course.TeacherId == teacherId)
            .Include(o => o.Course)
            .ToListAsync();
    }

    public async Task<Order?> GetOrderByIdAsync(int orderId, string userId)
    {
        return await _context.Orders
            .Include(o => o.Course) 
            .FirstOrDefaultAsync(o => o.Id == orderId && o.UserId == userId);
    }

    public async Task<Order> CreateOrderAsync(string userId, int courseId, decimal price)
    {
        var order = new Order
        {
            UserId = userId,
            CourseId = courseId,
            OrderDate = DateTime.UtcNow,
            Price = price,
            PaymentStatus = "Tamamlandı"
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return order;
    }

    public async Task<ServiceResult> CreateOrdersAsync(string userId, List<int> courseIds)
{

    List<Course> courses = new List<Course>();

    foreach (var courseId in courseIds)
    {
        var course = await _courseService.GetCourseAsync(courseId);
        if (course == null)
        {
            return ServiceResult.Failure(404, "Kurs bulunamadı.");
        }
        courses.Add(course);
    }



    foreach (var course in courseIds)
    {
        var hasBought = await this.HasUserBoughtCourseAsync(userId, course);
        if (hasBought)
        {
            return ServiceResult.Failure(400, "Bir/birden fazla kursu daha önce satın aldınız.");
        }
    }

    foreach (var courseId in courseIds)
    {
        var course = await _courseService.GetCourseAsync(courseId);
        if (course == null)
        {
            return ServiceResult.Failure(404, "Kurs bulunamadı.");
        }
    }

    foreach (var course in courses)
    {
        var courseCreateItem = await this.CreateOrderAsync(userId, course.Id, course.Price);
        if (courseCreateItem == null)
        {
            return ServiceResult.Failure(500, "Sipariş oluşturulurken bir hata oluştu.");
        }

    }

    return ServiceResult.Success();
}

    public async Task DeleteOrderAsync(Order order)
    {
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePaymentStatusAsync(int orderId, string status)
    {
        var order = await _context.Orders.FindAsync(orderId);
        if (order != null)
        {
            order.PaymentStatus = status; 
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> IsPaymentCompletedAsync(int orderId)
    {
        var order = await _context.Orders.FindAsync(orderId);
        return order != null && order.PaymentStatus == "Completed";
    }

    public async Task<bool> HasUserBoughtCourseAsync(string userId, int courseId)
    {
        return await _context.Orders.AnyAsync(o => o.UserId == userId && o.CourseId == courseId);
    }
}

public class ServiceResult
{
    public bool IsSuccess { get; private set; }
    public int StatusCode { get; private set; }
    public string Message { get; private set; }

    public static ServiceResult Success()
    {
        return new ServiceResult { IsSuccess = true, StatusCode = 200 };
    }

    public static ServiceResult Failure(int statusCode, string message)
    {
        return new ServiceResult { IsSuccess = false, StatusCode = statusCode, Message = message };
    }
}