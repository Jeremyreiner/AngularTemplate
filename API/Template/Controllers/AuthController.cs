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

    public class AuthController : ControllerBase
    {
        readonly IDalService _DalService;

        public AuthController(IDalService dalService)
        {
            _DalService = dalService;
        }

        [HttpPost(nameof(CreateAsync))]
        public async Task<Guid> CreateAsync()
        {
            var user = new UserModel
            {
                Id = Guid.Empty.ToString(),
                FirstName = Faker.Name.First(),
                LastName = Faker.Name.Last(),
                Bio = Faker.Lorem.Paragraph(1),
                Email = Faker.Internet.Email(),
                Followers = 0,
                Following = 0,
                CreatedOnDt = default,
                LastUpdateOnDt = default
            };

            var result = await _DalService.CreatorManagerAsync(Entity.User, user);

            return result;
        }

        [HttpPost(nameof(Login))]
        public async Task<UserModel?> Login(string email, string password)
        {
            var result = await _DalService.Login(email, password);

            return result?.ToModel();
        }

        [HttpPost(nameof(ChangePassword))]
        public async Task<bool> ChangePassword([FromBody] ChangePasswordModel model) =>
            await _DalService.ChangePassword(model);

        [HttpDelete(nameof(DeleteAsync))]
        public async Task<bool> DeleteAsync()
        {
            var user = await _DalService.GetRandomUserAsync();

            if (user == null)
                return false;

            return await _DalService.DeleteManagerAsync(Entity.User, user.PublicId.ToString());
        }
    }
}