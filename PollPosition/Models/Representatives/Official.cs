using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollPosition.Models.Representatives
{
    public class Official
    {
        public string DivisionName { get; set; }
        public string DivisionId { get; set; }
        public string OfficeName { get; set; }
        public string Name { get; set; }
        public string Party { get; set; }
        public string PhotoUrl { get; set; }
        public List<OfficialAddress> Addresses { get; set; }
        public List<OfficialPhone> PhoneNumbers { get; set; }
        public List<OfficialUrl> Urls { get; set; }
        public List<OfficialEmail> EmailAddresses { get; set; }
        public List<OfficialChannel> Channels { get; set; }

        public Official()
        {
            Addresses = new List<OfficialAddress>();
            PhoneNumbers = new List<OfficialPhone>();
            Urls = new List<OfficialUrl>();
            EmailAddresses = new List<OfficialEmail>();
            Channels = new List<OfficialChannel>();
        }
    }
}
