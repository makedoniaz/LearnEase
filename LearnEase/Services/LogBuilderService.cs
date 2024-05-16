using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnEase.Extensions;
using LearnEase.Models;
using LearnEase.Services.Interfaces;
using Microsoft.AspNetCore.Http.Extensions;

namespace LearnEase.Services
{
    public class LogBuilderService
    {
        private readonly Log log;

        public LogBuilderService(Log log)
        {
            if (log is null)
                throw new ArgumentNullException(nameof(log));

            this.log = log;
        }

        public void SetInitialInfo(string url) {
            this.log.Url = url;
            this.log.CreationDate = DateTime.Now;
        }

        public async Task SetRequestBodyAsync(HttpRequest request)
        {
            var requestBody = await request.Body.ReadAsStringAsync();
            this.log.RequestBody = requestBody;
        }

        public async Task SetResponseBodyAsync(HttpResponse response)
        {
            var responseBody = await response.Body.ReadAsStringAsync();
            this.log.ResponseBody = responseBody;
        }

        public void SetFinishInfo(int statusCode, string httpMethod)
        {
            log.StatusCode = statusCode;
            log.HttpMethod = httpMethod;
            log.EndDate = DateTime.Now;
        }
    }
}