using LibraryManagement.BLL.DTOs.Users;
using LibraryManagement.DAL.Models;
using LibraryManagement.DAL.Repositories.Interfaces;

namespace LibraryManagement.BLL.Services
{
    public class UserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Role = u.Role,
                JoinedDate = u.JoinedDate,
                Phone= u.Phone
            });
        }

        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return null;

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                JoinedDate = user.JoinedDate,
                Phone = user.Phone
            };
        }

        public async Task<int> CreateUserAsync(UserCreateDto userCreateDto)
        {
            var user = new User
            {
                Name = userCreateDto.Name,
                Email = userCreateDto.Email,
                Role = userCreateDto.Role,
                JoinedDate = DateTime.Now,
                Phone = userCreateDto.Phone
            };
            await _userRepository.AddAsync(user);
            await _userRepository.SaveAsync();
            return user.Id;
        }

        public async Task UpdateUserAsync(int id, UserUpdateDto userUpdateDto)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser != null)
            {
                existingUser.Name = userUpdateDto.Name;
                existingUser.Email = userUpdateDto.Email;
                existingUser.Role = userUpdateDto.Role;
                _userRepository.Update(existingUser);
                await _userRepository.SaveAsync();
            }
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                _userRepository.Delete(user);
                await _userRepository.SaveAsync();
            }
        }
    }
}
