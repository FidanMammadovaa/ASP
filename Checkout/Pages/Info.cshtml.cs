using Checkout.Models;
using Checkout.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Checkout.Pages
{
    public class InfoModel : PageModel
    {
        public string Title { get; set; }
        public IMDb IMDbResponse { get; set; } = new();

        private IMDbService _iMDbService;
        public InfoModel(IMDbService iMDbService)
        {
            _iMDbService = iMDbService;
        }


        public async Task<IActionResult> OnGet()
        {
            try
            {
                Title = Request.Query["movieTitle"];
                IMDbResponse = await _iMDbService.GetMovieInfoAsync(Title);
            }
            catch (Exception ex)
            {
                throw new JsonException(ex.Message);
            }
            return Page();
        }
    }
}
