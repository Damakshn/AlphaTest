using AlphaTest.Application.Models.Users;
using AlphaTest.Infrastructure.Auth.UserManagement;

namespace AlphaTest.Application.Mapping.Configuration
{
    public partial class MappingProfile
    {
        private void CreateMappingForUsers()
        {
            // ToDo перенести ContributorInfo сюда
            CreateMap<AppUser, StudentListItemDto>()
                .ForMember(
                    dest => dest.LastNameAndInitials,
                    opt => opt.MapFrom(source =>
                        $"{source.LastName} {source.FirstName[0]}. {source.MiddleName[0]}."));
        }
    }
}
