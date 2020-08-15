using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PollPosition.Pages
{
    public class _SearchbarModel : PageModel
    {
        public string Address { get; set; }
        public void OnGet()
        {

        }
    }
}