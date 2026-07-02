using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ECommerceSystem.Models
{
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int reviewId { get; set; } // system generated

        [Required]
        [ForeignKey("User")]
        public int userId { get; set; } // foreign key

        [Required]
        [ForeignKey("Product")]
        public int productId { get; set; } // foreign key

        [Required]
        [Range(1, 5)]
        public int rating { get; set; } // user input

        [MaxLength(500)]
        public string? comment { get; set; } // user input, optional

        [Required]
        public DateTime reviewDate { get; set; } // system generated

    }
}
