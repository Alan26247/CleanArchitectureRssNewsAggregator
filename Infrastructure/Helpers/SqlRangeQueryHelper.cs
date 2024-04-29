using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Infrastructure.Helpers;

public class SqlRangeQueryHelper : ISqlRangeQueryHelper
{
    private readonly ILogger<SqlRangeQueryHelper> _logger;
    private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;
    public SqlRangeQueryHelper(ILogger<SqlRangeQueryHelper> logger, IDbContextFactory<ApplicationDbContext> dbFactory)
    {
        _logger = logger;
        _dbFactory = dbFactory;
    }

    public async Task AddRangeAsync<T>(IEnumerable<T> entitys)
    {
        using var _context = _dbFactory.CreateDbContext();

        if (entitys == null || entitys.Any() is false) return;

        var firstEntity = entitys.First();

        var tableName = GetTableName(firstEntity!, _context);

        Type objectType = firstEntity!.GetType();

        var descriptions = GetDescriptions(entitys);

        try
        {
            var query = new StringBuilder($"INSERT INTO {tableName} (");

            foreach (var description in descriptions)
            {
                if (description.Key == "Id" || description.Key == "id") continue;

                query.Append($"\"{description.Value}\",");
            }

            query.Remove(query.Length - 1, 1);
            query.Append($") VALUES ");

            // ----- получаем массив полей сущности -----
            var properties = objectType.GetProperties();

            foreach (var entity in entitys)
            {
                query.Append($"(");

                foreach (var description in descriptions)
                {
                    if (description.Key == "Id" || description.Key == "id") continue;

                    if (description.Key == "CreatedAt")
                    {
                        query.Append($"{GetSqlDateTime(DateTime.UtcNow)},");
                        continue;
                    }

                    var property = properties.First(f => f.Name == description.Key);
                    query.Append($"{GetSQLValue(property, entity)},");
                }

                query.Remove(query.Length - 1, 1);
                query.Append($"),");
            }

            query.Remove(query.Length - 1, 1);
            query.Append($";");

            var test = query.ToString();

            await _context.Database.ExecuteSqlRawAsync(query.ToString());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task UpdateRangeAsync<T>(IEnumerable<T> entitys)
    {
        using var _context = _dbFactory.CreateDbContext();

        if (entitys == null || entitys.Any() is false) return;

        var firstEntity = entitys.First();

        var tableName = GetTableName(firstEntity!, _context);

        Type objectType = firstEntity!.GetType();

        var descriptions = GetDescriptions(entitys);

        try
        {
            var query = new StringBuilder();

            var properties = objectType.GetProperties();

            foreach (var entity in entitys)
            {
                query.Append($"UPDATE {tableName} SET ");

                foreach (var description in descriptions)
                {
                    if (description.Key == "Id" || description.Key == "id" ||
                        description.Key == "ID" || description.Key == "CreatedAt" ||
                        description.Key == "CreatedBy") continue;

                    query.Append($"\"{description.Value}\" = ");

                    if (description.Key == "UpdatedAt")
                    {
                        query.Append($"{GetSqlDateTime(DateTime.UtcNow)},");
                        continue;
                    }

                    var property = properties.First(f => f.Name == description.Key);
                    query.Append($"{GetSQLValue(property, entity)},");
                }

                query.Remove(query.Length - 1, 1);

                var idProperty = properties.First(f => f.Name == "Id");
                query.Append($" WHERE {descriptions["Id"]} = {GetSQLValue(idProperty, entity)};");
            }

            await _context.Database.ExecuteSqlRawAsync(query.ToString());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task DeleteRangeAsync<T>(IEnumerable<T> entitys)
    {
        using var _context = _dbFactory.CreateDbContext();

        if (entitys == null || entitys.Any() is false) return;

        var firstEntity = entitys.First();

        var tableName = GetTableName(firstEntity!, _context);

        Type objectType = firstEntity!.GetType();

        try
        {
            var query = new StringBuilder();

            query.Append($"DELETE FROM {tableName} WHERE \"id\" IN (");

            var properties = objectType.GetProperties();

            foreach (var entity in entitys)
            {
                var idProperty = properties.First(f => f.Name == "Id");
                query.Append($"{GetSQLValue(idProperty, entity)},");
            }

            query.Remove(query.Length - 1, 1);
            query.Append($")");

            await _context.Database.ExecuteSqlRawAsync(query.ToString());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task DeleteRangeAsync<T>(IEnumerable<T> entitys, string primaryKey)
    {
        using var _context = _dbFactory.CreateDbContext();

        if (entitys == null || entitys.Any() is false) return;

        var firstEntity = entitys.First();

        var tableName = GetTableName(firstEntity!, _context);

        Type objectType = firstEntity!.GetType();

        try
        {
            var query = new StringBuilder();

            query.Append($"DELETE FROM {tableName} WHERE \"{primaryKey}\" IN (");

            var properties = objectType.GetProperties();

            foreach (var entity in entitys)
            {
                var idProperty = properties.First(f => f.Name == "Id");
                query.Append($"{GetSQLValue(idProperty, entity)},");
            }

            query.Remove(query.Length - 1, 1);
            query.Append($")");

            await _context.Database.ExecuteSqlRawAsync(query.ToString());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task SoftDeleteRangeAsync<T>(IEnumerable<T> entitys)
    {
        using var _context = _dbFactory.CreateDbContext();

        if (entitys == null || entitys.Any() is false) return;

        var firstEntity = entitys.First();

        var tableName = GetTableName(firstEntity!, _context);

        Type objectType = firstEntity!.GetType();

        try
        {
            var query = new StringBuilder();

            query.Append($"UPDATE {tableName} SET is_active = false, deleted_at = {GetSqlDateTime(DateTime.UtcNow)}  WHERE id IN (");

            var properties = objectType.GetProperties();

            foreach (var entity in entitys)
            {
                var idProperty = properties.First(f => f.Name == "Id");
                query.Append($"{GetSQLValue(idProperty, entity)},");
            }

            query.Remove(query.Length - 1, 1);
            query.Append($")");

            await _context.Database.ExecuteSqlRawAsync(query.ToString());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }


    private string GetTableName(object entity, ApplicationDbContext _context)
    {
        var entitySchema = _context.Entry(entity).Metadata;

        string shemaName = entitySchema.GetSchema()!;
        string tableName = entitySchema.GetTableName()!;

        //return $"\"{shemaName}\".\"{tableName}\"";
        return $"\"{tableName}\"";
    }

    private static Dictionary<string, string> GetDescriptions<T>(IEnumerable<T> entities)
    {
        Type type = typeof(T);
        PropertyInfo[] properties = type.GetProperties();

        Dictionary<string, string> Descriptions = new Dictionary<string, string>();

        foreach (PropertyInfo propertyInfo in properties)
        {
            if (propertyInfo.PropertyType == typeof(bool) || propertyInfo.PropertyType == typeof(bool?) ||
                propertyInfo.PropertyType == typeof(short) || propertyInfo.PropertyType == typeof(short?) ||
                propertyInfo.PropertyType == typeof(ushort) || propertyInfo.PropertyType == typeof(ushort?) ||
                propertyInfo.PropertyType == typeof(int) || propertyInfo.PropertyType == typeof(int?) ||
                propertyInfo.PropertyType == typeof(uint) || propertyInfo.PropertyType == typeof(uint?) ||
                propertyInfo.PropertyType == typeof(long) || propertyInfo.PropertyType == typeof(long?) ||
                propertyInfo.PropertyType == typeof(ulong) || propertyInfo.PropertyType == typeof(ulong?) ||
                propertyInfo.PropertyType == typeof(float) || propertyInfo.PropertyType == typeof(float?) ||
                propertyInfo.PropertyType == typeof(double) || propertyInfo.PropertyType == typeof(double?) ||
                propertyInfo.PropertyType == typeof(Guid) || propertyInfo.PropertyType == typeof(Guid?) ||
                propertyInfo.PropertyType == typeof(DateTime) || propertyInfo.PropertyType == typeof(DateTime?) ||
                propertyInfo.PropertyType == typeof(DateTimeOffset) || propertyInfo.PropertyType == typeof(DateTimeOffset?) ||
                propertyInfo.PropertyType == typeof(List<int>) || propertyInfo.PropertyType == typeof(List<long>) ||
                propertyInfo.PropertyType == typeof(List<string>) || propertyInfo.PropertyType == typeof(List<Guid>) ||
                propertyInfo.PropertyType == typeof(string))
            {
                // ----- поиск названия таблицы по атрибутам -----
                ColumnAttribute columnAttribute = propertyInfo.GetCustomAttribute<ColumnAttribute>();

                //// ----- без преобразования в snake case -----
                //if (columnAttribute != null) Descriptions.Add(propertyInfo.Name, columnAttribute.Name!);
                //else Descriptions.Add(propertyInfo.Name, propertyInfo.Name);

                // ----- преобразование в snake case -----
                Descriptions.Add(propertyInfo.Name, Regex.Replace(propertyInfo.Name, @"(\p{Ll})(\p{Lu})", "$1_$2").ToLower());
            }
        }

        return Descriptions;
    }

    private static string GetSQLValue(PropertyInfo propertyInfo, object? entity)
    {
        if (entity == null) return "NULL";

        if (propertyInfo.PropertyType == typeof(bool) || propertyInfo.PropertyType == typeof(bool?) ||
            propertyInfo.PropertyType == typeof(short) || propertyInfo.PropertyType == typeof(short?) ||
            propertyInfo.PropertyType == typeof(ushort) || propertyInfo.PropertyType == typeof(ushort?) ||
            propertyInfo.PropertyType == typeof(int) || propertyInfo.PropertyType == typeof(int?) ||
            propertyInfo.PropertyType == typeof(uint) || propertyInfo.PropertyType == typeof(uint?) ||
            propertyInfo.PropertyType == typeof(long) || propertyInfo.PropertyType == typeof(long?) ||
            propertyInfo.PropertyType == typeof(ulong) || propertyInfo.PropertyType == typeof(ulong?) ||
            propertyInfo.PropertyType == typeof(float) || propertyInfo.PropertyType == typeof(float?) ||
            propertyInfo.PropertyType == typeof(double) || propertyInfo.PropertyType == typeof(double?))
        {
            var value = propertyInfo.GetValue(entity);

            if (value == null) return "NULL";
            else return value!.ToString()!.ToLower().Replace(",", ".");
        }

        if (propertyInfo.PropertyType == typeof(Guid) || propertyInfo.PropertyType == typeof(Guid?))
        {
            var value = propertyInfo.GetValue(entity);

            if (value == null) return "NULL";
            else return $"\'{value!.ToString()!}\'";
        }

        if (propertyInfo.PropertyType == typeof(DateTimeOffset) || propertyInfo.PropertyType == typeof(DateTimeOffset?))
        {
            return GetSqlDateTime(propertyInfo.GetValue(entity)! as DateTimeOffset?);
        }

        if (propertyInfo.PropertyType == typeof(DateTime) || propertyInfo.PropertyType == typeof(DateTime?))
        {
            return GetSqlDateTime(propertyInfo.GetValue(entity)! as DateTime?);
        }

        if (propertyInfo.PropertyType == typeof(List<int>))
        {
            List<int> value = (List<int>)propertyInfo.GetValue(entity)!;

            if (value == null || value.Any() is false) return "ARRAY[]::integer[]";
            else return "ARRAY" + JsonSerializer.Serialize(value!);
        }

        if (propertyInfo.PropertyType == typeof(List<long>))
        {
            List<long> value = (List<long>)propertyInfo.GetValue(entity)!;

            if (value == null || value.Any() is false) return "ARRAY[]::bigint[]";
            else return "ARRAY" + JsonSerializer.Serialize(value!);
        }

        if (propertyInfo.PropertyType == typeof(List<string>))
        {
            List<string> value = (List<string>)propertyInfo.GetValue(entity)!;

            if (value == null || value.Any() is false) return "ARRAY[]::text[]";
            else return "ARRAY" + JsonSerializer.Serialize(value!);
        }

        if (propertyInfo.PropertyType == typeof(List<Guid>))
        {
            List<Guid> value = (List<Guid>)propertyInfo.GetValue(entity)!;

            if (value == null || value.Any() is false) return "ARRAY[]::text[]";
            else return "ARRAY" + JsonSerializer.Serialize(value!);
        }

        if (propertyInfo.GetValue(entity) is not null)
            return $"\'{((string)propertyInfo.GetValue(entity)!)!.Replace("'", "\"")}\'";
        else return "NULL";
    }

    private static string GetSqlDateTime(DateTime? dateTime)
    {
        return dateTime != null ? $"\'{((DateTime)dateTime).ToString("yyyy-MM-dd HH:mm:ss")}\'" : "NULL";
    }

    private static string GetSqlDateTime(DateTimeOffset? dateTime)
    {
        return dateTime != null ? $"\'{((DateTimeOffset)dateTime).DateTime.ToString("yyyy-MM-dd HH:mm:ss.fff")}\'" : "NULL";
    }
}
