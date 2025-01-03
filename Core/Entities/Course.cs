using Core.Entities;
public class Course
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string CoverImage { get; set; }
    public decimal Price { get; set; }
    public string TeacherId { get; set; } 
    public User Teacher { get; set; }
    public List<Video> Videos { get; set; }
} 