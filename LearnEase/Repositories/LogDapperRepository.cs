using System.Data.SqlClient;
using Dapper;
using LearnEase.Models;
using LearnEase.Repositories.Interfaces;

namespace LearnEase.Repositories
{
    public class LogDapperRepository : ILogRepository
    {
        private readonly string connectionString = "Server=localhost; Database=LearnEase; TrustServerCertificate=True; Trusted_Connection=True; User Id=admin; Password=admin";

        public async Task CreateAsync(Log log)
        {
            using var connection = new SqlConnection(connectionString);

            var affectedRowsCount = await connection.ExecuteAsync(
                sql:@"insert into Logs([Url], [RequestBody], [ResponseBody], [CreationDate], [EndDate], [StatusCode], [HttpMethod])
                        values (@Url, @RequestBody, @ResponseBody, @CreationDate, @EndDate, @StatusCode, @HttpMethod)",
                param: log
            );

            if (affectedRowsCount <= 0)
                throw new Exception("Insert error!");
        }
    }
}