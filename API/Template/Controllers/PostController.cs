﻿using System.Net;
using Microsoft.AspNetCore.Mvc;
using Template.Shared.Entities;
using Template.Shared.Enums;
using Template.Shared.Extensions;
using Template.Shared.Interfaces;
using Template.Shared.Interfaces.IServices;
using Template.Shared.Models;

namespace Template.Controllers
{
    public class PostController
    {
        private readonly IDalService _DalService;
        public PostController(IDalService dalService)
        {
            _DalService = dalService;
        }

        [HttpPost(nameof(PostAsync))]
        public async Task<Guid> PostAsync()
        {
            var user = await _DalService.GetRandomUserAsync();

            PostModel post = new()
            {
                PublicId = Guid.NewGuid().ToString(),
                UserPublicId = user.PublicId.ToString(),
                Description = Faker.Lorem.Sentence(),
                CreatedOnDt = DateTime.Now,
                Likes = 0
            };
            var result = await _DalService.CreatorManagerAsync(Entity.Post, post);

            return result;
        }

        [HttpGet(nameof(GetByAsync))]
        public async Task<PostModel> GetByAsync()
        {
            var post = await _DalService.GetRandomPostAsync();

            var result = await _DalService.GetPostByAsync(post.PublicId.ToString());


            return result.ToModel();
        }

        [HttpGet(nameof(GetFollowers))]
        public async Task<List<UserModel>?> GetFollowers()
        {
            var post = await _DalService.GetRandomPostAsync();

            var result = await _DalService.GetPostWithAsync(post.PublicId.ToString());

            return result?.Likes.ToModelList();
        }

        [HttpGet(nameof(GetFollowers))]
        public async Task<List<PostModel>?> GetAllPostsBy()
        {
            var result = await _DalService.GetAllPostsByAsync();

            return result?.ToModelList();
        }

        [HttpPut(nameof(UpdatePostAsync))]
        public async Task<Guid> UpdatePostAsync()
        {
            var post = await _DalService.GetRandomPostAsync();

            if (post == null)
                return Guid.Empty;

            var description = Faker.Company.CatchPhrase();

            return await _DalService.UpdateManagerAsync(Entity.Post, post.PublicId.ToString(), description);
        }

        [HttpPut(nameof(Like))]
        public async Task<PostModel?> Like()
        {
            var user = await _DalService.GetRandomUserAsync();

            var post = await _DalService.GetRandomPostAsync();

            var response = await _DalService.LikePostAsync(user.PublicId.ToString(), post.PublicId.ToString());

            return response?.ToModel();
        }


        [HttpDelete(nameof(DeletePostAsync))]
        public async Task<bool> DeletePostAsync()
        {
            var post = await _DalService.GetRandomPostAsync();

            return await _DalService.DeleteManagerAsync(Entity.Post, post.PublicId.ToString());
        }
    }
}