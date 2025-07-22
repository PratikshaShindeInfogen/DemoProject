using DemoProject.Models;
using DemoProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        public readonly StudentService _studentService;
        public StudentController(StudentService service)
        {
            _studentService = service;
        }

        [HttpGet]
        public async Task<List<Student>> Get()
        {
            var students = await _studentService.GetStudents();
            return students;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> Get(string id)
        {
            if (id == null) { return NotFound();}
            var student = await _studentService.GetStudentById(id);
            return student;
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            _studentService.Create(student);
            return Ok("Student successfully registered");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> Update(string id,Student student) 
        {   
            if(student == null) { return NotFound(); }
            var studentdetails = await _studentService.Update(id,student);
            return studentdetails;
           
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id) 
        {
            if (id == null) { return NotFound(); }
            _studentService.Delete(id);
            return Ok("Student successfully deleted");
        }
    }
}
