public class VideoDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Url { get; set; }

    public int IndexInCourse { get; set; }

    public static VideoDto FromVideo(Video video)
    {
        return new VideoDto
        {
            Id = video.Id,
            Title = video.Title,
            Url = video.Url,
            IndexInCourse = video.IndexInCourse
        };
    }
} 

public class VideoAnonymousDto
{
    public int Id { get; set; }
    public string Title { get; set; }

    public int IndexInCourse { get; set; }

    public static VideoAnonymousDto FromVideo(Video video)
    {
        return new VideoAnonymousDto
        {
            Id = video.Id,
            Title = video.Title,
            IndexInCourse = video.IndexInCourse
        };
    }
} 

public class VideoUploadDto
{
    public string Title { get; set; }
    public string File { get; set; }
    public string FileName { get; set; }
}