using Core.Common.Entities;

namespace Core.Common.Interfaces.IServices
{
    public interface IUpdateChannelNewsService
    {
        /// <summary>
        /// Обновить новостную ленту канала
        /// </summary>
        /// <param name="channel">канал</param>
        /// <returns>Возвращает обновленный канал</returns>
        public Task<Channel> UpdateAsync(Channel channel);
    }
}