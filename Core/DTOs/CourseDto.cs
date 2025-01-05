using Core.Entities;

public class CourseDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string CoverImage { get; set; }
    public decimal Price { get; set; }
    public List<VideoDto> Videos { get; set; }

    public static CourseDto FromCourse(Course course)
    {
        return new CourseDto
        {
            Id = course.Id,
            Title = course.Title,
            Description = course.Description,
            CoverImage = course.CoverImage,
            Price = course.Price,
            Videos = course.Videos.Select(VideoDto.FromVideo).OrderBy(v => v.IndexInCourse).ToList() 
        };
    }
} 

public class CourseAnonymousDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string CoverImage { get; set; }
    public decimal Price { get; set; }
    public List<VideoAnonymousDto> Videos { get; set; } = new(); // Varsayılan boş liste
    public String Teacher { get; set; }

    public static CourseAnonymousDto FromCourse(Course course)
    {
        return new CourseAnonymousDto
        {
            Id = course.Id,
            Title = course.Title,
            Description = course.Description,
            CoverImage = course.CoverImage,
            Price = course.Price,

            Videos = course.Videos != null
                ? course.Videos.Select(VideoAnonymousDto.FromVideo).OrderBy(v => v.IndexInCourse).ToList()
                : new List<VideoAnonymousDto>(),
            Teacher = course.Teacher.Name + " " + course.Teacher.Surname
        };
    }
}
public class CourseUpdateDto
{
    public string Title { get; set; } 
    public string Description { get; set; } 

    
    public decimal Price { get; set; } 
}

public class CoursePhotoDto
{
    public string? CoverImage { get; set; } 
    public string? CoverImageName { get; set; } 
}

public class CourseCreateDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string CoverImage { get; set; }
    public string CoverImageName { get; set; }
    public decimal Price { get; set; }
    public List<VideoDto> Videos { get; set; }

    public static CourseCreateDto FromCourse(Course course)
    {
        return new CourseCreateDto
        {
            Title = course.Title,
            Description = course.Description,
            CoverImage = course.CoverImage,
            Price = course.Price,
            Videos = course.Videos.Select(VideoDto.FromVideo).ToList()
        };
    }
} 

