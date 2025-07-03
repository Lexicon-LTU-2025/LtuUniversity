using LtuUniversity.Data;
using LtuUniversity.Models.Dtos;
using LtuUniversity.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LtuUniversity.Controllers
{
    [Route("api/students")]
    [ApiController]
    [Produces("application/json")]
    public class StudentsController : ControllerBase
    {
        private readonly UniversityContext _context;

        public StudentsController(UniversityContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all students
        /// </summary>
        /// <returns>List of StudentDtos</returns>
        // GET: api/Students
        [HttpGet]
        [SwaggerOperation(Summary = "Get all students", Description = "Gets all students with their address city.")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<StudentDto>))]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetStudent()
        {
           // var addresInStockholm = _context.Address.Where(a => a.City == "Stockholm");
           //var res = await _context.Students.Include(s => s.Address).ToListAsync();

            //var res3 = await _context.Students.Include(s => s.Courses);
            var res3 = await _context.Students.Include(s => s.Enrollments).ToListAsync();
            
            var res4 = await _context.Students.Include(s => s.Enrollments).ThenInclude(e => e.Course).ToListAsync();
           
            var res5 = await _context.Students.Where(s => s.Address.City == "Bofred").ToListAsync();    
            
           var res2 = await _context.Students
                                  //  .Include(s => s.Address)
                                    .Select(s => new StudentDto(s.Id, s.FullName, s.Avatar, s.Address.City))
                                    .ToListAsync();

            return Ok(res2);
        }

        /// <summary>
        /// Gets a specific student by ID with details.
        /// </summary>
        /// <param name="id">The student ID.</param>
        /// <returns>Student details DTO.</returns>
        
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get student by ID", Description = "Returns full details of a student.")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentDetailsDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        [SwaggerResponse(StatusCodes.Status200OK, "Find Student", typeof(StudentDetailsDto))]
        public async Task<ActionResult<StudentDetailsDto>> GetStudent([FromRoute, Range(0 , int.MaxValue)]int id)
        {
            var student = await _context.Students
                //.AsNoTracking()
                .Where(s => s.Id == id)
                .Select(s => new StudentDetailsDto
                {
                    //Id = s.Id,
                    Avatar = s.Avatar,
                    FullName = s.FullName,
                    AddressCity = s.Address.City,
                    Courses = s.Enrollments.Select(e => new CourseDto(e.Course.Title, e.Grade))
                })
                .FirstOrDefaultAsync();

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

       
        /// <summary>
        /// Updates an existing student by ID.
        /// </summary>
        /// <param name="id">The student ID.</param>
        /// <param name="dto">The updated student data.</param>
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update student", Description = "Updates an existing student by ID.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> PutStudent([FromRoute]int id, [FromBody] UpdateStudentDto dto)
        {
            var student = await _context.Students
                                       .Include(s => s.Address)
                                       .FirstOrDefaultAsync(s => s.Id == id);

            if (student is null) return NotFound();

            student.FirstName = dto.FirstName;
            student.LastName = dto.LastName;
            student.Avatar = dto.Avatar;
            student.Address.Street = dto.Street;
            student.Address.City = dto.City;
            student.Address.ZipCode = dto.ZipCode;

           // _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Creates a new student.
        /// </summary>
        /// <param name="dto">The student creation DTO.</param>
        /// <returns>The created student DTO.</returns>
        [HttpPost]
        [SwaggerOperation(Summary = "Create student", Description = "Creates a new student.", Tags = ["Student"])]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(StudentDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Student>> PostStudent(CreateStudentDto dto)
        {
            var student = new Student
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Avatar = dto.Avatar,
                Address = new Address
                {
                    City = dto.City,
                    Street = dto.Street,
                    ZipCode = dto.ZipCode
                }
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            var studentDto = new StudentDto(student.Id, student.FullName, student.Avatar, student.Address.City);

            return CreatedAtAction(nameof(GetStudent), new { id = studentDto.Id }, studentDto);
        }

        /// <summary>
        /// Deletes a student by ID.
        /// </summary>
        /// <param name="id">The student ID.</param>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete student", Description = "Deletes a student by ID.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}
