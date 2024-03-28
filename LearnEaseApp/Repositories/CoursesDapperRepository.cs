using System.Data.SqlClient;
using Dapper;
using LearnEaseApp.Models;
using LearnEaseApp.ORMs;
using LearnEaseApp.Repositories.Interfaces;

namespace LearnEaseApp.Repositories;

public class CoursesDapperRepository : ICourseRepository
{
    public readonly string connectionString;

    private readonly SqlConnection _sqlConnection;

    public CoursesDapperRepository(string connectionString)
    {
        this.connectionString = connectionString;

        _sqlConnection = new SqlConnection(this.connectionString);
        _sqlConnection.Open();
    }

    public async Task<IEnumerable<Course>> GetAll()
    {
        return await _sqlConnection.QueryAsync<Course>("select * from Courses");
    }

    public async Task<Course> GetById(int id)
    {
        return await _sqlConnection.QueryFirstAsync<Course>(
            sql: @"select * from Courses c 
                    where c.Id = @Id",
            param: new { Id = id });
    }

    public async Task Create(CourseORM course)
    {
        var affectedRowsCount = await _sqlConnection.ExecuteAsync(
           sql: $@"insert into Courses([Name], [Description], [AmountOfLectures], [CreationDate])
                    values(@Name, @Description, 0, @CreationDate)",
           param: new
           {
               course.Name,
               course.Description,
               course.CreationDate,
           });

        if (affectedRowsCount <= 0)
            throw new Exception("Insert error!");
    }

    public async Task<int> DeleteById(int id)
    {
        return await _sqlConnection.ExecuteAsync(
            sql: @"delete from Courses
                    where Id = @Id",
            param: new { Id = id });
    }
}
