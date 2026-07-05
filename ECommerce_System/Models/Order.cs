using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ECommerceSystem.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int orderId { get; set; } // system generated

        [Required]
        [ForeignKey("User")]
        public int userId { get; set; } // foreign key

        [Required]
        public DateTime orderDate { get; set; } // system generated

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [Range(typeof(decimal), "0.00", "999999.99")]
        public decimal totalAmount { get; set; } // calculated

        [Required]
        [MaxLength(30)]
        public string status { get; set; } = "Pending"; // default value

        [Required]
        [MaxLength(300)]
        public string shippingAddress { get; set; } = string.Empty; // user input

        [Required]
        [MaxLength(50)]
        public string paymentMethod { get; set; } = string.Empty; // from list

        public User? User { get; set; } // relationship ==> many orders belong to one user


    }
}
