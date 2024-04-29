namespace Core.Common.Dtos.CommonDtos;

public class DataWithPaginationDto<T> : DataDto<T> where T : class
{
    public DataWithPaginationDto(T data) : base(data) { }
    public DataWithPaginationDto() { }

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 25;
    public int PageCount { get; set; } = 1;
    public int CountItems { get; set; } = 0;
}
