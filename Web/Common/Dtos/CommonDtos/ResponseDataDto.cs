namespace Web.Common.Dtos.CommonDtos
{
    public class ResponseDataDto<T> : ResponseDto
    {
        public T? Data { get; set; }
    }
}
