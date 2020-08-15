using System;
using System.Collections.Generic;
using System.Text;

namespace PollPosition.Models.Elections
{
    public class Election
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string ElectionDay { get; set; }
        public string DivisionId { get; set; }
    }
}
