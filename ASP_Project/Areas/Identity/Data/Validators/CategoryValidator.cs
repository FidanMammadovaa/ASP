using ASP_Project.Areas.Identity.Data.DbContexts;
using ASP_Project.Areas.Identity.Data.Models;
using FluentValidation;
using System.Text.RegularExpressions;

namespace ASP_Project.Areas.Identity.Data.Validators
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        private readonly static string nameMsg;
        private readonly static string descriptionMsg;
        private readonly static string uniqueNameMsg;

        private readonly UserContext _dbContext;


        public CategoryValidator(UserContext userContext)
        {
            _dbContext = userContext;
            RuleFor(c => c.Name).Must(CheckName).WithMessage(nameMsg);
            RuleFor(c => c.Description).Must(CheckDescription).WithMessage(descriptionMsg);
            RuleFor(c => c.Name).Must(CheckUniqueName).WithMessage(uniqueNameMsg);
        }

        static CategoryValidator()
        {
            nameMsg = "Category name must be between 1 and 30 characters and contain only letters.";
            descriptionMsg = "Category description must be between 1 and 100 characters and contain only letters and numbers " +
                "and special symbols";
            uniqueNameMsg = "Category name must be unique!";
        }


        public static bool CheckName(string? name)
        {
            Regex re = new(@"^[A-Za-z]");
            return name != null && re.IsMatch(name) && name.Length > 0 && name.Length <= 30;

        }


        public static bool CheckDescription(string? description)
        {
            Regex re = new(@"^[a-zA-Z0-9!.\-;:,\s]+$");
            return description != null && re.IsMatch(description) && description.Length > 0 && description.Length < 100;
        }

        private bool CheckUniqueName(string name)
        {
            return !_dbContext.Categories.Any(c => c.Name == name);
        }
    }
}
