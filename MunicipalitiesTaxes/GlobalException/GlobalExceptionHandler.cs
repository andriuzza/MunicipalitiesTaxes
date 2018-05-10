using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;
using WebGrease;

namespace MunicipalitiesTaxes.GlobalException
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public bool IsDebug { get; private set; }
        private string _errorMessage;
        private string _stackTrace;
        private string _exceptionProp;
        private HttpResponseMessage _response;

        public GlobalExceptionHandler(bool isDebug)
        {
            IsDebug = isDebug;
        }

        // Override because HTTPPOST and HTTPPUT exceptions are handled by CorsMessageHandler 
        public override bool ShouldHandle(ExceptionHandlerContext context)
        {
            return true;
        }

        public override void Handle(ExceptionHandlerContext context)
        {
            _exceptionProp = context.ExceptionContext.Exception
                                 .InnerException?.ToString() ?? 
                                 context.ExceptionContext?.Exception.Message;

            _errorMessage = context.Exception.Message;

            _stackTrace = context.ExceptionContext.Exception.StackTrace;

            _response = context.Request.CreateResponse(
                new
                {
                    Message = _errorMessage,
                    StackTrace = IsDebug ? _stackTrace : "Release env"
                });

            if (context.Exception is ValidationException)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
            }

            context.Result = new ResponseMessageResult(_response);
        }
    }
}