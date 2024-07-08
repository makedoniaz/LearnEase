using System.Data.SqlClient;
using Dapper;

using LearnEase.Core.Models;
using LearnEase.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LearnEase.Infrastructure.Repositories.Dapper;

public class FeedbackDapperRepository : IFeedbackRepository
{
    private readonly string connectionString;
    

    public FeedbackDapperRepository(IConfiguration config)
    {
        this.connectionString = config.GetConnectionString("MsSql") ?? "";
    }

    public async Task<Feedback?> GetByIdAsync(int id)
    {
        using var connection = new SqlConnection(connectionString);

        var feedback =  await connection.QueryFirstAsync<Feedback>(
                    sql: @"select * from Feedbacks
                            where Id = @id",
                    param: new { id }
                );
        
        return feedback;
    }

    public async Task<IEnumerable<Feedback>> GetAllByCourseIdAsync(int courseId)
    {
        using var connection = new SqlConnection(connectionString);

        var feedbacks = await connection.QueryAsync<Feedback>(
                    sql: @"select * from Feedbacks
                            where CourseId = @courseId
                            order by Feedbacks.CreationDate desc",
                    param: new { courseId }
                );
        
        return feedbacks;
    }


        public async Task<int> CreateAsync(Feedback feedback)
    {
        using var connection = new SqlConnection(connectionString);

            var affectedRowsCount = await connection.ExecuteAsync(
                sql: @"insert into Feedbacks
                    (Text, Rating, CourseId, CreationDate)
                    values (@Text, @Rating, @CourseId, @CreationDate)",
                param: new {
                    feedback.Text,
                    feedback.Rating,
                    feedback.CourseId,
                    feedback.CreationDate
                }
            );

        return affectedRowsCount;
    }


    public async Task<int> PutAsync(int id, Feedback feedback)
    {
        using var connection = new SqlConnection(connectionString);

        var affectedRowsCount = await connection.ExecuteAsync(
                sql: @"update Feedbacks 
                    set 
                    Text = @Text,
                    Rating = @Rating,
                    CreationDate = @CreationDate
                    where Id = @id",
                param: new {
                    feedback.Text,  
                    feedback.Rating,
                    feedback.CreationDate,
                    Id = id,
                }
            );

            return affectedRowsCount;
    }

    public async Task<int> DeleteAsync(int id)
    {
        using var connection = new SqlConnection(connectionString);

        var affectedRowsCount = await connection.ExecuteAsync(
                sql:
                    @"delete from Feedbacks
                    where Id = @Id",            
                param: new {
                    Id = id
                }
            );

        return affectedRowsCount;
    }

    public Task DeleteByCourseId(int courseId)
    {
        throw new NotImplementedException();
    }
}
