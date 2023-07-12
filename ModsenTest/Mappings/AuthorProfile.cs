using AutoMapper;
using ModsenTest.Dtos;
using ModsenTest.Models;

namespace ModsenTest.Mappings
{
    public class AuthorProfile:Profile
    {
        public AuthorProfile()
        {
            CreateMap<RegisterAuthorDto, Author>().ForMember(e => e.Password, opt => opt.Ignore());
            CreateMap<Author, RegisteredAuthorDto>().ForMember(e => e.Password, opt => opt.Ignore());
        }
    }
}
