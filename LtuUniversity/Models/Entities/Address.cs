namespace LtuUniversity.Models.Entities;

public class Address
{
    public int Id { get; set; }
    public string Street { get; set; }
    public string ZipCode { get; set; }
    public string City { get; set; }

    //Foreign Key
    public int StudentId { get; set; }

    //Navigation property
    public Student Student { get; set; }
}
