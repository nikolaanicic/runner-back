using Contracts.Exceptions;
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


        /// <summary>
        /// Gets the path of users profile pic on the server
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>

        public async Task<string> Get(string username)
        {
            var user = await _repository.Users.GetByUsernameAsync(username, false);
            if (user == null)
                throw new NotFoundException($"User {username} is not found");

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
        public async Task<string> Save(IFormFile image, string username)
        {
            string relativePath = Path.Combine(_configuration["ImagesRelativePath"], username) + _configuration["ImageEncodingFormat"];
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), relativePath);
            
            if(image != null && image.Length > 0)
            {
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
            }

            return relativePath;
        }
    }
}
