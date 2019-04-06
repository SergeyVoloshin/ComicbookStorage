

namespace ComicbookStorage.Application.Services.MappingProfiles
{
    using AutoMapper;
    using Domain.Core.Entities;
    using DTOs.Comicbook;

    public class ComicbookProfile : Profile
    {
        public ComicbookProfile()
        {
            CreateMap<Comicbook, ComicbookListDto>();
        }
    }
}
