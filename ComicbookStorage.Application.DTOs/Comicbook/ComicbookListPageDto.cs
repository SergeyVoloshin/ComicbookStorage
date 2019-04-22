
namespace ComicbookStorage.Application.DTOs.Comicbook
{
    using System.Collections.Generic;

    public class ComicbookListPageDto
    {
        public ComicbookListPageDto(bool hasMore, IEnumerable<ComicbookListItemDto> comicbooks)
        {
            Comicbooks = comicbooks;
            HasMore = hasMore;
        }

        public IEnumerable<ComicbookListItemDto> Comicbooks { get; private set; }

        public bool HasMore { get; private set; }
    }
}
