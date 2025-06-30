using System.ComponentModel.DataAnnotations;

namespace LtuUniversity.Models.Entities;

public class Student
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string FullName => $"{FirstName} {LastName}";
    public string Avatar { get; set; } = null!;

    //Navigation property
    public Address Address { get; set; } = null!;

    //Conv 2
    //Conv 3
    //Conv 4
    public ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();
}
