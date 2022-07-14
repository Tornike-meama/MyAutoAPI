using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAutoAPI1.Models
{
    public class Statement
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        [Required]
        [ForeignKey("CurrencyId")]
        public int CurrencyId { get; set; }
        [Required]
        public string Creator { get; set; }

    }
}
