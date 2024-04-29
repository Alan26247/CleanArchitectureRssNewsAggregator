using Core.Common.Dtos.CommonDtos;
using Core.Common.Entities;

namespace Core.Common.Interfaces.IRepositories
{
    public interface INewsRepository : IRepository<News>
    {
        /// <summary>
        /// Получить список новостей с учетом фильтров
        /// </summary>
        /// <param name="findString">строка поиска по новостям</param>
        /// <param name="channelId">фильтр по id канала</param>
        /// <param name="pageNumber">номер страницы</param>
        /// <param name="pageSize">количество записей на странице</param>
        /// <returns>возвращает список новостей</returns>
        public Task<DataWithPaginationDto<List<News>>> GetNewsAsync(string? findString, int? channelId, int? pageNumber, int? pageSize);

        /// <summary>
        /// Удалить просроченные новости
        /// </summary>
        public Task ClearOldNewsAsync();
    }
}
