using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnEase.Services;
using LearnEase.Services.Interfaces;
using Microsoft.AspNetCore.Http.Extensions;

namespace LearnEase.Middlewares
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
            await this.logBuilder.SetRequestBodyAsync(request);

            await next.Invoke(context);

            await this.logBuilder.SetRequestBodyAsync(request);
            this.logBuilder.SetFinishInfo(response.StatusCode, request.Method);
        }
    }
}