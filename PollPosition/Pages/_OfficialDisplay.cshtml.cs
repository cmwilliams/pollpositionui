using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PollPosition.Models.Representatives;

namespace PollPosition.Pages
{
    public class _OfficialDisplayModel : PageModel
    {
        public string SectionTitle { get; set; }
        public List<Official> Officials { get; set; }


        public void OnGet()
        {

        }
    }
}