using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using PlatformService.Models;

namespace PlatformService.Utils
{
    public class ControllerBaseCustom : ControllerBase
    {
        protected NotFoundObjectResult NotFound([ActionResultObjectValue] object value)
        {
            if (value.GetType() == typeof(string))
            {
                NotFoundResult nf = new NotFoundResult();
                ApiResponse<Boolean?> api = new ApiResponse<Boolean?>();
                api.Message = value.ToString();
                api.CodeResult = nf.StatusCode.ToString();
                api.Success = false;
                api.Data = null;
                return new NotFoundObjectResult(api);
            }
            else
            {
                return new NotFoundObjectResult(value);
            }
        }
        /// <summary>
        /// Not Found Response.
        /// </summary>
        /// <returns></returns>
        protected NotFoundObjectResult NotFound()
        {
            NotFoundResult nf = new NotFoundResult();
            ApiResponse<Boolean?> api = new ApiResponse<Boolean?>();
            api.Message = "Not Found";
            api.CodeResult = nf.StatusCode.ToString();
            api.Success = false;
            api.Data = null;
            return new NotFoundObjectResult(api);
        }
        protected BadRequestObjectResult BadRequest([ActionResultObjectValue] object value)
        {
            if (value.GetType() == typeof(string))
            {
                BadRequestResult nf = new BadRequestResult();
                ApiResponse<Boolean?> api = new ApiResponse<Boolean?>();
                api.Message = value.ToString();
                api.CodeResult = nf.StatusCode.ToString();
                api.Success = false;
                api.Data = null;
                return new BadRequestObjectResult(api);
            }
            else
            {
                return new BadRequestObjectResult(value);
            }
        }
        /// <summary>
        /// Bad Request Response.
        /// </summary>
        /// <returns></returns>
        protected BadRequestObjectResult BadRequest()
        {
            BadRequestResult nf = new BadRequestResult();
            ApiResponse<Boolean?> api = new ApiResponse<Boolean?>();
            api.Message = "Bad Request";
            api.CodeResult = nf.StatusCode.ToString();
            api.Success = false;
            api.Data = null;
            return new BadRequestObjectResult(api);
        }
        /// <summary>
        /// Dynamic Response
        /// </summary>
        /// <returns></returns>
        protected ObjectResult Ok([ActionResultObjectValue] object value)
        {
            if (value.GetType() != typeof(string))
            {
                string Response = value.ToString();
                if (Response.Contains("ApiResponse"))
                {
                    ApiResponse<Boolean?> api = new ApiResponse<Boolean?>();
                    string CodeResult = value.GetType().GetProperty("CodeResult").GetValue(value, null).ToString();
                    string Message = value.GetType().GetProperty("Message").GetValue(value, null).ToString();
                    switch (CodeResult)
                    {
                        case "200":
                            return new OkObjectResult(value);
                            break;
                        case "400":
                            api.Message = Message;
                            api.CodeResult = "400";
                            api.Success = false;
                            api.Data = null;
                            return new BadRequestObjectResult(api);
                            break;
                        case "401":
                            api.Message = "Unauthorized";
                            api.CodeResult = "401";
                            api.Success = false;
                            api.Data = null;
                            return new UnauthorizedObjectResult(api);
                            break;
                        case "404":
                        case "202":
                            api.Message = Message;
                            api.CodeResult = "404";
                            api.Success = false;
                            api.Data = null;
                            return new NotFoundObjectResult(api);
                            break;
                        case "406":
                            api.Message = Message;
                            api.CodeResult = "406";
                            api.Success = false;
                            api.Data = null;
                            return StatusCode(StatusCodes.Status406NotAcceptable, api);
                            break;
                        case "500":
                            api.Message = Message;
                            api.CodeResult = "500";
                            api.Success = false;
                            api.Data = null;
                            return StatusCode(StatusCodes.Status500InternalServerError, api);
                            break;
                        default:
                            api.Message = Message;
                            api.CodeResult = "400";
                            api.Success = false;
                            api.Data = null;
                            return new BadRequestObjectResult(api);
                            break;
                    }
                }
                else
                {
                    return new OkObjectResult(value);
                }
            }
            else
            {
                OkResult nf = new OkResult();
                ApiResponse<Boolean?> api = new ApiResponse<Boolean?>();
                api.Message = value.ToString();
                api.CodeResult = nf.StatusCode.ToString();
                api.Success = true;
                api.Data = null;
                return new OkObjectResult(api);
            }
        }
        /// <summary>
        /// Ok Response.
        /// </summary>
        /// <returns></returns>
        protected OkObjectResult Ok()
        {
            OkResult nf = new OkResult();
            ApiResponse<Boolean?> api = new ApiResponse<Boolean?>();
            api.Message = "Response Successful";
            api.CodeResult = nf.StatusCode.ToString();
            api.Success = true;
            api.Data = null;
            return new OkObjectResult(api);
        }

        /// <summary>
        /// Not Found Response.
        /// </summary>
        /// <returns></returns>
        protected UnauthorizedObjectResult Unauthorized()
        {
            UnauthorizedObjectResult nf = new UnauthorizedObjectResult("Unauthorized");
            ApiResponse<Boolean?> api = new ApiResponse<Boolean?>();
            api.Message = "Unauthorized";
            api.CodeResult = nf.StatusCode.ToString();
            api.Success = false;
            api.Data = null;
            return new UnauthorizedObjectResult(api);
        }
    }
}
