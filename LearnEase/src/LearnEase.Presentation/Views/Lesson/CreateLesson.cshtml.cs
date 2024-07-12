using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace LearnEase.Presentation.Views.Lesson
{
    public class CreateLesson : PageModel
    {
        private readonly ILogger<CreateLesson> _logger;

        public CreateLesson(ILogger<CreateLesson> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}