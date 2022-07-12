using MyAutoAPI1.Models.Responses;

namespace MyAutoAPI1.Models
{
    public class AuthResponse<T> : IComonResponse<T>
    {
        public AuthResponse(string token, string message)
        {
            IsError = false;
            Message = message;
            Token = token;
        }

        public AuthResponse(string message)
        {
            IsError = true;
            Message = message;
        }

        public bool IsError { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
    }
}
