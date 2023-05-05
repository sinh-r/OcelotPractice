using Identity.API.Enum;
using Identity.API.Interfaces;
using Identity.API.Models.Responses;

namespace Identity.API.Business
{
    public class PrepareResponseBusiness : IPrepareResponseBusiness
    {
        public Response PrepareResponse(Status status, string successResponse, string invalidInputResponse, string exceptionResponse)
        {
            Response response = new();

            switch (status)
            {
                case Status.Created:
                    response = new() { Message = successResponse, Status = "Created", StatusCode = StatusCodes.Status201Created };
                    break;
                case Status.Success:
                    response = new() { Message = successResponse, Status = "Success", StatusCode = StatusCodes.Status200OK };
                    break;
                case Status.InvalidInput:
                    response = new() { Message = invalidInputResponse, Status = "InvalidInput", StatusCode = StatusCodes.Status400BadRequest };
                    break;
                case Status.Exception:
                    response = new() { Message = exceptionResponse, Status = "Error", StatusCode = StatusCodes.Status500InternalServerError };
                    break;
            }

            return response;
        }

        public Response PrepareResponse(Status status, string successResponse, string invalidInputResponse, string exceptionResponse, string expiredResponse)
        {
            Response response = new();

            switch (status)
            {
                case Status.Created:
                    response = new() { Message = successResponse, Status = "Created", StatusCode = StatusCodes.Status201Created };
                    break;
                case Status.Success:
                    response = new() { Message = successResponse, Status = "Success", StatusCode = StatusCodes.Status200OK };
                    break;
                case Status.InvalidInput:
                    response = new() { Message = invalidInputResponse, Status = "InvalidInput", StatusCode = StatusCodes.Status400BadRequest };
                    break;
                case Status.Exception:
                    response = new() { Message = exceptionResponse, Status = "Error", StatusCode = StatusCodes.Status500InternalServerError };
                    break;
                case Status.ExpiredRefresh:
                    response = new() { Message = expiredResponse, Status = "Expired", StatusCode = StatusCodes.Status401Unauthorized };
                    break;
            }

            return response;
        }
    }
}
