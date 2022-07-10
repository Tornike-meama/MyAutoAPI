using System.ComponentModel.DataAnnotations;

namespace MyAutoAPI1.Models
{
    public class Statement
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        [Required]
        public int CurrencyId { get; set; }
        [Required]
        public int Creator { get; set; }

    }
}
