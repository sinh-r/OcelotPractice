using Identity.API.Enum;
using Identity.API.Models.Responses;

namespace Identity.API.Interfaces
{
    public interface IPrepareResponseBusiness
    {
        public Response PrepareResponse(Status status, string successResponse, string invalidInputResponse, string exceptionResponse);
        public Response PrepareResponse(Status status, string successResponse, string invalidInputResponse, string exceptionResponse, string expiredResponse);
    }
}
