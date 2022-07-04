namespace MyAutoAPI1.Models
{
	public class ComonResponse<T>
	{
		public bool isError { get; set; }
		public T data { get; set; }
	}
}
