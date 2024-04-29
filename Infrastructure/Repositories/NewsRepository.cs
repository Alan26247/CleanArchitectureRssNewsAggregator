using Core.Common.Dtos.CommonDtos;
using Core.Common.Entities;
using Core.Common.Interfaces.IRepositories;
using Infrastructure.Data;
using Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure.Repositories
{
    public class NewsRepository : BaseRepository<News>, INewsRepository
    {
        private readonly IConfiguration _configuration;

        public NewsRepository(IConfiguration configuration, IDbContextFactory<ApplicationDbContext> dbFactory) : base(dbFactory)
        {
            _configuration = configuration;
        }


        public async Task<DataWithPaginationDto<List<News>>> GetNewsAsync(string? findString, int? channelId, int? pageNumber, int? pageSize)
        {
            using var _context = _dbFactory.CreateDbContext();

            IQueryable<News> query = _context.News.Include(n => n.Channel);

            if (channelId != null && channelId != 0) query = query.Where(e => e.ChannelId == channelId);

            if (string.IsNullOrEmpty(findString) is false) query = query
                    .Where(t => EF.Functions.Like(t.Title.ToLower(), $"%{findString.ToLower()}%") ||
                    EF.Functions.Like(t.Description.ToLower(), $"%{findString.ToLower()}%"));

            query.Include(n => n.Channel);

            var entitys = await query.ToListAsync();

            entitys = entitys.OrderByDescending(e => e.PubDate).ToList();

            int count = entitys.Count();

            if (pageNumber is null) pageNumber = 1;
            if (pageSize is null) pageSize = 20;

            int skip = PaginationHelper.GetSkip((int)pageNumber, (int)pageSize);

            entitys = entitys.Skip(skip).Take((int)pageSize).ToList();

            var response = new DataWithPaginationDto<List<News>>()
            {
                Data = entitys.ToList(),
                PageNumber = (int)pageNumber,
                PageSize = (int)pageSize,
                PageCount = PaginationHelper.GetPageCount(count, (int)pageSize),
                CountItems = count,
            };

            return response;
        }

        public async Task ClearOldNewsAsync()
        {
            using var _context = _dbFactory.CreateDbContext();

            var expiredDate = DateTime.UtcNow.AddDays(-int.Parse(_configuration["NewsLifeTimeDays"]!));

            await _context.Database.ExecuteSqlRawAsync($"""
                DELETE 
                FROM News 
                WHERE pub_date < '{expiredDate:yyyy-MM-dd HH:mm:ss}';
                """);
        }
    }
}
