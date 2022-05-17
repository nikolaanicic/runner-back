﻿using Contracts.Exceptions;
using Contracts.Images;
using Contracts.Logger;
using Contracts.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace Services.ImageService
{


    /// <summary>
    /// This manager class is concerned with saving and getting user images
    /// </summary>

    public class ImageManager :ServiceBase, IImageService
    {

        private IConfiguration _configuration;

        public ImageManager(ILoggerManager logger, IRepositoryManager repository, IConfiguration configuration):base(logger,repository)
        {
            _configuration = configuration;
        }

        public async Task<string> Get(string username)
        {
            var user = await _repository.Users.GetByUsernameAsync(username, false);
            if (user == null)
                throw new NotFoundException($"User {username} is not found");

            return user.ImagePath;
        }

        public async Task<string> Save(IFormFile image, string username)
        {
            string relativePath = _configuration["ImagesRelativePath"];
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), relativePath, username) + _configuration["ImageEncodingFormat"];
            
            if(image != null && image.Length > 0)
            {
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
            }

            return fullPath;
        }
    }
}