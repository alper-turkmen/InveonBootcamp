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
    Summary = "Kullanicinin siparislerini getirir."
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
        Summary = "Siparis id'sine göre siparis getirir."
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
        Summary = "Siparis id'sine göre kurs detaylarını getirir."
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
    [SwaggerOperation(
        Summary = "Öğretmenin siparislerini getirir."
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
        Summary = "Siparis oluşturur. Birden fazla kurs siparişi verilebilir."
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
        Summary = "Siparis id'sine göre siparis siler."
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