public class Video
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Url { get; set; }
    public int CourseId { get; set; }
    public Course Course { get; set; }
    public int Duration { get; set; }

    public int IndexInCourse { get; set; }
} 