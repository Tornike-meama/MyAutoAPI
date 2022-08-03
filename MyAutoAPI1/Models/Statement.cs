using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAutoAPI1.Models
{
    public class Statement
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        [ForeignKey("Currency")]
        [Required]
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }
        [ForeignKey("Creator")]
        public string CreatorId { get; set; }
        [Required]
        public IdentityUser Creator { get; set; }

    }
}
