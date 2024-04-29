using Core.Commands.ChannelCommands;
using Core.Common.Entities;
using Core.Common.Interfaces.IRepositories;
using Core.Handlers.ChannelHandlers;
using Moq;

namespace Tests.Tests.ChannelTests
{
    public class DeleteChannelHandlerTest
    {
        private static IChannelRepository? _channelRepository;

        public DeleteChannelHandlerTest()
        {
            if (_channelRepository == null)
            {
                var channel = new Channel
                {
                    Id = 1,
                    Title = "Test",
                    Description = "Test",
                    Link = "Link",
                    IsFixed = false,
                };

                var protectedChannel = new Channel
                {
                    Id = 2,
                    Title = "Test2",
                    Description = "Test2",
                    Link = "Link",
                    IsFixed = true,
                };

                var repository = new MockRepository(MockBehavior.Default);

                _channelRepository = repository.Of<IChannelRepository>()
                    .Where(r => r.GetAsync(It.Is<long>(s => s == 1L)) == Task.FromResult(channel))
                    .Where(r => r.GetAsync(It.Is<long>(s => s == 2L)) == Task.FromResult(protectedChannel))
                    .Where(r => r.GetAsync(It.Is<long>(s => s > 2L)) == Task.FromResult<Channel?>(null))
                    .First();
            }
        }

        [Fact]
        public async void DeleteChannel()
        {
            // ----- Arrange -----
            var command = new DeleteChannelCommand { Id = 1 };

            var handler = new DeleteChannelHandler(_channelRepository!);

            // ----- Act -----
            await handler.Handle(command, CancellationToken.None);

            // ----- Assert ------
        }

        [Fact]
        public async void DeleteChannel_ChannelNotFound()
        {
            // ----- Arrange -----
            var command = new DeleteChannelCommand { Id = 3 };

            var handler = new DeleteChannelHandler(_channelRepository!);

            // ----- Act -----
            try
            {
                await handler.Handle(command, CancellationToken.None);
            }
            // ----- Assert ------
            catch
            {
                Assert.True(true);
                return;
            }

            Assert.Fail("Cannel found");
        }

        [Fact]
        public async void DeleteProtectedChannel()
        {
            // ----- Arrange -----
            var command = new DeleteChannelCommand { Id = 2 };

            var handler = new DeleteChannelHandler(_channelRepository!);

            // ----- Act -----
            try
            {
                await handler.Handle(command, CancellationToken.None);
            }
            // ----- Assert ------
            catch
            {
                Assert.True(true);
                return;
            }

            Assert.Fail("Protected channel was deleted");
        }
    }
}