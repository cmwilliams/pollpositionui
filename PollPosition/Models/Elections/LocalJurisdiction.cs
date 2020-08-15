using System;
using System.Collections.Generic;
using System.Text;

namespace PollPosition.Models.Elections
{
    public class LocalJurisdiction
    {
        public LocalJurisdiction()
        {
            Sources = new List<Source>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public ElectionAdministrationBody ElectionAdministrationBody { get; set; }
        public List<Source> Sources { get; set; }
    }
}
