using AutoMapper;
using LtuUniversity.Models.Dtos;
using LtuUniversity.Models.Entities;

namespace LtuUniversity.Data;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Student, StudentDto>();
             //.ForMember(dest => dest.AddressCity,
             // opt => opt.MapFrom(src => src.Address));

        CreateMap<Student, CreateStudentDto>().ReverseMap();

        CreateMap<Enrollment, CourseDto>();
        // .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Course.Title));

        CreateMap<Student, StudentDetailsDto>();
            //.ForMember(dest => dest.Courses,
            //opt => opt.MapFrom(src => src.Enrollments));
    }
}
