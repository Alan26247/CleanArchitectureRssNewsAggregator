namespace Infrastructure.Interfaces;

public interface ISqlRangeQueryHelper
{
    /// <summary>
    /// Добавляет массив сущностей в базу данных одним запросом.
    /// Автоматически добавляет дату создания и пользователя
    /// добавившего сущность.
    /// </summary>
    /// <param name="entitys">сущности</param>
    public Task AddRangeAsync<T>(IEnumerable<T> entitys);

    /// <summary>
    /// Обновляет сущности в базе данных одним запросом.
    /// Автоматически обновляет дату обновления сущностей,
    /// а также помечает пользователя обновившего сущность.
    /// </summary>
    /// <param name="entitys">сущности</param>
    public Task UpdateRangeAsync<T>(IEnumerable<T> entitys);

    /// <summary>
    /// Удаляет сущности из базы данных одним запросом
    /// </summary>
    /// <param name="entitys">сущности</param>
    public Task DeleteRangeAsync<T>(IEnumerable<T> entitys);

    /// <summary>
    /// Удаляет сущности из базы данных одним запросом с указанием первичного ключа
    /// </summary>
    /// <param name="entitys">сущности</param>
    public Task DeleteRangeAsync<T>(IEnumerable<T> entitys, string primaryKey);

    /// <summary>
    /// Помечает сущности в базе данных как удаленные.
    /// Автоматически маркирует датой удаления и пользователем удалившем сущности.
    /// </summary>
    /// <param name="entitys">сущности</param>
    public Task SoftDeleteRangeAsync<T>(IEnumerable<T> entitys);
}
