using System.Data.SqlClient;
using Dapper;
using LearnEase.Models;
using LearnEase.Repositories.Interfaces;

namespace LearnEase.Repositories.Dapper
{
    public class LogDapperRepository : ILogRepository
    {
        private readonly string connectionString;

        public LogDapperRepository(IConfiguration config)
        {
            this.connectionString = config.GetConnectionString("MsSql") ?? "";
        }

        public async Task<int> CreateAsync(Log log)
        {
            using var connection = new SqlConnection(connectionString);
            var affectedRowsCount = await connection.ExecuteAsync(
                sql:@"insert into Logs([Url], [RequestBody], [ResponseBody], [CreationDate], [EndDate], [StatusCode], [HttpMethod])
                        values (@Url, @RequestBody, @ResponseBody, @CreationDate, @EndDate, @StatusCode, @HttpMethod)",
                param: log
            );

            return affectedRowsCount;
        }
    }
}