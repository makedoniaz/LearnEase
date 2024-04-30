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
    public class FeedbackDapperRepository : IFeedbackRepository
    {
        private const string connectionString = @"Server=localhost;Database=MyGames;Trusted_Connection=True;TrustServerCertificate=True";


        public async Task<IEnumerable<Feedback>> GetAllByCourseIdAsync(int courseId)
        {
            using var connection = new SqlConnection(connectionString);

            return await connection.QueryAsync<Feedback>(
                        sql: @"select * from Feedbacks
                                where CourseId = @courseId",
                        courseId
                    );
        }


         public async Task CreateAsync(Feedback feedback)
        {
            using var connection = new SqlConnection(connectionString);

            await connection.ExecuteAsync(
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
        }


        public async Task PutAsync(int id, Feedback feedback)
        {
            using var connection = new SqlConnection(connectionString);

            await connection.ExecuteAsync(
                    sql: @"update Feedbacks 
                        set 
                        Text = @Text,
                        Rating = @Rating
                        where Id = @id",
                    param: new {
                        feedback.Text,  
                        feedback.Rating,
                        Id = id,
                    }
                );
        }

        public async Task DeleteAsync(int feedbackId)
        {
            using var connection = new SqlConnection(connectionString);

            await connection.ExecuteAsync(
                    sql:
                        @"delete from Feedbacks
                        where Id = @Id",            
                    param: new {
                       feedbackId
                    }
                );
        }

    }
}