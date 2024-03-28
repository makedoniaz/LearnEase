using System.Net;
using System.Text.Json;
using LearnEaseApp.Repositories.Interfaces;

namespace LearnEaseApp.Controllers;

public class CoursesController
{
    private readonly ICourseRepository repository;

    public CoursesController(ICourseRepository repository)
    {
        this.repository = repository;
    }

    public async Task Courses(HttpListenerContext client) {
        var response = client.Response;
        var request = client.Request;
        var responseText = "";

        if (request.HttpMethod == HttpMethod.Get.Method) {
            response.ContentType = "application/json";

            var courses = await repository.GetAll();
            responseText = JsonSerializer.Serialize(courses);
        }

        using var streamWriter = new StreamWriter(response.OutputStream);
        await streamWriter.WriteAsync(responseText);

        response.StatusCode = (int)HttpStatusCode.OK;
    }
}
