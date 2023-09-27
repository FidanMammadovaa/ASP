using System.ComponentModel.DataAnnotations;

namespace ASP_Project.Areas.Identity.Data.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }


    }

}
