using System;
using System.Collections.Generic;
using System.Text;

namespace PollPosition.Models.Elections
{
    public class Candidate
    {
        public Candidate()
        {
            Channels = new List<Channel>();
        }

        public string Name { get; set; }
        public string Party { get; set; }
        public string CandidateUrl { get; set; }
        public string Phone { get; set; }
        public string PhotoUrl { get; set; }
        public string Email { get; set; }
        public long? OrderOnBallot { get; set; }
        public List<Channel> Channels { get; set; }
    }
}
