using System.Net;
using Microsoft.AspNetCore.Mvc;
using Template.Shared.Enums;
using Template.Shared.Extensions;
using Template.Shared.Interfaces.IServices;
using Template.Shared.Models;

namespace Template.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly IDalService _DalService;

        public UserController(IDalService dalService)
        {
            _DalService = dalService;
        }

        [HttpPost]
        public async Task<Guid> CreateAsync([FromBody] UserModel user) => await _DalService.CreateManagerAsync(ClassType.User, user);

        [HttpPost("LogIn")]
        public async Task<UserModel> Login(string email, string password)
        {
            var result = await _DalService.Login(email, password);

            _DalService.CheckForThrow(result.Error);

            return result.Value.ToModel();
        }

        [HttpPost("ChangePassword")]
        public async Task<HttpStatusCode> ChangePassword([FromBody] ChangePasswordModel model)
        {
            var result = await _DalService.ChangePassword(model);

            _DalService.CheckForThrow(result.Error);

            return result.Status;
        } 

        [HttpGet("{id}")]
        public async Task<UserModel> GetByAsync(string id)
        {
            var result = await _DalService.GetUserAsync(id);

            _DalService.CheckForThrow(result.Error);

            return result.Value.ToModel();
        }

        [HttpPut("Update")]
        public async Task<Guid> UpdateAsync([FromBody] UserModel user) => await _DalService.UpdateManagerAsync(ClassType.User, user);

        [HttpDelete]
        public async Task DeleteAsync([FromBody] UserModel user) => await _DalService.DeleteManagerAsync(ClassType.User, user);
    }
}
