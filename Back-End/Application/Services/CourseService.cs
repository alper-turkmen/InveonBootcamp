using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data; 
using Microsoft.EntityFrameworkCore;
using Core.Entities;

public class CourseService
{
    private readonly ApplicationDbContext _context;

    public CourseService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<CourseAnonymousDto>> GetCoursesAsync()
    {
        var courses = await _context.Courses
            .Include(c => c.Videos).Include(c => c.Teacher)
            .ToListAsync();

        return courses.Select(CourseAnonymousDto.FromCourse).ToList();
    }
    
    public async Task<List<CourseDto>> GetTeacherCoursesAsync(string teacherId)
    {
        var courses = await _context.Courses
            .Where(c => c.TeacherId == teacherId)
            .Include(c => c.Videos)
            .ToListAsync();

        return courses.Select(CourseDto.FromCourse).ToList();
    }

    public async Task<Course> GetTeacherCourseAsync(int id, string teacherId)
    {
        return await _context.Courses
            .Include(c => c.Videos)
            .FirstOrDefaultAsync(c => c.Id == id && c.TeacherId == teacherId);
    }

    public async Task<Course> GetCourseAsync(int id)
    {
        return await _context.Courses
            .Include(c => c.Videos)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Course> CreateCourseAsync(Course course)
    {
        _context.Courses.Add(course);
        await _context.SaveChangesAsync();
        return course;
    }

    public async Task UpdateCourseAsync(Course course)
    {
        _context.Courses.Update(course);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCourseAsync(Course course)
    {
        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();
    }

    public async Task<Video> AddVideoAsync(Video video)
    {
        _context.Videos.Add(video);
        await _context.SaveChangesAsync();
        return video;
    }

    public async Task DeleteVideoAsync(Video video)
    {
        _context.Videos.Remove(video);
        await _context.SaveChangesAsync();
    }
}