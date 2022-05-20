using AutoMapper;
using Contracts.Dtos.User.Get;
using Contracts.Dtos.User.Post;
using Contracts.Exceptions;
using Contracts.Images;
using Contracts.Logger;
using Contracts.Models;
using Contracts.Repository;
using Contracts.Security.Passwords;
using Contracts.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public UserManager(ILoggerManager logger, IRepositoryManager repository, IMapper mapper,IImageService imageService,IPasswordHasher passwordManager) : base(logger, repository, mapper)
        {
            _imageService = imageService;
            _passwordManager = passwordManager;
        }

        public async Task<IEnumerable<GetUserDto>> GetAllUsers()
        {
            return _mapper.Map<IEnumerable<GetUserDto>>(await _repository.Users.GetAllAsync(false));
        }

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
            }

            await _repository.SaveAsync();
        }
    }
}
