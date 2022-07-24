using AutoMapper;
using MyAutoAPI1.Controllers.GetBody.Statement;
using MyAutoAPI1.Models;

namespace MyAutoAPI1.MappingProfile
{
    public class MappingToResponse : Profile
    {
        public MappingToResponse()
        {
            CreateMap<AddStatementModel, Statement>();
        }
    }
}
