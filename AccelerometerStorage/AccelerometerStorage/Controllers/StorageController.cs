using System.Threading.Tasks;
using AccelerometerStorage.Business;
using AccelerometerStorage.WebApi.Extensions;
using CSharpFunctionalExtensions;
using EnsureThat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccelerometerStorage.WebApi
{
    [Route("api/storage")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly IStorageService storageService;
        private readonly IUserService userService;

        public StorageController(IStorageService storageService, IUserService userService)
        {
            EnsureArg.IsNotNull(storageService);
            EnsureArg.IsNotNull(userService);

            this.storageService = storageService;
            this.userService = userService;
        }

        [HttpPost("upload")]
        [Authorize]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddData([FromForm] DataModel data)
        {
            var file = data.CsvFile;
            var username = HttpContext.ExtractUsername();
            var command = new AddDataCommand(username, file.FileName, file.OpenReadStream());

            var result = await storageService.AddData(command);

            return result.IsFailure
                ? (IActionResult) BadRequest(Result.Failure(result.Error).ToInternalResponse())
                : CreatedAtAction(null, result.ToInternalResponse());
        }

        [HttpPost("user")]
        [Authorize]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddUser()
        {
            var username = HttpContext.ExtractUsername();
            var command = new AddUserCommand(username);
            var result = await userService.AddUser(command);

            return result.IsFailure
                ? (IActionResult)BadRequest(Result.Failure(result.Error).ToInternalResponse())
                : CreatedAtAction(null, result.ToInternalResponse());
        }
    }
}
