using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using System.Security.Claims;
using Swashbuckle.AspNetCore.Annotations;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Teacher")]
public class VideoController : ControllerBase
{
        private readonly IWebHostEnvironment _environment;
    private readonly VideoService _videoService;

    public VideoController(VideoService videoService, IWebHostEnvironment environment)
    {
        _videoService = videoService;
        _environment = environment;
    }

    private string GetUserId() => User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

[RequestSizeLimit(500 * 1024 * 1024)]
[HttpPost("{courseId}/videos")]
[SwaggerOperation(Summary = "Kursa video upload eder. Öğretmene özeldir. base64 formatında kabul eder. Dosyayı /files/coursevideo klasörüne kaydeder.")]
public async Task<ActionResult<VideoDto>> AddVideo(int courseId, [FromBody] VideoUploadDto videoUploadDto)
{
    var userId = GetUserId();

    var course = await _videoService.GetTeacherCourseAsync(courseId, userId);
    if (course == null) return NotFound("Course not found.");

  var uploadsFolder = Path.Combine(_environment.WebRootPath, "files/coursevideo");

        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        var bytes = Convert.FromBase64String(videoUploadDto.File);
        var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(videoUploadDto.FileName)}";
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        await System.IO.File.WriteAllBytesAsync(filePath, bytes);

        var fileUrl = $"/files/coursevideo/{uniqueFileName}";
        

    var lastIndex = await _videoService.GetLastVideoIndexAsync(courseId);
    var newIndex = lastIndex + 1;

    var video = new Video
    {
        Title = videoUploadDto.Title,
        Url = fileUrl, 
        CourseId = courseId,
        Duration = 123,
        IndexInCourse = newIndex
    };

    await _videoService.AddVideoAsync(video);

    var videoDto = VideoDto.FromVideo(video);
    return CreatedAtAction(nameof(AddVideo), new { courseId = courseId, videoId = video.Id }, videoDto);
}

    [HttpDelete("{courseId}/videos/{videoId}")]
    [SwaggerOperation(Summary = "Kurstan video siler. Öğretmene özeldir.")]
    public async Task<ActionResult> DeleteVideo(int courseId, int videoId)
    {
        var userId = GetUserId();
        var course = await _videoService.GetTeacherCourseAsync(courseId, userId);
        if (course == null) return NotFound("Course not found.");

        var video = await _videoService.GetVideoAsync(courseId, videoId);
        if (video == null) return NotFound("Video not found.");

        await _videoService.DeleteVideoAsync(video);
        return NoContent();
    }

    [HttpPut("{courseId}/videos/{videoId}")]
    [SwaggerOperation(Summary = "Videonun görüntüleneceği sırayı vb. bilgileri günceller.")]
    public async Task<ActionResult<VideoDto>> UpdateVideo(int courseId, int videoId, [FromBody] VideoDto videoDto)
    {
        var userId = GetUserId();
        var course = await _videoService.GetTeacherCourseAsync(courseId, userId);
        if (course == null) return NotFound("Course not found.");

        var video = await _videoService.GetVideoAsync(courseId, videoId);
        if (video == null) return NotFound("Video not found.");

        await _videoService.UpdateVideoAsync(video, videoDto);
        return VideoDto.FromVideo(video);
    }
}