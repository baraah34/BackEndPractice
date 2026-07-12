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

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [Range(typeof(decimal), "0.01", "99999.99")]
        public decimal unitPrice { get; set; } // product price at time of order


        //relations
        public virtual Order? Order { get; set; } // relationship ==> many orderproduct belong to one order

        public virtual Product? Product { get; set; } // relationship ==> many orderproduct belong to one product



    }
}