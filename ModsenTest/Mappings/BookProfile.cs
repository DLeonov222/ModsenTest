using AutoMapper;
using ModsenTest.Dtos;
using ModsenTest.Models;

namespace ModsenTest.Mappings
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookViewDto>().ForMember(e => e.AuthorName,
                    opt => opt.MapFrom(e => e.Author.FirstName + " " + e.Author.LastName))
                .ForMember(e => e.Genre, opt => opt.MapFrom(e => e.Genre.GenreName));

            CreateMap<Book, CreatedBookDto>().ForMember(e => e.AuthorName, opt => opt.MapFrom(e => e.Author.FirstName + " " + e.Author.LastName))
                .ForMember(e => e.Genre, opt => opt.MapFrom(e => e.Genre.GenreName));

            CreateMap<Book, UpdatedBookDto>().ForMember(e => e.AuthorName, opt => opt.MapFrom(e => e.Author.FirstName + " " + e.Author.LastName))
                .ForMember(e => e.Genre, opt => opt.MapFrom(e => e.Genre.GenreName));

            CreateMap<Book, DeletedBookDto>().ForMember(e => e.AuthorName, opt => opt.MapFrom(e => e.Author.FirstName + " " + e.Author.LastName))
                .ForMember(e => e.Genre, opt => opt.MapFrom(e => e.Genre.GenreName));


            CreateMap<CreateBookDto, Book>();

            CreateMap<UpdateBookDto, Book>();
        }
    }
}
