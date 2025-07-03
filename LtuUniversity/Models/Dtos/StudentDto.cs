using LtuUniversity.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace LtuUniversity.Models.Dtos;

public record StudentDto(int Id, string FullName, string Avatar, string AddressCity);

public record CourseDto
{
    public string CourseTitle { get; init; } = string.Empty;
    public int Grade { get; init; }
}

public class StudentDetailsDto
{
    public int Id { get; set; }
    public required string FullName { get; set; }
    public required string Avatar { get; set; }
    public required string AddressCity { get; set; }
    public IEnumerable<CourseDto> Enrollments { get; set; } = Enumerable.Empty<CourseDto>();
}

public record CreateStudentDto(string FirstName, string LastName, string Avatar, string AddressStreet, string AddressZipCode, string AddressCity);
public record UpdateStudentDto(string FirstName, string LastName, string Avatar, string Street, string ZipCode, string City);
