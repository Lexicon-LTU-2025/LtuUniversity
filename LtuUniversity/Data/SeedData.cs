
using Bogus;
using LtuUniversity.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace LtuUniversity.Data;

public class SeedData
{
    private static Faker faker = new Faker("sv");
    internal static async Task InitAsync(UniversityContext context)
    {
        if (await context.Students.AnyAsync()) return;

        var courses = GenerateCourses(30);
        await context.AddRangeAsync(courses);

        var students = GenerateStudents(100, courses);
        await context.AddRangeAsync(students);

        await context.SaveChangesAsync();
    }

    private static IEnumerable<Student> GenerateStudents(int numberOfStudents, List<Course> courses)
    {
        var students = new List<Student>(numberOfStudents);

        for (int i = 0; i < numberOfStudents; i++)
        {
            int numCourses = faker.Random.Int(0, courses.Count);
            var assignedCourses = faker.PickRandom(courses, numCourses).ToList();

            var fName = faker.Name.FirstName();
            var lName = faker.Name.LastName();
            var avatar = faker.Internet.Avatar();

            var student = new Student()
            {
                FirstName = fName,
                LastName = lName,
                Avatar = avatar,
                Address = new Address
                {
                    City = faker.Address.City(),
                    Street = faker.Address.StreetName(),
                    ZipCode = faker.Address.ZipCode()
                },
                Courses = assignedCourses
            };

            students.Add(student);
        }

        return students;
    }

    private static List<Course> GenerateCourses(int numberOfCourses)
    {
        var courses = new List<Course>();

        for (int i = 0; i < numberOfCourses; i++)
        {
            var title = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(faker.Company.Bs());
            var course = new Course { Title = title };
            courses.Add(course);
        }

        return courses;
    }
}
