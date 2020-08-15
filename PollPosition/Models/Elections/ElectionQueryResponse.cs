using System;
using System.Collections.Generic;
using System.Text;

namespace PollPosition.Models.Elections
{
    public class ElectionQueryResponse
    {
        public List<ElectionInformation> Elections { get; set; }

        public ElectionQueryResponse()
        {
            Elections = new List<ElectionInformation>();
        }
    }
}
