using ASP_Project.Areas.Identity.Data.DbContexts;
using ASP_Project.Areas.Identity.Data.Models;
using FluentValidation;
using System.Net;
using System.Text.RegularExpressions;

namespace ASP_Project.Areas.Identity.Data.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        private readonly static string nameMsg;
        private readonly static string imageURLMsg;
        private readonly static string descriptionMsg;

        private readonly UserContext _dbContext;

        public ProductValidator(UserContext userContext)
        {
            _dbContext = userContext;
            RuleFor(p => p.Name).Must(CheckName).WithMessage(nameMsg).NotNull().Length(0, 50);
            RuleFor(p => p.ImageURL).Must(CheckImageURL).NotNull().WithMessage(imageURLMsg);
            RuleFor(p => p.Description).Must(CheckDescription).WithMessage(descriptionMsg).NotNull().Length(0, 100);

        }
        static ProductValidator()
        {
            nameMsg = "Product\'s name must be between 1 and 50 characters and contain only letters!";
            imageURLMsg = "Product\'s image is required!";
            descriptionMsg = "Product\'s description must be between 1 and 100 characters and contain only letters and numbers!" +
                "and special symbols";
        }
        public static bool CheckName(string name)
        {
            Regex re = new(@"^[A-Za-z]");
            return re.IsMatch(name);
        }
        public static bool CheckDescription(string description)
        {
            Regex re = new(@"^[a-zA-Z0-9!.\-;:,\s]+$");
            return re.IsMatch(description);
        }

        public static bool CheckImageURL(string? imageUrl)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(imageUrl);
            request.Method = "HEAD";
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
