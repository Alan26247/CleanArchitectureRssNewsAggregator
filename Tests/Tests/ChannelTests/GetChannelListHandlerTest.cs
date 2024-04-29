using AutoMapper;
using Core.Common.Dtos.CommonDtos;
using Core.Common.Entities;
using Core.Common.Interfaces.IRepositories;
using Core.Handlers.ChannelHandlers;
using Core.Queries.ChannelQueries;
using Moq;
using Tests.Common.Helpers;

namespace Tests.Tests.ChannelTests
{
    public class GetChannelListHandlerTest
    {
        private static IMapper? _mapper;
        private static IChannelRepository? _channelRepository;

        public GetChannelListHandlerTest()
        {
            if (_mapper == null) _mapper = MapperHelper.GetMapper();

            if (_channelRepository == null)
            {
                var channel = new Channel
                {
                    Id = 1,
                    Title = "Test",
                    Description = "Test",
                    Link = "Link",
                    IsFixed = true,
                };

                var responseCount1 = new DataWithPaginationDto<List<Channel>>
                {
                    Data = new List<Channel> { channel },
                    PageNumber = 1,
                    PageSize = 20,
                    PageCount = 1,
                    CountItems = 1,
                };

                var repository = new MockRepository(MockBehavior.Default);

                _channelRepository = repository.Of<IChannelRepository>()
                    .Where(r => r.GetChannelsAsync(It.Is<string?>(s => s == null), 1, 20) == Task.FromResult(responseCount1))
                    .First();
            }
        }

        [Fact]
        public async void GetChannelList()
        {
            // ----- Arrange -----
            var query = new GetChannelListQuery
            {
                FindString = null,
                PageNumber = 1,
                PageSize = 20,
            };

            var handler = new GetChannelListHandler(_mapper!, _channelRepository!);

            // ----- Act -----
            var actual = await handler.Handle(query, CancellationToken.None);

            // ----- Assert ------
            Assert.True(actual.Data.Count() == 1);
        }
    }
}