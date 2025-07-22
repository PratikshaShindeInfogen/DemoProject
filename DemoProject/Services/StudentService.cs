using DemoProject.Data;
using DemoProject.Models;
using MongoDB.Driver;

namespace DemoProject.Services
{
    public class StudentService 
    {
        
        private readonly IBaseService<Student> _baseService;
        public StudentService(IBaseService<Student> baseService)
        {
            _baseService = baseService;
        }
        public async Task<List<Student>> GetStudents()
        {
            var students = await _baseService.GetAll();
            var filteredStudent = students.Where(s => s.IsDeleted == false).ToList();
            return filteredStudent;
        }

        public async Task<Student> GetStudentById(string id)
        {
            var student = await _baseService.GetById(id);
            return student;
        }
        //public async Task<Student> GetStudentByName(string name)
        //{
        //    var student = await _baseService.GetByName(name);
        //    return student;
        //}

        public async Task<Student> Create(Student student)
        {
            await _baseService.AddAsync(student);
            return student;
        }

        public async Task<Student> Update(string id, Student student) 
        {
            await _baseService.UpdateAsync(id, student);
            return student;
        }

        public async void Delete(string id) 
        {
            var filter = Builders<Student>.Filter.Eq(x => x.Id, id);
            var updated = Builders<Student>.Update.Set(x => x.IsDeleted, true).Set(x => x.UpdatedAt, DateTime.UtcNow);
            var updateStudent = _baseService.DeleteAsync(filter, updated);
            //await _baseService.DeleteAsync(id);
        }
    }
}
