using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Google.Apis.CivicInfo.v2.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PollPosition.Models.Elections;


namespace PollPosition.Pages
{
    public class ElectionsModel : PageModel
    {
        private readonly IMediator _mediator;

        [BindProperty]
        public ElectionQueryResponse Data { get; set; }

        public ElectionsModel(IMediator mediator) => _mediator = mediator;

        public async Task OnGetAsync(Query query)
        {
            ViewData["Title"] = $"Elections for {query.Address}";
            Data = await _mediator.Send(query);
        }

        public class Query : IRequest<ElectionQueryResponse>
        {
            public string Address { get; set; }
            public IEnumerable<string> ElectionIds { get; set; }
        }

        public class QueryHandler : IRequestHandler<Query, ElectionQueryResponse>
        {
            private readonly IMapper _mapper;

            public QueryHandler(IMapper mapper)
            {
                _mapper = mapper;
            }

            private VoterInfoResponse GetElectionInformationFromApi(string address, long electionId)
            {
                var initializer = new Google.Apis.Services.BaseClientService.Initializer { ApiKey = Environment.GetEnvironmentVariable("GOOGLE_CIVIC_API_KEY") };

                using var civicInfoService = new Google.Apis.CivicInfo.v2.CivicInfoService(initializer);
                try
                {
                    var voterInfoQueryRequest = civicInfoService.Elections.VoterInfoQuery(address);
                    voterInfoQueryRequest.ElectionId = electionId;
                    return voterInfoQueryRequest.Execute();
                }
                catch (Exception)
                {
                    return null;
                }
            }


            public Task<ElectionQueryResponse> Handle(Query request, CancellationToken cancellationToken)
            {
                var electionQueryResponse = new ElectionQueryResponse();

                try
                {
                    foreach (var electionId in request.ElectionIds)
                    {
                        bool success = Int64.TryParse(electionId, out long number);
                        if (success)
                        {
                            var voterInfoResponse = GetElectionInformationFromApi(request.Address, number);

                            if (voterInfoResponse != null)
                            {
                                var electionInfo = _mapper.Map<ElectionInformation>(voterInfoResponse);
                                electionQueryResponse.Elections.Add(electionInfo);

                            }
                        }
                    }

                    return Task.FromResult(electionQueryResponse);

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}