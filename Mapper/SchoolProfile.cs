using AutoMapper;
using WebRepository1.Models.Dtos;
using WebRepository1.Models.Entities;

namespace WebRepository1.Mapper
{
    public class SchoolProfile:Profile
    {
        public SchoolProfile()
        {
            CreateMap<School, SchoolDetailscs>().ReverseMap();
                
        }
    }

}
