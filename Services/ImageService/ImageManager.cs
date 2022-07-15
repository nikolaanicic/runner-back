using Contracts.Exceptions;
using Contracts.Images;
using Contracts.Logger;
using Contracts.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
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


        /// <summary>
        /// Gets the path of users profile pic on the server
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>

        public async Task<string> Get(string email)
        {
            var user = await _repository.Users.GetByEmailAsync(email, false);
            if (user == null)
                throw new NotFoundException($"User {email} is not found");

            return user.ImagePath;
        }


        /// <summary>
        /// This method saves a form file 
        /// Uses the username to make the path unique and adds the encoding format as the file extension so the os handles the image encoding
        /// If the image with the same path already exists it will be overwritten
        /// </summary>
        /// <param name="image"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<string> Save(IFormFile image, string email)
        {
            string relativePath = string.Empty;
            string fullPath = string.Empty;
            do
            {
                relativePath = Path.Combine(_configuration["ImagesRelativePath"], RandomPathPart()) + _configuration["ImageEncodingFormat"];
                fullPath = MakeImageFullPath(relativePath);
            } while (File.Exists(fullPath));


            if (image != null && image.Length > 0)
            {
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
            }

            return relativePath;
        }

        public async Task RemoveImage(string email)
        {
            var user = await _repository.Users.GetByEmailAsync(email, false);
            if (user == null)
                throw new NotFoundException($"User with the email {email} is not found");

            var fullPath = MakeImageFullPath(user.ImagePath);

            if(File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }


        private string MakeImageFullPath(string relativePath)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), relativePath);
        }


        private string RandomPathPart()
        {
            string charset = _configuration["ImagePathCharSet"];
            var chars = new char[16];
            var random = new Random();

            for(int i=0;i<chars.Length;i++)
            {
                chars[i] = charset[random.Next(chars.Length)];
            }

            return new string(chars);
        }
    }
}
