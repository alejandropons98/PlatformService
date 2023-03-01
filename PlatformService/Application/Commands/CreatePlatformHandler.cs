using AutoMapper;
using MediatR;
using PlatformService.Application.Queries;
using PlatformService.Data.Interfaces;
using PlatformService.Models;

namespace PlatformService.Application.Commands
{
    public class CreatePlatformHandler : IRequestHandler<CreatePlatformRequest, ApiResponse<CreatePlatformRequest>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreatePlatformHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ApiResponse<CreatePlatformRequest>> Handle(CreatePlatformRequest request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Platform>(request);

            Boolean success;
            String Message;
            Platform dbResponse;
            CreatePlatformRequest response;
            String CodeResult;

            try
            {
                dbResponse = await _unitOfWork.platformRepo.Create(entity);

                response = _mapper.Map<CreatePlatformRequest>(dbResponse);

                if (dbResponse.Id > 0)
                {
                    CodeResult = StatusCodes.Status200OK.ToString();
                    Message = "Success, and there is a response body.";
                    success = true;
                }
                else
                {
                    CodeResult = StatusCodes.Status400BadRequest.ToString();
                    Message = "Platform not registered";
                    response = null;
                    success = false;
                }
            } catch(Exception ex)
            {
                CodeResult = StatusCodes.Status500InternalServerError.ToString();
                Message = "Internal Server Error";
                success = false;
                response = null;
            }

            return new ApiResponse<CreatePlatformRequest>
            {
                CodeResult = CodeResult,
                Message = Message,
                Data = response,
                Success = success
            };
        }
    }
}
