namespace Core.Common.Dtos.CommonDtos
{
    public class DataDto<T>
    {
        public T Data { get; set; }

        public DataDto() { }

        public DataDto(T data)
        {
            Data = data;
        }
    }
}
