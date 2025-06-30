using System.ComponentModel.DataAnnotations;

namespace LtuUniversity.Models.Entities;

public class Student
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}";
    public string Avatar { get; set; } = string.Empty;

    //Navigation property
    public Address Address { get; set; }

    //Conv 2
    //Conv 3
    //Conv 4
    public ICollection<Assignment> Assignments { get; set; }
}
