using System;
using System.Collections.Generic;
using System.Text;

namespace PollPosition.Models.Elections
{
    public class Contest
    {
        public Contest()
        {
            Candidates = new List<Candidate>();
            Sources = new List<Source>();
            Level = new List<Level>();
            Roles = new List<Role>();
            ReferendumBallotResponses = new List<ReferendumBallotResponse>();
        }

        public string Id { get; set; }
        public string Type { get; set; }
        public string PrimaryParty { get; set; }
        public string ElectorateSpecifications { get; set; }
        public string Special { get; set; }
        public string BallotTitle { get; set; }
        public string Office { get; set; }
        public List<Level> Level { get; set; }
        public List<Role> Roles { get; set; }
        public District District { get; set; }
        public long? NumberElected { get; set; }
        public long? NumberVotingFor { get; set; }
        public long? BallotPlacement { get; set; }
        public List<Candidate> Candidates { get; set; }
        public string ReferendumTitle { get; set; }
        public string ReferendumSubtitle { get; set; }
        public string ReferendumUrl { get; set; }
        public string ReferendumBrief { get; set; }
        public string ReferendumText { get; set; }
        public string ReferendumProStatement { get; set; }
        public string ReferendumConStatement { get; set; }
        public string ReferendumPassageThreshold { get; set; }
        public string ReferendumEffectOfAbstain { get; set; }
        public List<ReferendumBallotResponse> ReferendumBallotResponses { get; set; }
        public List<Source> Sources { get; set; }

    }
}
