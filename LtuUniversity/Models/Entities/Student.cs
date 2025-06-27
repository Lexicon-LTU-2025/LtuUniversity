using System.ComponentModel.DataAnnotations;

namespace LtuUniversity.Models.Entities;

public class Student
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}";
    public string Avatar { get; set; } = string.Empty;
}
