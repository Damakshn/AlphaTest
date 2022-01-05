using AlphaTest.Application.Models.Users;
using AlphaTest.Core.Users;


namespace AlphaTest.Application.Mapping.Configuration
{
    public partial class MappingProfile
    {
        private void CreateMappingForUsers()
        {
            // ToDo перенести ContributorInfo сюда
            CreateMap<AlphaTestUser, StudentListItemDto>()
                .ForMember(
                    dest => dest.LastNameAndInitials,
                    opt => opt.MapFrom(source =>
                        $"{source.LastName} {source.FirstName[0]}. {source.MiddleName[0]}."));
        }
    }
}
