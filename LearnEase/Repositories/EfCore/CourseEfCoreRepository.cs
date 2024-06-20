using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnEase.Data;
using LearnEase.Models;
using LearnEase.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LearnEase.Repositories.EfCore
{
    public class CourseEfCoreRepository : ICourseRepository
    {

        private readonly LearnEaseDbContext _context;

        public CourseEfCoreRepository(LearnEaseDbContext context) => _context = context;
        
        public async Task<Course?> GetByIdAsync(int id)
        {
            return await _context.Courses.FirstOrDefaultAsync((c) => c.Id == id);
        }
        
        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await _context.Courses.OrderByDescending(c => c.CreationDate).ToListAsync();
        }

        public async Task<int> CreateAsync(Course course)
        {
            _context.Courses.Add(course);
            var changedObjectsCount = await _context.SaveChangesAsync();

            return changedObjectsCount;
        }

        public async Task<int> DeleteAsync(int id) {
            var courseToDelete = _context.Courses.FirstOrDefault(f => f.Id == id);

            if (courseToDelete is null)
                return 0;

            _context.Courses.Remove(courseToDelete);
            var changedObjectsCount = await _context.SaveChangesAsync();

            return changedObjectsCount;
        }
    }
}