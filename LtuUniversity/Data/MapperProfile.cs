using AutoMapper;
using LtuUniversity.Models.Dtos;
using LtuUniversity.Models.Entities;

namespace LtuUniversity.Data;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Student, StudentDto>();
    }
}
