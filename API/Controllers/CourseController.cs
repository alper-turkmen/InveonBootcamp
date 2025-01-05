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
    private readonly IWebHostEnvironment _environment;


    public CourseController(CourseService courseService, IWebHostEnvironment environment)
    {
        _courseService = courseService;
        _environment = environment;
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
            Price = courseDto.Price,
            TeacherId = GetUserId(),
            Videos = courseDto.Videos.Select(v => new Video { Title = v.Title, Url = v.Url }).ToList()
        };
        var fileUrl = "";

           if (courseDto.CoverImage != null)
        {
              var uploadsFolder = Path.Combine(_environment.WebRootPath, "files/courseimg");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var bytes = Convert.FromBase64String(courseDto.CoverImage);
                var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(courseDto.CoverImageName)}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                await System.IO.File.WriteAllBytesAsync(filePath, bytes);

                fileUrl = $"/files/courseimg/{uniqueFileName}";
        }

        course.CoverImage = fileUrl;

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
        course.Price = courseDto.Price;

        await _courseService.UpdateCourseAsync(course);
        return NoContent();
    }

    [HttpPut("{id}/photo")]
    public async Task<IActionResult> UpdateCoursePhoto(int id, [FromBody] CoursePhotoDto courseDto)
    {
        var course = await _courseService.GetTeacherCourseAsync(id, GetUserId());
        if (course == null) return NotFound("Course not found.");
        var fileUrl = course.CoverImage;
          if (courseDto.CoverImage != null)
        {
              var uploadsFolder = Path.Combine(_environment.WebRootPath, "files/courseimg");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var bytes = Convert.FromBase64String(courseDto.CoverImage);
                var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(courseDto.CoverImageName)}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                await System.IO.File.WriteAllBytesAsync(filePath, bytes);

                fileUrl = $"/files/courseimg/{uniqueFileName}";
        }
        course.CoverImage = fileUrl;

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