

namespace ComicbookStorage.Application.Services.Base
{
    using AutoMapper;

    public abstract class ServiceBase
    {
        protected ServiceBase(IMapper mapper)
        {
            Mapper = mapper;
        }

        protected IMapper Mapper { get; }
    }
}
