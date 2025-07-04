using LtuUniversity.Data;
using LtuUniversity.Models.Dtos;
using System.ComponentModel.DataAnnotations;

namespace LtuUniversity.Validations;

public class NotSameName : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        //var context = validationContext.GetRequiredService<UniversityContext>();

        if(value is string input)
        {
            if(validationContext.ObjectInstance is CreateStudentDto dto)
            {
                return dto.FirstName.Trim().Equals(input.Trim(), StringComparison.OrdinalIgnoreCase) ?
                    new ValidationResult("Firstname cant be same as lastname") : 
                    ValidationResult.Success;
            }
        }

        return new ValidationResult("Error");
    }
}
