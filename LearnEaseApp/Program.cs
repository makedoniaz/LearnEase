using System.Net;
using LearnEaseApp.Controllers;
using LearnEaseApp.Repositories;

namespace LearnEaseApp;
class Program
{
    static async Task Main()
    {
        const string connectionString = "Server =localhost; Database=LearnEase; TrustServerCertificate=True; Trusted_Connection=True; User Id=admin;Password=admin" ;
        
        var coursesController = new CoursesController(new CoursesDapperRepository(connectionString));

        var httpListener = new HttpListener();
        var prefix = "http://*:8080/";

        httpListener.Prefixes.Add(prefix);
        httpListener.Start();

        System.Console.WriteLine($"Server started... {prefix.Replace("*", "localhost")}");

        while (true)
        {
            var client = await httpListener.GetContextAsync();

            string? endpoint = client.Request.RawUrl;

            switch (endpoint)
            {
                case "/":
                    {
                        break;
                    }
                case "/Courses":
                    {
                        await coursesController.Courses(client);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

                client.Response.Close();
        }
    }
}
