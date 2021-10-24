using AlphaTest.Application.Models.Tests;
using AlphaTest.Core.Tests;
using AlphaTest.Infrastructure.Auth;

namespace AlphaTest.Application.Mapping.Configuration
{
    public partial class MappingProfile
    {
        private void CreateMappingForTestAndTestSettings()
        {
            CreateMap<AppUser, ContributorInfo>()
                .ForMember(dto => dto.FIO, opt => opt.MapFrom(u => $"{u.LastName} {u.FirstName} {u.MiddleName}"));
        }
    }
}
