using Greenflux.Exceptions;
using Greenflux.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;

namespace Greenflux.Filters
{
    public class ResponseFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null || context.Result == null)
                return;

            if (context.Result is ObjectResult)
            {
                var result = context.Result as ObjectResult;
                result.Value = new VResponse<dynamic>() { Errors = new List<VResponseError>() { AppErrors.SUCCESS.ToResponseError() }, Data = result.Value };
                context.Result = result;
            }
            else if (context.Result is StatusCodeResult)
            {
                var result = new ObjectResult(new VResponse<dynamic>() { Errors = new List<VResponseError>() { AppErrors.SUCCESS.ToResponseError() } })
                {
                    StatusCode = (context.Result as StatusCodeResult).StatusCode
                };
                context.Result = result;
            }
        }
    }
}