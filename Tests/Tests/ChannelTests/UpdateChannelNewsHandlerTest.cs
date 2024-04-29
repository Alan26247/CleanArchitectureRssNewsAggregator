using Core.Commands.ChannelCommands;
using Core.Common.Entities;
using Core.Common.Interfaces.IRepositories;
using Core.Common.Interfaces.IServices;
using Core.Handlers.ChannelHandlers;
using Moq;

namespace Tests.Tests.ChannelTests
{
    public class UpdateChannelNewsHandlerTest
    {
        private static IChannelRepository? _channelRepository;
        private static IUpdateChannelNewsService _updateChannelNewsService;

        public UpdateChannelNewsHandlerTest()
        {
            if (_channelRepository == null)
            {
                var channel1 = new Channel
                {
                    Id = 1,
                    Title = "Test",
                    Description = "Test",
                    Link = "Link",
                    IsFixed = true,
                };

                var repository = new MockRepository(MockBehavior.Default);

                _channelRepository = repository.Of<IChannelRepository>()
                    .Where(r => r.GetAsync(It.Is<long>(s => s == 1L)) == Task.FromResult(channel1))
                    .First();
            }

            if (_updateChannelNewsService == null)
            {
                var channel2 = new Channel
                {
                    Id = 1,
                    Title = "Test",
                    Description = "Test",
                    Link = "Link",
                    IsFixed = true,
                };

                _updateChannelNewsService = Mock.Of<IUpdateChannelNewsService>(d => d.UpdateAsync(It.IsAny<Channel>()) == Task.FromResult(channel2));
            }
        }

        [Fact]
        public async void UpdateChannelNews()
        {
            //// ----- Arrange -----
            //var command = new UpdateChannelsNewsCommand();

            //var handler = new UpdateChannelsNewsHandler(_channelRepository!, _updateChannelNewsService);

            //// ----- Act -----
            //await handler.Handle(command, CancellationToken.None);

            //// ----- Assert ------
            Assert.Fail();
        }
    }
}