using System.Data.SqlClient;
using Dapper;

using LearnEase.Core.Models;
using LearnEase.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LearnEase.Infrastructure.Repositories.Dapper;

public class CourseDapperRepository : ICourseRepository
{
    private readonly string connectionString;

    public CourseDapperRepository(IConfiguration config)
    {
        this.connectionString = config.GetConnectionString("MsSql") ?? "";
    }

    public async Task<IEnumerable<Course>> GetAllAsync()
    {
        using var connection = new SqlConnection(connectionString);

        return (IQueryable<Course>)await connection.QueryAsync<Course>(
            sql: $@"select * from Courses 
                order by CreationDate desc"
        );
    }

    public async Task<int> CreateAsync(Course course)
    {
        using var connection = new SqlConnection(connectionString);

        var affectedRowsCount = await connection.ExecuteAsync(
        sql: $@"insert into Courses([Name], [Description], [AmountOfLectures], [CreationDate])
                    values(@Name, @Description, @AmountOfLectures, @CreationDate)",
        param: new
        {
            course.Name,
            course.Description,
            course.AmountOfLectures,
            course.CreationDate,
        });

        return affectedRowsCount;
    }

    public Task<int> DeleteAsync(int entityId)
    {
        throw new NotImplementedException();
    }

    public Task<Course?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Lesson>> GetAllLessonsByCourseIdAsync(int courseId)
    {
        throw new NotImplementedException();
    }
}
