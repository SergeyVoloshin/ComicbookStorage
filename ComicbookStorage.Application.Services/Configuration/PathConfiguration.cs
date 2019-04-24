
namespace ComicbookStorage.Application.Services.Configuration
{
    public interface IPathConfiguration
    {
        string ComicbookImages { get; set; }
        string SmallCoverName { get; set; }
    }

    public class PathConfiguration : IPathConfiguration
    {
        public string ComicbookImages { get; set; }
        public string SmallCoverName { get; set; }
    }
}
