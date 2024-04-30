using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnEase.Models;

namespace LearnEase.Services.Interfaces
{
    public interface ICourseService
    {
        public Task<IEnumerable<Course>> GetAllCoursesAsync();
        
        public Task CreateCourseAsync(Course newCourse);
    }
}