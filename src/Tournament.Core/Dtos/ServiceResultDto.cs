namespace Tournament.Core.Dtos
{
    public class ServiceResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
    public class ServiceResultDto<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }

}
