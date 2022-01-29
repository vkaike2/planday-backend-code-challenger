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

            if (context.Exception is CarFactoryException)
            {
                context.Result = new ContentResult
                {
                    Content = JsonConvert.SerializeObject(new ResponseBase<BuildCarOutputModel>(context.Exception, "CarFactoryException")),
                    StatusCode = 400,
                    ContentType = "application/json"
                };

            }
            else if (context.Exception is ArgumentNullException)
            {
                context.Result = new ContentResult
                {
                    Content = JsonConvert.SerializeObject(new ResponseBase<BuildCarOutputModel>(context.Exception, typeof(ArgumentNullException).ToString())),
                    StatusCode = 400,
                    ContentType = "application/json"
                };
            }
            else if (context.Exception is ArgumentOutOfRangeException)
            {
                context.Result = new ContentResult
                {
                    Content = JsonConvert.SerializeObject(new ResponseBase<BuildCarOutputModel>(context.Exception, typeof(ArgumentOutOfRangeException).ToString())),
                    StatusCode = 400,
                    ContentType = "application/json"
                };
            }
            else if (context.Exception is ArgumentException)
            {
                context.Result = new ContentResult
                {
                    Content = JsonConvert.SerializeObject(new ResponseBase<BuildCarOutputModel>(context.Exception, typeof(ArgumentException).ToString())),
                    StatusCode = 400,
                    ContentType = "application/json"
                };
            }
            else
            {
                context.Result = new ContentResult
                {
                    Content = JsonConvert.SerializeObject(new ResponseBase<BuildCarOutputModel>(context.Exception, "Unknown Error")),
                    StatusCode = 500,
                    ContentType = "application/json"
                };
            }
        }
    }
}
