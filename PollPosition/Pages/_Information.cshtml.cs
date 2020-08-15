using Microsoft.AspNetCore.Mvc.RazorPages;
using PollPosition.Models.Elections;

namespace PollPosition.Pages
{
    public class _InformationModel : PageModel
    {
        public ElectionInformation ElectionInformation { get; set; }

        public void OnGet()
        {

        }
    }
}