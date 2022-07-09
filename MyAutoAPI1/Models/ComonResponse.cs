namespace MyAutoAPI1.Models
{
	public class ComonResponse<T>
	{
        public ComonResponse(T data)
        {
            if(data == null)
            {
                IsError = true;
                Message = "Cann't find Item";
                return;
            }
            this.IsError = false;
            this.Data = data;
        }
        public ComonResponse(string message)
        {
            this.IsError = true;
            this.Message = message;
        }

        public string Message { get; set; }
        public bool IsError { get; set; }
		public T Data { get; set; }
	}
}
