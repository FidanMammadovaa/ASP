using System.ComponentModel.DataAnnotations;

namespace ASP_Project.Areas.Identity.Data.Models
{
    public class PaymentDetail
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public decimal Amount { get; set; }

        public string PaymentMethod { get; set; } 

    }

}
