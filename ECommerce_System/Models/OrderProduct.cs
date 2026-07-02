using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ECommerceSystem.Models
{
    public class OrderProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int orderProductId { get; set; } // system generated

        [Required]
        [ForeignKey("Order")]
        public int orderId { get; set; } // foreign key

        [Required]
        [ForeignKey("Product")]
        public int productId { get; set; } // foreign key

        [Required]
        [Range(1, 999)]
        public int quantity { get; set; } // user input

       
    }
}