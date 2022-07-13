namespace MyAutoAPI1.Models.Responses
{
    public class ComonResponse<T> : IComonResponse<T>
    {
        public ComonResponse(T data)
        {
            Data = data;
            IsError = false;
        }
        public string Message { get; set; }
        public bool IsError { get; set; }
        public T Data { get; set; }
    }
}
