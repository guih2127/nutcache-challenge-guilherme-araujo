using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EmployeesAPITests
{
    public static class Utils
    {
        public static void ValidateModelForTests<T>(T model, Controller controller)
        {
            if (model is not null)
            {
                var validationContext = new ValidationContext(model, null, null);
                var validationResults = new List<ValidationResult>();
                Validator.TryValidateObject(model, validationContext, validationResults, true);
                foreach (var validationResult in validationResults)
                {
                    if (validationResult.ErrorMessage is not null)
                        controller.ModelState.AddModelError(validationResult.MemberNames.First(), validationResult.ErrorMessage);
                }
            }
        }
    }
}
