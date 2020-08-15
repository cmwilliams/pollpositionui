using System;
using System.Collections.Generic;
using System.Text;

namespace PollPosition.Models.Elections
{
    public class Location
    {
        public Location()
        {
            Sources = new List<Source>();
        }

        public string Id { get; set; }
        public Address Address { get; set; }
        public string Notes { get; set; }
        public string PollingHours { get; set; }
        public string Name { get; set; }
        public string VoterServices { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public List<Source> Sources { get; set; }

    }
}
