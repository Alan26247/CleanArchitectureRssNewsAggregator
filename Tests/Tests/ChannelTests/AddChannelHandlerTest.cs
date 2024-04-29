using AutoMapper;
using Core.Commands.ChannelCommands;
using Core.Common.Entities;
using Core.Common.Interfaces.IRepositories;
using Core.Common.Interfaces.IServices;
using Core.Handlers.ChannelHandlers;
using Moq;
using Tests.Common.Helpers;

namespace Tests.Tests.ChannelTests
{
    public class AddChannelHandlerTest
    {
        private static IMapper? _mapper;
        private static Channel? _channel;

        public AddChannelHandlerTest()
        {
            if (_mapper == null) _mapper = MapperHelper.GetMapper();

            _channel = new Channel
            {
                Id = 1,
                Title = "Test",
                Description = "Test",
                Link = "Link",
                IsFixed = true,
            };
        }

        [Fact]
        public async void AddChannel()
        {
            // ----- Arrange -----
            var expected = 1L;

            var command = new AddChannelCommand
            {
                RssLink = "Link",
            };

            IChannelRepository channelRepository =
                Mock.Of<IChannelRepository>(d => d.AddAsync(It.IsAny<Channel>()) == Task.FromResult(1L));

            IUpdateChannelNewsService updateChannelNewsService =
                Mock.Of<IUpdateChannelNewsService>(d => d.UpdateAsync(It.IsAny<Channel>()) == Task.FromResult(_channel));

            var handler = new AddChannelHandler(_mapper!, channelRepository, updateChannelNewsService);

            // ----- Act -----
            var actual = await handler.Handle(command, CancellationToken.None);

            // ----- Assert ------
            Assert.Equal(expected, actual);
        }
    }
}