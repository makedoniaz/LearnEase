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

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await _context.Courses.OrderByDescending(c => c.CreationDate).ToListAsync();
        }

        public async Task CreateAsync(Course course)
        {
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
        }
    }
}