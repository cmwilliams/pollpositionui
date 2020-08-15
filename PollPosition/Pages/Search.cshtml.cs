using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.CivicInfo.v2.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PollPosition.Models.Representatives;

namespace PollPosition.Pages
{
    public class SearchModel : PageModel
    {
        private readonly IMediator _mediator;

        [BindProperty]
        public RepresentativeQueryResponse Data { get; set; }
        public string Address { get; set; }

        public SearchModel(IMediator mediator) => _mediator = mediator;

        public async Task OnGetAsync(Query query)
        {
            ViewData["Title"] = $"Elected Officials for {query.Address}";
            Data = await _mediator.Send(query);
            Address = query.Address;
        }

        public class Query : IRequest<RepresentativeQueryResponse>
        {
            public string Address { get; set; }
        }

        public class QueryHandler : IRequestHandler<Query, RepresentativeQueryResponse>
        {
            public Task<RepresentativeQueryResponse> Handle(Query request, CancellationToken cancellationToken)
            {
                var representativeQueryResponse = new RepresentativeQueryResponse();

                try
                {
                    if (string.IsNullOrEmpty(request.Address))
                        return Task.FromResult(representativeQueryResponse);

                    var representativeResponse = GetRepresentativesFromApi(request.Address);

                    //Transformation of response into the model
                    var offices = representativeResponse.Offices.ToList();
                    var officials = representativeResponse.Officials.ToArray();
                    var divisions = representativeResponse.Divisions;

                    foreach (var division in divisions)
                    {
                        var divisionOffices = offices.Where(x => x.DivisionId == division.Key);

                        foreach (var office in divisionOffices)
                        {
                            foreach (var indice in office.OfficialIndices)
                            {
                                var foundOfficial = officials[Convert.ToInt64(indice)];
                                var divisionName = GetDivisionName(division.Key, office.Name);

                                var official = new Models.Representatives.Official
                                {
                                    DivisionName = divisionName,
                                    DivisionId = division.Key,
                                    OfficeName = office.Name,
                                    Name = foundOfficial.Name,
                                    Party = GetPartyName(foundOfficial.Party),
                                    PhotoUrl = foundOfficial.PhotoUrl,

                                    Addresses = foundOfficial.Address != null ? foundOfficial.Address.Select(c => new OfficialAddress
                                    {
                                        Street = c.Line1,
                                        City = c.City,
                                        State = c.State,
                                        Zip = c.Zip
                                    }).ToList() : new List<OfficialAddress>(),

                                    PhoneNumbers = foundOfficial.Phones != null ? foundOfficial.Phones.Select(number => new OfficialPhone
                                    {
                                        PhoneNumber = number
                                    }).ToList() : new List<OfficialPhone>(),

                                    Urls = foundOfficial.Urls != null ? foundOfficial.Urls.Select(url => new OfficialUrl
                                    {
                                        Link = url
                                    }).ToList() : new List<OfficialUrl>(),

                                    EmailAddresses = foundOfficial.Emails != null ? foundOfficial.Emails.Select(email => new OfficialEmail
                                    {
                                        EmailAddress = email
                                    }).ToList() : new List<OfficialEmail>(),

                                    Channels = foundOfficial.Channels != null ? foundOfficial.Channels.Select(channel => new OfficialChannel
                                    {
                                        Type = channel.Type,
                                        Id = channel.Id
                                    }).ToList() : new List<OfficialChannel>()
                                };

                                switch (divisionName)
                                {
                                    case Constants.GovtTypes.FEDERAL:
                                        representativeQueryResponse.FederalOfficials.Add(official);
                                        break;
                                    case Constants.GovtTypes.STATE:
                                        representativeQueryResponse.StateOfficials.Add(official);
                                        break;
                                    case Constants.GovtTypes.COUNTY:
                                        representativeQueryResponse.CountyOfficials.Add(official);
                                        break;
                                    case Constants.GovtTypes.LOCAL:
                                        representativeQueryResponse.LocalOfficials.Add(official);
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }

                    var divisionIds = divisions.Select(x => x.Key).Distinct().ToList();
                    representativeQueryResponse.UpcomingElections = GetUpcomingElections(divisionIds);

                    return Task.FromResult(representativeQueryResponse);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }

            private string GetPartyName(string party)
            {
                if (string.IsNullOrEmpty(party))
                    return string.Empty;

                return (party.First().ToString()) switch
                {
                    Constants.PartyTypes.REPUBLICAN => $"({Constants.PartyTypes.REPUBLICAN})",
                    Constants.PartyTypes.DEMOCRAT => $"({Constants.PartyTypes.DEMOCRAT})",
                    Constants.PartyTypes.INDEPENDENT => $"({Constants.PartyTypes.INDEPENDENT})",
                    _ => string.Empty,
                };
            }

            private RepresentativeInfoResponse GetRepresentativesFromApi(string address)
            {
                var initializer = new Google.Apis.Services.BaseClientService.Initializer { ApiKey = Environment.GetEnvironmentVariable("GOOGLE_CIVIC_API_KEY") };
                using var civicInfoService = new Google.Apis.CivicInfo.v2.CivicInfoService(initializer);
                var representativeRequest = civicInfoService.Representatives.RepresentativeInfoByAddress();
                representativeRequest.Address = address;
                return representativeRequest.Execute();
            }

            private ElectionsQueryResponse GetElectionsFromApi()
            {
                var initializer = new Google.Apis.Services.BaseClientService.Initializer { ApiKey = Environment.GetEnvironmentVariable("GOOGLE_CIVIC_API_KEY") };
                using var civicInfoService = new Google.Apis.CivicInfo.v2.CivicInfoService(initializer);
                var electionQueryRequest = civicInfoService.Elections.ElectionQuery();
                return electionQueryRequest.Execute();
            }

            private List<UpcomingElection> GetUpcomingElections(List<string> divisionIds)
            {
                var upcomingElectionInformation = GetElectionsFromApi();

                var upcomingElections = (from election in upcomingElectionInformation.Elections
                                         where election.Id != 2000 && divisionIds.Contains(election.OcdDivisionId)
                                         select new UpcomingElection
                                         {
                                             DivisionId = election.OcdDivisionId,
                                             ElectionDay = election.ElectionDay,
                                             Id = election.Id,
                                             Name = election.Name
                                         }).ToList();

                return upcomingElections;
            }

            private string GetDivisionName(string divisionId, string officeName)
            {
                var federalOffices = new string[] { "U.S. Senator", "U.S. Representative" };
                var statePattern = @"ocd-division\/country:us\/state:(\D{2}$)";
                var slPattern = @"ocd-division\/country:us\/state:(\D{2})\/(sldl:|sldu:)";
                var countyPattern = @"ocd-division\/country:us\/state:\D{2}\/county:\D+";
                var localPattern = @"ocd-division\/country:us\/state:\D{2}\/place:\D+";
                var districtPattern = @"ocd-division\/country:us\/district:\D+";
                var federalPattern = "ocd-division/country:us";
                var cdPattern = @"ocd-division\/country:us\/state:(\D{2})\/cd:";

                if (divisionId == federalPattern || Regex.IsMatch(divisionId, cdPattern) || federalOffices.Contains(officeName))
                {
                    return Constants.GovtTypes.FEDERAL;
                }

                if (Regex.IsMatch(divisionId, statePattern) || Regex.IsMatch(divisionId, slPattern))
                {
                    return Constants.GovtTypes.STATE;
                }

                if (Regex.IsMatch(divisionId, countyPattern))
                {
                    return Constants.GovtTypes.COUNTY;
                }

                if (Regex.IsMatch(divisionId, localPattern) || Regex.IsMatch(divisionId, districtPattern))
                {
                    return Constants.GovtTypes.LOCAL;
                }


                return string.Empty;

            }
        }

    }
}