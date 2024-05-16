using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using LearnEase.Models;

namespace LearnEase.Repositories.Interfaces
{
    public class LogDapperRepository : ILogRepository
    {
        private readonly string connectionString = "Server=localhost; Database=LearnEase; TrustServerCertificate=True; Trusted_Connection=True; User Id=admin; Password=admin";

        public async Task CreateAsync(Log log)
        {
            using var connection = new SqlConnection(connectionString);

            var affectedRowsCount = await connection.ExecuteAsync(
                sql:@"insert into Logs([Url], [RequestBody], [ResponsetBody], [CreationDate], [EndDate], [StatusCode], [HttpMethod])
                        values (@Url, @RequestBody, @ResponsetBody, @CreationDate, @EndDate, @StatusCode, @HttpMethod)",
                param: log
            );

            if (affectedRowsCount <= 0)
                throw new Exception("Insert error!");
        }
    }
}