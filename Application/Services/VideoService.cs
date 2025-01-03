using Core.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class VideoService
{
    private readonly ApplicationDbContext _context;

    public VideoService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Video> AddVideoAsync(int courseId, VideoDto videoDto)
    {
        var video = new Video
        {
            Title = videoDto.Title,
            Url = videoDto.Url,
            CourseId = courseId
        };

        _context.Videos.Add(video);
        await _context.SaveChangesAsync();
        return video;
    }

    public async Task<Video> GetVideoAsync(int courseId, int videoId)
    {
        return await _context.Videos
            .FirstOrDefaultAsync(v => v.Id == videoId && v.CourseId == courseId);
    }

    public async Task UpdateVideoAsync(Video video, VideoDto videoDto)
    {
        video.Title = videoDto.Title;
        video.Url = videoDto.Url;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteVideoAsync(Video video)
    {
        _context.Videos.Remove(video);
        await _context.SaveChangesAsync();
    }

    public async Task<Course> GetTeacherCourseAsync(int courseId, string teacherId)
    {
        return await _context.Courses
            .Include(c => c.Videos)
            .FirstOrDefaultAsync(c => c.Id == courseId && c.TeacherId == teacherId);
    }
}