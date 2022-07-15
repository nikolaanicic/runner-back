using AutoMapper;
using Contracts.Dtos.User.Get;
using Contracts.Dtos.User.Patch;
using Contracts.Dtos.User.Post;
using Contracts.Exceptions;
using Contracts.Images;
using Contracts.Logger;
using Contracts.Models;
using Contracts.Repository;
using Contracts.Security.Passwords;
using Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Dtos.Email;

namespace Services.UserService
{
    /// <summary>
    /// This class is the user manager class
    /// It should work with user data 
    /// Register users
    /// Fetch user data
    /// Modify user data
    /// </summary>
    public class UserManager : ModelServiceBase, IUserService
    {

        private IImageService _imageService;
        private IPasswordHasher _passwordManager;
        private IEmailService _emailManager;

        public UserManager(ILoggerManager logger, IRepositoryManager repository, IMapper mapper,IImageService imageService,IPasswordHasher passwordManager,IEmailService email) : base(logger, repository, mapper)
        {
            _imageService = imageService;
            _passwordManager = passwordManager;
            _emailManager = email;
        }


        /// <summary>
        /// This method gets all users
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<GetUserDto>> GetAllUsers()
        {
            return _mapper.Map<IEnumerable<GetUserDto>>(await _repository.Users.GetAllAsync(false));
        }

        public async Task<GetUserDto> GetUser(string email)
        {
            var user = await _repository.Users.GetByEmailAsync(email, false);
            if (user == null)
                throw new NotFoundException($"User with the email {email} is not found");

            return _mapper.Map<GetUserDto>(user);
        }

        /// <summary>
        /// This method registers a user
        /// Checks if the username and email are unique
        /// Saves the profile image uploaded
        /// Hashes a password
        /// Adds the hashed password and the image path to the user
        /// And saves the user in the database
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>

        public async Task Register(PostUserDto newUser)
        {
            var takenUsername = await _repository.Users.GetByUsernameAsync(newUser.Username, false);
            var takenEmail = await _repository.Users.GetByEmailAsync(newUser.Email, false);


            if (takenUsername != null)
                throw new ConflictException("Username is already taken");
            else if (takenEmail != null)
                throw new ConflictException("Email is already taken");


            string imagePath = await _imageService.Save(newUser.Image, newUser.Username);
            string password = await _passwordManager.HashPasswordAsync(newUser.Password);


            // think of a way to make registration more generic
            // if more roleas are added this way you would have to add a branch for each of the new roles

            if (newUser.RoleName == RolesConstants.Consumer)
            {
                var consumer = _mapper.Map<Consumer>(newUser);
                consumer.ImagePath = imagePath;
                consumer.PasswordHash = password;
                consumer.RoleId = (long)Roles.Consumer;
                _repository.Consumers.Create(consumer);
            
            }
            else if(newUser.RoleName == RolesConstants.Deliverer)
            {
                var deliverer = _mapper.Map<Deliverer>(newUser);
                deliverer.ImagePath = imagePath;
                deliverer.PasswordHash = password;
                deliverer.RoleId = (long)Roles.Deliverer;

                _repository.Deliverers.Create(deliverer);

                await _emailManager.SendEmail(new Message(deliverer.Email, "Pending request",
                    $"Dear {deliverer.Name} {deliverer.LastName},\n\t\tYour registration request is being processed.\n\t\tWe will contact you as soon as your request is processed.\n\t\t" +
                    $"Best regards, Admin team at runner"));
            }

            await _repository.SaveAsync();
        }


        /// <summary>
        /// This method updates users profile image, should overwrite the previous one and save this one with the same path
        /// </summary>
        /// <param name="image"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<string> UpdateProfileImage(IFormFile image, string email)
        {
            var user = await _repository.Users.GetByEmailAsync(email, true);

            if (user == null)
            {
                _logger.LogError($"Username {user} doesn't exist");
                throw new NotFoundException($"Username {user} doesn't exist");
            }
            else if(image == null)
            {
                _logger.LogError($"Image file is null");
                throw new BadRequestException($"Image is required");
            }

            await _imageService.RemoveImage(email);
            string newPath = await _imageService.Save(image, email);
            user.ImagePath = newPath;
            await _repository.SaveAsync();
            return newPath;
        }


        /// <summary>
        /// This method handles updating users personal information
        /// </summary>
        /// <param name="patchDocument"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task UpdateUser(JsonPatchDocument<UserUpdateDto> patchDocument, string email)
        {
            var user = await _repository.Users.GetByEmailAsync(email, true);

            if (user == null)
                throw new NotFoundException($"User with the email {email} doesn't exist");


            int passwordIndex = (from x in patchDocument.Operations where x.path.ToLower() == "/password" select patchDocument.Operations.IndexOf(x))
                .FirstOrDefault();


            if(patchDocument.Operations[passwordIndex].path.ToLower() == "/password")
            {
                string passwordValue = (string)patchDocument.Operations[passwordIndex].value;
                patchDocument.Operations[passwordIndex].value = _passwordManager.HashPassword(passwordValue);
            }

            var toPatch = _mapper.Map<UserUpdateDto>(user);
            patchDocument.ApplyTo(toPatch);
            toPatch.Password = user.PasswordHash;
            _mapper.Map(toPatch, user);
            await _repository.SaveAsync();
        }
    }
}
