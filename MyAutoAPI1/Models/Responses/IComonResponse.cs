namespace MyAutoAPI1.Models.Responses
{
    public interface IComonResponse<T>
    {
        public bool IsError { get; set; }
        public string Message { get; set; }

    }
}
