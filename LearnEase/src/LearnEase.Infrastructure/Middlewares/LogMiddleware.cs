using LearnEase.Core.Services;
using LearnEase.Infrastructure.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Configuration;

namespace LearnEase.Infrastructure.Middlewares
{
    public class LogMiddleware : IMiddleware
    {
        private readonly ILogService service;
        
        private readonly LogBuilderService logBuilder;

        private readonly bool isLoggerOn;
        
        public LogMiddleware(ILogService service, IConfiguration config) 
        {
            this.service = service;
            this.logBuilder = new LogBuilderService();
            this.isLoggerOn = config.GetSection("LoggingOptions:isLoggerOn").Get<bool>();
        }


        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var request = context.Request;
            var response = context.Response;

            if (!isLoggerOn) {
                await next.Invoke(context);
                return;
            }

            this.logBuilder.SetInitialInfo(request.GetDisplayUrl());
            await this.logBuilder.SetRequestBodyAsync(context);

            await next.Invoke(context);

            await this.logBuilder.SetResponseBodyAsync(context);
            this.logBuilder.SetFinishInfo(response.StatusCode, request.Method);

            var log = this.logBuilder.GetLog();

            try {
                await this.service.CreateLogAsync(log);
            }
            catch (Exception ex){
                Console.WriteLine(ex.Message);
            }
        }
    }
}