using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
        /// <summary>
        /// Controller for exceptions
        /// </summary>
        [ApiExplorerSettings(IgnoreApi = true)]
        public class ErrorController : ControllerBase
        {
            public ErrorController()
            {
            }

            [Route("error")]
            public string Error()
            {
                var exception = HttpContext.Features.Get<IExceptionHandlerFeature>().Error;
                string message;    

                // BL exception
                if (exception is MindSphereSdk.Exceptions.MindSphereApiException)
                {
                    Response.StatusCode = 400;
                    message = exception.Message;
                }
                // server exception
                else
                {
                    Response.StatusCode = 500;
                    message = "Server error has occured";
                }
                return message;
            }
        }

    public class ErrorRes
    {
        public string Message { get; set; }

        public ErrorRes(string message)
        {
            Message = message;
        }
    }
}
