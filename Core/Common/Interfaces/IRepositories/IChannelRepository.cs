using Core.Common.Dtos.CommonDtos;
using Core.Common.Entities;

namespace Core.Common.Interfaces.IRepositories
{
    public interface IChannelRepository : IRepository<Channel>
    {
        /// <summary>
        /// Получить список каналов
        /// </summary>
        /// <param name="findString">строка поиска</param>
        /// <param name="pageNumber">Номер страницы</param>
        /// <param name="pageSize">Количество элементов на странице</param>
        /// <returns>Возвращает список каналов</returns>
        public Task<DataWithPaginationDto<List<Channel>>> GetChannelsAsync(string? findString, int? pageNumber, int? pageSize);

        /// <summary>
        /// Обновить новостную ленту канала
        /// </summary>
        /// <param name="entity">сущность</param>
        public Task UpdateChannelNewsAsync(Channel entity);
    }
}
