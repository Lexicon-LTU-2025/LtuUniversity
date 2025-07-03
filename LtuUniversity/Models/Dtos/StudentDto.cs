using LtuUniversity.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace LtuUniversity.Models.Dtos;

public record StudentDto(int Id, string FullName, string Avatar, string AddressCity);

public record CourseDto(string Title, int Grade);

public class StudentDetailsDto
{
    public int Id { get; set; }
    public required string FullName { get; set; }
    public required string Avatar { get; set; }
    public required string AddressCity { get; set; }
    public IEnumerable<CourseDto> Courses { get; set; } = Enumerable.Empty<CourseDto>();
}

public record CreateStudentDto(string FirstName, string LastName, string Avatar, string Street, string ZipCode, string City);
public record UpdateStudentDto(string FirstName, string LastName, string Avatar, string Street, string ZipCode, string City);
