using AutoMapper;
using MediatR;
using PlatformService.Application.Queries;
using PlatformService.AsyncDataServices;
using PlatformService.Data.Interfaces;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;

namespace PlatformService.Application.Commands
{
    public class CreatePlatformHandler : IRequestHandler<CreatePlatformRequest, ApiResponse<CreatePlatformRequest>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandDataClient;
        private readonly IMessageBusClient _messageBus;

        public CreatePlatformHandler(IUnitOfWork unitOfWork, IMapper mapper, ICommandDataClient commandDataClient, IMessageBusClient messageBus)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _commandDataClient = commandDataClient;
            _messageBus = messageBus;
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

                    try
                    {
                        await _commandDataClient.SendPlatformToCommand(response);
                    }
                    catch(Exception ex)
                    {
                        CodeResult = StatusCodes.Status500InternalServerError.ToString();
                        Message = $"Error sending sync platform to command: {ex.Message}";
                        success = false;
                        response = null;
                    }

                    try
                    {
                        var platform = _mapper.Map<PublishPlatformRequest>(response);
                        platform.Event = "Platform_Published";
                        _messageBus.PublishNewPlatform(platform);

                    }
                    catch (Exception ex)
                    {

                        CodeResult = StatusCodes.Status500InternalServerError.ToString();
                        Message = $"Error sending async platform to command: {ex.Message}";
                        success = false;
                        response = null;
                    }
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
