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
    video.Title = videoDto.Title ?? video.Title; 
    video.Url = videoDto.Url ?? video.Url;   
    video.IndexInCourse = videoDto.IndexInCourse;
                          

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


    public async Task<int> GetLastVideoIndexAsync(int courseId)
{
    var videos = await _context.Videos
                               .Where(v => v.CourseId == courseId)
                               .OrderByDescending(v => v.IndexInCourse)
                               .FirstOrDefaultAsync();

    return videos?.IndexInCourse ?? 0;
}

public async Task AddVideoAsync(Video video)
{
    _context.Videos.Add(video);
    await _context.SaveChangesAsync();
}

    public async Task<string> SaveVideoFileAsync(string base64File)
    {
        var fileBytes = Convert.FromBase64String(base64File);

        var fileName = $"{Guid.NewGuid()}.mp4";

        var filePath = Path.Combine("wwwroot/coursevideo", fileName);

        await System.IO.File.WriteAllBytesAsync(filePath, fileBytes);

        return $"/coursevideo/{fileName}";
    }

}