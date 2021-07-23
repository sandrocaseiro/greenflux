using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

namespace Greenflux.Exceptions
{
    public class BindValidationException : BaseException
    {
        public BindValidationException(IEnumerable<ModelStateEntry> bindErrors)
        {
            BindErrors = bindErrors;
        }

        public IEnumerable<ModelStateEntry> BindErrors { get; private init; }
        public override AppErrors AppError => AppErrors.BINDING_VALIDATION_ERROR;

        public static IActionResult Handle(ActionContext context)
        {
            throw new BindValidationException(context.ModelState.Values);
        }
    }
}