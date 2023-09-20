using System.Net;
using Microsoft.AspNetCore.Mvc;
using Template.Shared.Entities;
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
        readonly IDalService _DalService;

        public UserController(IDalService dalService)
        {
            _DalService = dalService;
        }

        [HttpGet(nameof(GetByAsync))]
        public async Task<UserModel?> GetByAsync()
        {
            var user = await _DalService.GetRandomUserAsync();

            var result = await _DalService.GetUserByAsync(user.PublicId.ToString());

            return result?.ToModel();
        }

        [HttpGet(nameof(GetFollowers))]
        public async Task<List<UserModel>?> GetFollowers()
        {
            var user = await _DalService.GetRandomUserAsync();

            var result = await _DalService.GetUserWithAsync(user.PublicId.ToString());

            return result?.Followers.ToModelList();
        }

        [HttpGet(nameof(GetFollowing))]
        public async Task<List<UserModel>?> GetFollowing()
        {
            var user = await _DalService.GetRandomUserAsync();

            var result = await _DalService.GetUserWithAsync(user.PublicId.ToString());

            return result?.Following.ToModelList();
        }


        [HttpGet(nameof(GetAllBy))]
        public async Task<List<UserModel>?> GetAllBy()
        {
            var result = await _DalService.GetAllByAsync();

            return result?.ToModelList();
        }

        [HttpPut(nameof(UpdateAsync))]
        public async Task<Guid> UpdateAsync()
        {
            var user = await _DalService.GetRandomUserAsync();

            if (user == null)
                return Guid.Empty;

            var toUpdate = Faker.Company.CatchPhrase();

            var response = await _DalService.UpdateManagerAsync(Entity.User, user.PublicId.ToString(), toUpdate);

            return response;
        }

        [HttpPut(nameof(Subscribe))]
        public async Task<UserEntity?> Subscribe()
        {
            var user1 = await _DalService.GetRandomUserAsync();

            var user2 = await _DalService.GetRandomUserAsync();

            var response = await _DalService.SubscribeToAsync(user1.PublicId.ToString(), user2.PublicId.ToString());

            return response;
        }
    }
}