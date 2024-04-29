using AutoMapper;
using Core.Common.Entities;
using Core.Common.Interfaces.IRepositories;
using Core.Handlers.ChannelHandlers;
using Core.Queries.ChannelQueries;
using Moq;
using Tests.Common.Helpers;

namespace Tests.Tests.ChannelTests
{
    public class GetChannelByIdHandlerTest
    {
        private static IMapper? _mapper;
        private static IChannelRepository? _channelRepository;

        public GetChannelByIdHandlerTest()
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

                var repository = new MockRepository(MockBehavior.Default);

                _channelRepository = repository.Of<IChannelRepository>()
                    .Where(r => r.GetAsync(It.Is<long>(s => s == 1L)) == Task.FromResult(channel))
                    .Where(r => r.GetAsync(It.Is<long>(s => s > 1L)) == Task.FromResult<Channel?>(null))
                    .First();
            }
        }

        [Fact]
        public async void GetChannelById()
        {
            // ----- Arrange -----
            var query = new GetChannelByIdQuery { Id = 1 };

            var handler = new GetChannelByIdHandler(_mapper!, _channelRepository!);

            // ----- Act -----
            var actual = await handler.Handle(query, CancellationToken.None);

            // ----- Assert ------
            Assert.True(actual is not null);
        }

        [Fact]
        public async void GetChannelById_ChannelNotFound()
        {
            // ----- Arrange -----
            var query = new GetChannelByIdQuery { Id = 2 };

            var handler = new GetChannelByIdHandler(_mapper!, _channelRepository!);

            // ----- Act -----
            var actual = await handler.Handle(query, CancellationToken.None);

            // ----- Assert ------
            Assert.True(actual is null);
        }
    }
}