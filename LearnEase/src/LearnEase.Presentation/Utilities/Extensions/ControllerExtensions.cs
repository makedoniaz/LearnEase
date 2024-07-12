using System.Text.Json;
using FluentValidation.Results;
using LearnEase.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace LearnEase.Presentation.Utilities.Extensions;

public static class ControllerExtensions
{
    public static IActionResult? HandleValidationErrors(this Controller controller, ValidationResult validationResult, Func<IActionResult> returnLogic, string pageKey)
    {
        if (!validationResult.IsValid)
        {
            foreach (var error in validationResult.Errors)
                controller.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            

            var serializedErrors = JsonSerializer.Serialize(validationResult.Errors.Select(e => new ValidationErrorModel
            {
                PropertyName = e.PropertyName,
                ErrorMessage = e.ErrorMessage
            }));

            controller.TempData[GetTempDataKey(pageKey)] = serializedErrors;
            
            return returnLogic();
        }

        return null;
    }

    public static void RestoreValidationErrors(this Controller controller, string pageKey)
    {
        string pk = GetTempDataKey(pageKey);

        if (controller.TempData[pk] is string serializedErrors)
        {
            var errors = JsonSerializer.Deserialize<List<ValidationErrorModel>>(serializedErrors);

            if (errors is null)
                return;

            foreach (var error in errors)
            {
                controller.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            controller.TempData.Remove("ValidationErrors");
        }
    }

    private static string GetTempDataKey(string pageKey)
    {
        return $"ValidationErrors_{pageKey}";
    }
}
