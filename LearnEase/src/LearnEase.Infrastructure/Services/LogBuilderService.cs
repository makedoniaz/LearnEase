using System.Text;
using LearnEase.Core.Models;
using Microsoft.AspNetCore.Http;

namespace LearnEase.Infrastructure.Services
{
    public class LogBuilderService
    {
        private Log log = new();

        public void Reset() => this.log = new Log();

        public void SetInitialInfo(string url) {
            this.log.Url = url;
            this.log.CreationDate = DateTime.Now;
        }

        public async Task SetRequestBodyAsync(HttpContext context)
        {
            var request = context.Request;

            request.EnableBuffering();
            var requestBodyStr = await new StreamReader(request.Body, Encoding.UTF8).ReadToEndAsync();
            request.Body.Position = 0;

            this.log.RequestBody = requestBodyStr;
        }

        public async Task SetResponseBodyAsync(HttpContext context)
        {
            Stream originalBody = context.Response.Body;
            var response = context.Response;

            using var memStream = new MemoryStream();
            response.Body = memStream;

            memStream.Position = 0;
            var responseBodyStr = await new StreamReader(memStream).ReadToEndAsync();

            memStream.Position = 0;
            await memStream.CopyToAsync(originalBody);

            response.Body = originalBody;

            this.log.ResponseBody = responseBodyStr;
        }

        public void SetFinishInfo(int statusCode, string httpMethod)
        {
            log.StatusCode = statusCode;
            log.HttpMethod = httpMethod;
            log.EndDate = DateTime.Now;
        }

        public Log GetLog() 
        {
            var logResult = this.log;
            this.Reset();

            return logResult;
        }
    }   
}