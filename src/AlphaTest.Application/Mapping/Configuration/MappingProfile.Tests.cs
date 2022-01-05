using AlphaTest.Application.Models.Tests;
using AlphaTest.Core.Users;


namespace AlphaTest.Application.Mapping.Configuration
{
    public partial class MappingProfile
    {
        private void CreateMappingForTestAndTestSettings()
        {
            CreateMap<AlphaTestUser, ContributorInfo>()
                .ForMember(dto => dto.FIO, opt => opt.MapFrom(u => $"{u.LastName} {u.FirstName} {u.MiddleName}"));
        }
    }
}
