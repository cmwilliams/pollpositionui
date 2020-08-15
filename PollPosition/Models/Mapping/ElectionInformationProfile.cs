using PollPosition.Models.Elections;
using AutoMapper;
using Google.Apis.CivicInfo.v2.Data;

namespace PollPosition.Models.Mapping
{
    public class ElectionInformationProfile : Profile
    {
        public ElectionInformationProfile()
        {
            CreateMap<Google.Apis.CivicInfo.v2.Data.Source, Elections.Source>();
            CreateMap<Google.Apis.CivicInfo.v2.Data.Channel, Elections.Channel>();
            CreateMap<Google.Apis.CivicInfo.v2.Data.ElectionOfficial, Elections.ElectionOfficial>();
            CreateMap<Google.Apis.CivicInfo.v2.Data.Candidate, Elections.Candidate>();

            CreateMap<Google.Apis.CivicInfo.v2.Data.Contest, Elections.Contest>();

            CreateMap<string, Level>().ConstructUsing(str => new Level { Name = str });
            CreateMap<string, Role>().ConstructUsing(str => new Role { Name = str });
            CreateMap<string, ReferendumBallotResponse>().ConstructUsing(str => new ReferendumBallotResponse { Name = str });
            CreateMap<string, VoterService>().ConstructUsing(str => new VoterService { Name = str });

            CreateMap<SimpleAddressType, Address>()
                .ForPath(dest => dest.Street, opt => opt.MapFrom(src => src.Line1))
                .ForPath(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForPath(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ForPath(dest => dest.Zip, opt => opt.MapFrom(src => src.Zip))
                .ForPath(dest => dest.LocationName, opt => opt.MapFrom(src => src.LocationName));

            CreateMap<ElectoralDistrict, District>();
            CreateMap<AdministrationRegion, State>();
            CreateMap<AdministrationRegion, LocalJurisdiction>();
            CreateMap<AdministrativeBody, ElectionAdministrationBody>();

            CreateMap<Google.Apis.CivicInfo.v2.Data.PollingLocation, Elections.PollingLocation>()
                .ForPath(dest => dest.Location.Address, opt => opt.MapFrom(src => src.Address))
                .ForPath(dest => dest.Location.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForPath(dest => dest.Location.Id, opt => opt.MapFrom(src => src.Id))
                .ForPath(dest => dest.Location.Name, opt => opt.MapFrom(src => src.Name))
                .ForPath(dest => dest.Location.Notes, opt => opt.MapFrom(src => src.Notes))
                .ForPath(dest => dest.Location.PollingHours, opt => opt.MapFrom(src => src.PollingHours))
                .ForPath(dest => dest.Location.Sources, opt => opt.MapFrom(src => src.Sources))
                .ForPath(dest => dest.Location.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForPath(dest => dest.Location.VoterServices, opt => opt.MapFrom(src => src.VoterServices));

            CreateMap<Google.Apis.CivicInfo.v2.Data.PollingLocation, EarlyVoteSite>()
                .ForPath(dest => dest.Location.Address, opt => opt.MapFrom(src => src.Address))
                .ForPath(dest => dest.Location.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForPath(dest => dest.Location.Id, opt => opt.MapFrom(src => src.Id))
                .ForPath(dest => dest.Location.Name, opt => opt.MapFrom(src => src.Name))
                .ForPath(dest => dest.Location.Notes, opt => opt.MapFrom(src => src.Notes))
                .ForPath(dest => dest.Location.PollingHours, opt => opt.MapFrom(src => src.PollingHours))
                .ForPath(dest => dest.Location.Sources, opt => opt.MapFrom(src => src.Sources))
                .ForPath(dest => dest.Location.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForPath(dest => dest.Location.VoterServices, opt => opt.MapFrom(src => src.VoterServices));

            CreateMap<VoterInfoResponse, ElectionInformation>()
                .ForPath(dest => dest.Election.ElectionDay, opt => opt.MapFrom(src => src.Election.ElectionDay))
                .ForPath(dest => dest.Election.Id, opt => opt.MapFrom(src => src.Election.Id))
                .ForPath(dest => dest.Election.Name, opt => opt.MapFrom(src => src.Election.Name))
                .ForPath(dest => dest.Election.DivisionId, opt => opt.MapFrom(src => src.Election.OcdDivisionId));
        }
    }
}
