using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PollPosition.Models.Elections;

namespace PollPosition.Pages
{
    public class _ContestsModel : PageModel
    {
        public List<Contest> Contests { get; set; }

        public void OnGet()
        {

        }
    }
}