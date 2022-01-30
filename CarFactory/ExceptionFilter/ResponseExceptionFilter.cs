using CarFactory.InputModels;
using CarFactory_Domain.Exceptions;
using CarFactory_Domain.Models.InputModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;

namespace CarFactory.ExceptionFilter
{
    public class ResponseExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception == null) return;

            ContentResult response = new ContentResult
            {
                Content = JsonConvert.SerializeObject(new ResponseBase<BuildCarOutputModel>(context.Exception)),
                ContentType = "application/json"
            };

            if (context.Exception is CarFactoryException 
                || context.Exception is ArgumentNullException
                || context.Exception is ArgumentOutOfRangeException
                || context.Exception is ArgumentException)
            {
                response.StatusCode = 400;
            }
            else
            {
                response.StatusCode = 500;
            }


        }
    }
}
