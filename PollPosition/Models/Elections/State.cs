using System;
using System.Collections.Generic;
using System.Text;

namespace PollPosition.Models.Elections
{
    public class State
    {
        public State()
        {
            Sources = new List<Source>();
            ElectionAdministrationBody = new ElectionAdministrationBody();
            LocalJurisdiction = new LocalJurisdiction();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public ElectionAdministrationBody ElectionAdministrationBody { get; set; }
        public LocalJurisdiction LocalJurisdiction { get; set; }
        public List<Source> Sources { get; set; }
    }
}
