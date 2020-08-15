using System.Collections.Generic;

namespace PollPosition.Models.Representatives
{
    public class RepresentativeQueryResponse
    {
        public List<Official> FederalOfficials { get; set; }
        public List<Official> StateOfficials { get; set; }
        public List<Official> CountyOfficials { get; set; }
        public List<Official> LocalOfficials { get; set; }
        public List<UpcomingElection> UpcomingElections { get; set; }

        public RepresentativeQueryResponse()
        {
            FederalOfficials = new List<Official>();
            StateOfficials = new List<Official>();
            CountyOfficials = new List<Official>();
            LocalOfficials = new List<Official>();
            UpcomingElections = new List<UpcomingElection>();
        }
    }
}
