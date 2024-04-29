using Core.Common.Dtos.CommonDtos;
using Core.Common.Entities;
using Core.Common.Interfaces.IRepositories;
using Infrastructure.Data;
using Infrastructure.Helpers;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure.Repositories
{
    public class ChannelRepository : BaseRepository<Channel>, IChannelRepository
    {
        private readonly ISqlRangeQueryHelper _sqlRangeQueryHelper;
        private readonly IConfiguration _configuration;

        public ChannelRepository(IConfiguration configuration, IDbContextFactory<ApplicationDbContext> dbFactory, ISqlRangeQueryHelper sqlRangeQueryHelper) : base(dbFactory)
        {
            _configuration = configuration;
            _sqlRangeQueryHelper = sqlRangeQueryHelper;
        }

        public async Task<DataWithPaginationDto<List<Channel>>> GetChannelsAsync(string? findString, int? pageNumber, int? pageSize)
        {
            using var _context = _dbFactory.CreateDbContext();

            IQueryable<Channel> query = _context.Channels;

            if (string.IsNullOrEmpty(findString) is false) query = query
                    .Where(t => EF.Functions.Like(t.Title.ToLower(), $"%{findString.ToLower()}%") ||
                    EF.Functions.Like(t.Description.ToLower(), $"%{findString.ToLower()}%"));

            var entitys = await query.ToListAsync();

            int count = entitys.Count();

            if (pageNumber is null) pageNumber = 1;
            if (pageSize is null) pageSize = 20;

            int skip = PaginationHelper.GetSkip((int)pageNumber, (int)pageSize);

            entitys = entitys.Skip(skip).Take((int)pageSize).ToList();

            entitys = entitys.OrderBy(e => e.Title).ToList();

            var response = new DataWithPaginationDto<List<Channel>>()
            {
                Data = entitys.ToList(),
                PageNumber = (int)pageNumber,
                PageSize = (int)pageSize,
                PageCount = PaginationHelper.GetPageCount(count, (int)pageSize),
                CountItems = count,
            };

            return response;
        }

        public async Task UpdateChannelNewsAsync(Channel channel)
        {
            using var _context = _dbFactory.CreateDbContext();

            // ----- удаляем просроченные новости из входящего списка -----
            var expiredDate = DateTime.UtcNow.AddDays(-int.Parse(_configuration["NewsLifeTimeDays"]!));

            channel.News = channel.News
                .Where(n => n.PubDate > expiredDate)
                .ToList();

            // ----- удаляем входные новости которые уже имеются в базе данных -----
            var startPubDate = channel.News.Min(n => n.PubDate);

            var news = await _context.News.Where(n => n.PubDate >= startPubDate.AddDays(-1) && n.ChannelId == channel.Id).ToListAsync();

            foreach (var n in news)
            {
                var entityToDelete = channel.News.FirstOrDefault(e => e.Link == n.Link || e.Title == n.Title);
                if (entityToDelete != null) channel.News.Remove(entityToDelete);
            }

            // ----- добавляем новые новости -----
            await _sqlRangeQueryHelper.AddRangeAsync(channel.News);
        }

    }
}
