using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LtuUniversity.Data;
using LtuUniversity.Models.Entities;
using LtuUniversity.Models.Dtos;

namespace LtuUniversity.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly UniversityContext _context;

        public StudentsController(UniversityContext context)
        {
            _context = context;
        }

        // GET: api/Students
        [HttpGet]
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

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDetailsDto>> GetStudent(int id)
        {
            var student = await _context.Students
                .Where(s => s.Id == id)
                .Select(s => new StudentDetailsDto
                {
                    Id = s.Id,
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

        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

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

        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = student.Id }, student);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
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
