using System.Collections.Generic;

namespace PollPosition.Models.Elections
{
    public class ElectionInformation
    {
        public ElectionInformation()
        {
            OtherElections = new List<Election>();
            PollingLocations = new List<PollingLocation>();
            EarlyVoteSites = new List<EarlyVoteSite>();
            DropOffLocations = new List<DropOffLocation>();
            Contests = new List<Contest>();
            State = new List<State>();
        }


        public string Kind { get; set; }
        public Election Election { get; set; }
        public List<Election> OtherElections { get; set; }
        public Address NormalizedAddress { get; set; }
        public List<PollingLocation> PollingLocations { get; set; }
        public List<EarlyVoteSite> EarlyVoteSites { get; set; }
        public List<DropOffLocation> DropOffLocations { get; set; }
        public List<Contest> Contests { get; set; }
        public List<State> State { get; set; }
        public bool? MailOnly { get; set; }
    }
}
