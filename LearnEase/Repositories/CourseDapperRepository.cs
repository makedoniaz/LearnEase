using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using LearnEase.Models;
using LearnEase.Repositories.Interfaces;

namespace LearnEase.Repositories
{
    public class CourseDapperRepository : ICourseRepository
    {
        private readonly string connectionString = "Server =localhost; Database=LearnEase; TrustServerCertificate=True; Trusted_Connection=True; User Id=admin;Password=admin";

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            using var connection = new SqlConnection(connectionString);

            return await connection.QueryAsync<Course>(
                sql: $@"select * from Courses 
                    order by CreationDate desc"
            );
        }

        public async Task CreateAsync(Course course)
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

            if (affectedRowsCount <= 0)
                throw new Exception("Insert error!");
        }
    }
}