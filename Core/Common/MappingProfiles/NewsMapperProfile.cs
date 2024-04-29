using AutoMapper;
using Core.Common.Dtos.CommonDtos;
using Core.Common.Dtos.NewsDtos;
using Core.Common.Entities;

namespace Core.Common.MappingProfiles;

public class NewsMapperProfile : Profile
{
    public NewsMapperProfile()
    {
        CreateMap<News, NewsDto>()
            .ForMember(d => d.ChannelTitle, opt => opt.MapFrom(n => n.Channel.Title));

        CreateMap<DataWithPaginationDto<List<News>>, DataWithPaginationDto<List<NewsDto>>>();
    }
}
