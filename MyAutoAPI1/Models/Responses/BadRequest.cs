namespace MyAutoAPI1.Models.Responses
{
    public class BadRequest<T> : IComonResponse<T>
    {
        public BadRequest(string message)
        {
            Message = message;
            IsError = true;
        }

        public T Data { get; set; }
        public bool IsError { get; set; }
        public string Message { get; set; }
    }
}
