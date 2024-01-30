using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThePlannerAPI.Context;
using ThePlannerAPI.DTOs.Assignment;
using ThePlannerAPI.DTOs.User;
using ThePlannerAPI.Enums;
using ThePlannerAPI.Interface;
using ThePlannerAPI.Model;

namespace ThePlannerAPI.Services
{
    public class UserService : IUser
    {
        private readonly ApplicationDbContext context;

        public UserService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<int> AddUser(AddUserDto addUserDto)
        {
            var newUser = new User
            {
                Id = addUserDto.Id,
                FirstName = addUserDto.FirstName,
                LastName = addUserDto.LastName,
                Username = addUserDto.Username,
                ImageUrl = addUserDto.ImageUrl,
                HasImage = addUserDto.HasImage,

            };

            await context.Users.AddAsync(newUser);
            await context.SaveChangesAsync();

            return (int)ApiResponseStatus.Deleted;
        }

        public async Task<int> DeleteUser(string userId)
        {
            var userToDelete = await context.Users.FindAsync(userId);

            context.Users.Remove(userToDelete!); 
            await context.SaveChangesAsync();

            return (int)ApiResponseStatus.Deleted;
        }

        public async Task<GetUserDTO> GetUserById(string userId)
        {
            var user = await context.Users.FindAsync(userId);

            var userDTO = new GetUserDTO
            {
                Id = user!.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                ImageUrl = user.ImageUrl,
                HasImage = user.HasImage,
            };

            return userDTO;
        }

        public async Task<List<GetUserDTO>> GetUsers()
        {
            var usersList = await context.Users.ToListAsync();

            var users = usersList.Select(user => new GetUserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                ImageUrl = user.ImageUrl,
                HasImage = user.HasImage,
            }).ToList();

            return users;
        }

        public async Task<int> UpdateUser(UpdateUserDto user , string id)
        {
            var existingUser = await context.Users.FindAsync(id);

            existingUser!.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Username = user.Username;
            existingUser.ImageUrl = user.ImageUrl;

            await context.SaveChangesAsync();

            return (int)ApiResponseStatus.Updated;
        }

     
    }
}
