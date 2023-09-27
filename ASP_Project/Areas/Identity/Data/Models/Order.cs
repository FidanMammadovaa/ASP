using System.ComponentModel.DataAnnotations;

namespace ASP_Project.Areas.Identity.Data.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string OrderStatus { get; set; } 

        public virtual List<OrderDetail>? OrderDetails { get; set; }

    }
}
