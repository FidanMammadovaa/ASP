using ASP_Project.Areas.Identity.Data.Models;
using FluentValidation;
using System.Text.RegularExpressions;

namespace ASP_Project.Areas.Identity.Data.Validators
{
    public class CategoryValidator: AbstractValidator<Category>
    {
        private readonly static string name;
        private readonly static string description;



        public CategoryValidator()
        {
            RuleFor(c => c.Name).Must(CheckName).WithMessage(name);
            RuleFor(c => c.Description).Must(CheckDescription).WithMessage(description);
        }

        static CategoryValidator()
        {
            name = "Category name must be between 1 and 30 characters and contain only letters.";
            description = "Category description must be between 1 and 100 characters and contain only letters and numbers " +
                "and special symbols";
        }


        public static bool CheckName(string? name)
        {
            Regex re = new(@"^[A-Z][a-z]");
            return name != null && re.IsMatch(name) && name.Length > 0 && name.Length <= 30;

        }


        public static bool CheckDescription(string? description)
        {
            Regex re = new(@"^[a-zA-Z0-9!.\-;:,\s]+$");
            return description != null && re.IsMatch(description) && description.Length > 0 && description.Length < 100;
        }


    }
}
