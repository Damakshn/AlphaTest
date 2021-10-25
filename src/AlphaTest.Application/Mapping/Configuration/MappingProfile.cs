using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace AlphaTest.Application.Mapping.Configuration
{
    public partial class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMappingForTestAndTestSettings();
        }
    }
}
