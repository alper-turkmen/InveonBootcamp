using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrdersController : ControllerBase
{
    private readonly OrderService _orderService;
    private readonly CourseService _courseService;

    public OrdersController(OrderService orderService, CourseService courseService)
    {
        _orderService = orderService;
        _courseService = courseService;
    }

    [HttpGet]
    [SwaggerOperation(
    Summary = "Kullanıcının siparişlerini getirir."
    )]
    public async Task<ActionResult<List<OrderDto>>> GetUserOrders()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var orders = await _orderService.GetUserOrdersAsync(userId);

        orders = orders.OrderByDescending(o => o.OrderDate).ToList();

        return Ok(orders.Select(o => OrderDto.FromOrder(o)));
    }

    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Sipariş id'sine göre sipariş getirir."
    )]
    public async Task<ActionResult<OrderDto>> GetOrderById(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var order = await _orderService.GetOrderByIdAsync(id, userId);
        if (order == null) return NotFound("Sipariş bulunamadı.");

        return Ok(OrderDto.FromOrder(order));
        
    }

    [HttpGet("{id}/coursedetails")]
    [SwaggerOperation(
        Summary = "Sipariş id'sine göre kurs detaylarını getirir."
    )]
    public async Task<ActionResult<CourseDto>> GetOrderedCourse(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var order = await _orderService.GetOrderByIdAsync(id, userId);
        if (order == null) return NotFound("Sipariş bulunamadı.");

        var course = await _courseService.GetCourseAsync(order.CourseId);
        return Ok(CourseDto.FromCourse(course));
    }


    [HttpGet("teacher")]
    [Authorize(Roles = "Teacher")]
    [SwaggerOperation(
        Summary = "Amacı öğretmenin kendi kurslarının siparişlerini görmesidir."
    )]
    public async Task<ActionResult<List<OrderDto>>> GetTeacherOrders()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var orders = await _orderService.GetTeacherOrdersAsync(userId);

        orders = orders.OrderByDescending(o => o.OrderDate).ToList();

        return Ok(orders.Select(o => OrderDto.FromOrder(o)));
    }

 [HttpPost]
    [SwaggerOperation(
        Summary = "Sipariş oluşturur. Birden fazla kurs siparişi verilebilir. Sepet mantığı ile çalışır."
    )]
public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDto orderCreateDto)
{
    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
    if (userId == null) return Unauthorized();

    var result = await _orderService.CreateOrdersAsync(userId, orderCreateDto.CourseIds);

    if (!result.IsSuccess)
    {
        return BadRequest(new { message = result.Message });
    }

    return Ok(new { message = "Sipariş oluşturuldu." });
}

  

    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Sipariş id'sine göre sipariş siler."
    )]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var order = await _orderService.GetOrderByIdAsync(id, userId);
        if (order == null) return NotFound("Sipariş bulunamadı.");

        await _orderService.DeleteOrderAsync(order);
        return Ok(new { message = "Sipariş silindi." });
    }
}