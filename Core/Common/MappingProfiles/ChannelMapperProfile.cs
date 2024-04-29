using AutoMapper;
using Core.Commands.ChannelCommands;
using Core.Common.Dtos.ChannelDtos;
using Core.Common.Dtos.CommonDtos;
using Core.Common.Entities;

namespace Core.Common.MappingProfiles;

public class ChannelMapperProfile : Profile
{
    public ChannelMapperProfile()
    {
        CreateMap<AddChannelCommand, Channel>();

        CreateMap<UpdateChannelCommand, Channel>();

        CreateMap<Channel, ChannelDto>();

        CreateMap<DataWithPaginationDto<List<Channel>>, DataWithPaginationDto<List<ChannelDto>>>();
    }
}
