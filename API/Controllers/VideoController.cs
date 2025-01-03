using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Teacher")]
public class VideoController : ControllerBase
{
    private readonly VideoService _videoService;

    public VideoController(VideoService videoService)
    {
        _videoService = videoService;
    }

    private string GetUserId() => User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    [HttpPost("{courseId}/videos")]
    public async Task<ActionResult<VideoDto>> AddVideo(int courseId, [FromBody] VideoDto videoDto)
    {
        var userId = GetUserId();
        var course = await _videoService.GetTeacherCourseAsync(courseId, userId);
        if (course == null) return NotFound("Course not found.");

        var video = await _videoService.AddVideoAsync(courseId, videoDto);
        return VideoDto.FromVideo(video);
    }

    [HttpDelete("{courseId}/videos/{videoId}")]
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