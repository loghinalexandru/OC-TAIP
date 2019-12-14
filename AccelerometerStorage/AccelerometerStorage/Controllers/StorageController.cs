using AccelerometerStorage.Business;
using AccelerometerStorage.Domain;
using CSharpFunctionalExtensions;
using EnsureThat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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
            var command = new AddDataCommand(username, file.FileName, file.OpenReadStream(), FileType.Input);

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
                ? (IActionResult) BadRequest(Result.Failure(result.Error).ToInternalResponse())
                : CreatedAtAction(null, result.ToInternalResponse());
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Get([FromQuery] UserFilterModel model)
        {
            var username = model?.Username ?? "";
            var startDate = model?.StartingFrom ?? DateTime.MinValue.ToString();
            var stream =
                await storageService.GetData(new GetFilteredDataQuery(username, FileType.Input,
                    DateTime.Parse(startDate)));

            return File(stream.ToArray(), "application/zip", "Data.zip");
        }

        [HttpGet("data/latest/{username}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetLatestData(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest();
            }

            var stream =
                await storageService.GetLatest(new GetFilteredDataQuery(username, FileType.Input,
                    DateTime.MinValue));

            return File(stream.ToArray(), "text/csv", $"latest_{username}.csv");
        }

        [HttpGet("models/latest/{username}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetLatestModel(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest();
            }

            var stream =
                await storageService.GetLatest(new GetFilteredDataQuery(username, FileType.Model,
                    DateTime.MinValue));

            return File(stream.ToArray(), "text/csv", $"latest_{username}.h5");
        }

        [HttpPost("models/{username}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> StoreModel([FromRoute] string username, [FromForm] ModelModel model)
        {
            var file = model.ModelFile;
            var command = new AddDataCommand(username, file.FileName, file.OpenReadStream(), FileType.Model);

            var result = await storageService.AddData(command);

            return result.IsFailure
                ? (IActionResult) BadRequest(Result.Failure(result.Error).ToInternalResponse())
                : CreatedAtAction(null, result.ToInternalResponse());
        }

        [HttpGet("models/{username}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetModel(string username)
        {
            var stream =
                await storageService.GetData(new GetFilteredDataQuery(username, FileType.Model, DateTime.MinValue));

            return File(stream.ToArray(), "application/zip", "Model.zip");
        }

        [HttpGet("users")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await userService.Get());
        }
    }
}