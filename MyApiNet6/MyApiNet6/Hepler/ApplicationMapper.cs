using AutoMapper;
using MyApiNet6.data;
using MyApiNet6.Models;

namespace MyApiNet6.Hepler
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper() 
        {
            CreateMap<Book, BookModel>().ReverseMap();
        }
    }
}
