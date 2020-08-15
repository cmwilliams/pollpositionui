using System.Collections.Generic;

namespace PollPosition.Models.Elections
{
    public class ElectionAdministrationBody
    {
        public ElectionAdministrationBody()
        {
            VoterServices = new List<VoterService>();
            ElectionOfficials = new List<ElectionOfficial>();
        }

        public string Name { get; set; }
        public string ElectionInfoUrl { get; set; }
        public string ElectionRegistrationUrl { get; set; }
        public string ElectionRegistrationConfirmationUrl { get; set; }
        public string AbsenteeVotingInfoUrl { get; set; }
        public string VotingLocationFinderUrl { get; set; }
        public string BallotInfoUrl { get; set; }
        public string ElectionRulesUrl { get; set; }
        public List<VoterService> VoterServices { get; set; }
        public string HoursOfOperation { get; set; }
        public Address CorrespondenceAddress { get; set; }
        public Address PhysicalAddress { get; set; }
        public List<ElectionOfficial> ElectionOfficials { get; set; }
    }
}
