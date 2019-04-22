
namespace ComicbookStorage.Application.DTOs.Comicbook
{
    public class ComicbookListItemDto
    {
        public ComicbookListItemDto(string coverUrl)
        {
            CoverUrl = coverUrl;
        }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public string CoverUrl { get; private set; }

        public string Url { get; private set; }
    }
}
