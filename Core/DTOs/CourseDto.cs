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
            Videos = course.Videos.Select(VideoDto.FromVideo).ToList()
        };
    }
} 


public class CourseUpdateDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string CoverImage { get; set; }
    public decimal Price { get; set; }

    public static CourseUpdateDto FromCourse(Course course)
    {
        return new CourseUpdateDto
        {
            Title = course.Title,
            Description = course.Description,
            CoverImage = course.CoverImage,
            Price = course.Price
        };
    }
} 


public class CourseCreateDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string CoverImage { get; set; }
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

