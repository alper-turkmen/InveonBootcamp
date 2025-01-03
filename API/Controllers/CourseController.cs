using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Teacher")]
public class CourseController : ControllerBase
{
    private readonly CourseService _courseService;

    public CourseController(CourseService courseService)
    {
        _courseService = courseService;
    }

    private string GetUserId() => User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    [HttpGet]
    public async Task<ActionResult<List<CourseDto>>> GetTeacherCourses()
    {
        var courses = await _courseService.GetTeacherCoursesAsync(GetUserId());
        return Ok(courses);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CourseDto>> GetCourse(int id)
    {
        var course = await _courseService.GetTeacherCourseAsync(id, GetUserId());
        if (course == null) return NotFound("Course not found.");

        return CourseDto.FromCourse(course);
    }

    [HttpPost]
    public async Task<ActionResult<Course>> CreateCourse([FromBody] CourseCreateDto courseDto)
    {
        var course = new Course
        {
            Title = courseDto.Title,
            Description = courseDto.Description,
            CoverImage = courseDto.CoverImage,
            Price = courseDto.Price,
            TeacherId = GetUserId(),
            Videos = courseDto.Videos.Select(v => new Video { Title = v.Title, Url = v.Url }).ToList()
        };

        course = await _courseService.CreateCourseAsync(course);
        return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, CourseDto.FromCourse(course));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCourse(int id, [FromBody] CourseUpdateDto courseDto)
    {
        var course = await _courseService.GetTeacherCourseAsync(id, GetUserId());
        if (course == null) return NotFound("Course not found.");

        course.Title = courseDto.Title;
        course.Description = courseDto.Description;
        course.CoverImage = courseDto.CoverImage;
        course.Price = courseDto.Price;

        await _courseService.UpdateCourseAsync(course);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse(int id)
    {
        var course = await _courseService.GetTeacherCourseAsync(id, GetUserId());
        if (course == null) return NotFound("Course not found.");

        await _courseService.DeleteCourseAsync(course);
        return NoContent();
    }
}