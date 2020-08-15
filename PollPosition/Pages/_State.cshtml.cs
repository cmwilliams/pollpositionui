using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PollPosition.Models.Elections;

namespace PollPosition.Pages
{
    public class _StateModel : PageModel
    {
        public List<State> States { get; set; }

        public void OnGet()
        {

        }
    }
}