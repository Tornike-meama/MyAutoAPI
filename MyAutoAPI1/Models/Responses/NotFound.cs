namespace MyAutoAPI1.Models.Responses
{
    public class NotFound<T> : IComonResponse<T>
    {
        public NotFound(string message)
        {
            Message = message;
            IsError = true;
        }

        public T Data { get; set; }
        public bool IsError { get; set; }
        public string Message { get; set; }
    }
}
