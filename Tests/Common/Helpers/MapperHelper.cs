using AutoMapper;
using Core.Common.MappingProfiles;

namespace Tests.Common.Helpers
{
    public static class MapperHelper
    {
        public static IMapper GetMapper()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ChannelMapperProfile());
                cfg.AddProfile(new NewsMapperProfile());
            });

            return mapperConfiguration.CreateMapper();
        }
    }
}
