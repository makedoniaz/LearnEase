using System.Data.SqlClient;
using Dapper;
using LearnEase.Models;
using LearnEase.Repositories.Interfaces;

namespace LearnEase.Repositories
{
    public class FeedbackDapperRepository : IFeedbackRepository
    {
        private readonly string connectionString;
        

        public FeedbackDapperRepository(IConfiguration config)
        {
            this.connectionString = config.GetConnectionString("MsSql") ?? "";
        }

        public async Task<Feedback> GetById(int id)
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


         public async Task CreateAsync(Feedback feedback)
        {
            using var connection = new SqlConnection(connectionString);

             var affectedRowsCount = await connection.ExecuteAsync(
                    sql: @"insert into Feedbacks
                        (Username, Text, Rating, CourseId, CreationDate)
                        values (@Username, @Text, @Rating, @CourseId, @CreationDate)",
                    param: new {
                        feedback.Username,
                        feedback.Text,
                        feedback.Rating,
                        feedback.CourseId,
                        feedback.CreationDate
                    }
                );

            if (affectedRowsCount <= 0)
                throw new Exception("Insert error!");
        }


        public async Task PutAsync(int id, Feedback feedback)
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

            if (affectedRowsCount <= 0)
                throw new Exception("Put error!");
        }

        public async Task DeleteAsync(int id)
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

            if (affectedRowsCount <= 0)
                throw new Exception("Delete error!");
        }
    }
}