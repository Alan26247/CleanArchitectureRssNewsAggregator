namespace Core.Common.Interfaces.IRepositories;

public interface IRepository<T>
{
    /// <summary>
    /// Получить сущность по ID
    /// </summary>
    /// <param name="id">id</param>
    /// <returns>Возвращает сущность либо null если сущность не найдена</returns>
    public Task<T?> GetAsync(long id);

    /// <summary>
    /// Получить список сущностей
    /// </summary>
    /// <returns>Возвращает список сущностей</returns>
    public Task<List<T>> GetListAsync();

    /// <summary>
    /// Добавить сущность в базу данных
    /// </summary>
    /// <param name="entity">сущность</param>
    /// <returns>Возвращает id добавленной сущности</returns>
    public Task<long> AddAsync(T entity);

    /// <summary>
    /// Обновить сущность
    /// </summary>
    /// <param name="entity">сущность</param>
    public Task UpdateAsync(T entity);

    /// <summary>
    /// Удалить сущность
    /// </summary>
    /// <param name="entity">сущность</param>
    public Task DeleteAsync(T entity);

    /// <summary>
    /// Получить количество сущностей
    /// </summary>
    /// <returns>Возвращает количество сущностей в базе данных</returns>
    public Task<int> GetCountAsync();
}