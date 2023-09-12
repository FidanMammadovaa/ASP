using System.ComponentModel.DataAnnotations;

namespace ASP_Project.Areas.Identity.Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string ImageURL { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }

    
}
