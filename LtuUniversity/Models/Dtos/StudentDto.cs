using LtuUniversity.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace LtuUniversity.Models.Dtos;

public record StudentDto(int Id, string FullName, string Avatar, string City);

public record CourseDto(string Title, int Grade);

public class StudentDetailsDto
{
    public int Id { get; set; }
    public required string FullName { get; set; }
    public required string Avatar { get; set; }
    public required string AddressCity { get; set; }
    public IEnumerable<CourseDto> Courses { get; set; } = Enumerable.Empty<CourseDto>();
}