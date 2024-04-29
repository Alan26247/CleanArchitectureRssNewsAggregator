using AutoMapper;
using Core.Common.Dtos.CommonDtos;
using Core.Common.Entities;
using Core.Common.Interfaces.IRepositories;
using Core.Handlers.NewsHandlers;
using Core.Queries.NewsQueries;
using Moq;
using Tests.Common.Helpers;

namespace Tests.Tests.NewsTests
{
    public class GetNewsListHandlerTest
    {
        private static IMapper? _mapper;
        private static INewsRepository? _newsRepository;

        public GetNewsListHandlerTest()
        {
            if (_mapper == null) _mapper = MapperHelper.GetMapper();

            if (_newsRepository == null)
            {
                var channel = new News
                {
                    Id = 1,
                    Title = "Test",
                    Description = "Test",
                    Link = "Link",
                    ChannelId = 1,
                    PubDate = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow,
                };

                var responseCount1 = new DataWithPaginationDto<List<News>>
                {
                    Data = new List<News> { channel },
                    PageNumber = 1,
                    PageSize = 20,
                    PageCount = 1,
                    CountItems = 1,
                };

                var repository = new MockRepository(MockBehavior.Default);

                _newsRepository = repository.Of<INewsRepository>()
                    .Where(r => r.GetNewsAsync(
                        It.Is<string?>(s => s == null),
                        It.Is<int?>(s => s == null),
                        1,
                        20) == Task.FromResult(responseCount1))
                    .First();
            }
        }

        [Fact]
        public async void GetChannelList()
        {
            // ----- Arrange -----
            var query = new GetNewsListQuery
            {
                FindString = null,
                ChannelId = null,
                PageNumber = 1,
                PageSize = 20,
            };

            var handler = new GetNewsListHandler(_mapper!, _newsRepository!);

            // ----- Act -----
            var actual = await handler.Handle(query, CancellationToken.None);

            // ----- Assert ------
            Assert.True(actual.Data.Count() == 1);
        }
    }
}