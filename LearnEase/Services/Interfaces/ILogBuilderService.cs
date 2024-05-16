using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnEase.Models;

namespace LearnEase.Services.Interfaces
{
    public interface ILogBuilderService
    {

        void SetInitialInfo(string url);

        public void SetFinishInfo(int statusCode, string httpMethod);

        Task SetResponseBodyAsync(HttpResponse context);

        Task SetRequestBodyAsync(HttpRequest context);
    }
}