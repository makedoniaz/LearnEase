namespace LearnEase.Core.Models;

public class ValidationErrorModel
{
    public required string PropertyName  { get; set; }
    
    public required string ErrorMessage { get; set; }
}
