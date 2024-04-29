using Core.Common.MappingProfiles;

namespace Web.Common.Extensions;

public static class MapperExtentions
{
    public static void AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(
            typeof(ChannelMapperProfile),
            typeof(NewsMapperProfile)
            );
    }
}
