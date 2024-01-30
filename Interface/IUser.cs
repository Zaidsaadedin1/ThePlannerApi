using ThePlannerAPI.DTOs.User;

namespace ThePlannerAPI.Interface
{
    public interface IUser
    {
        Task<int> AddUser(AddUserDto addUserDto);
        Task<GetUserDTO> GetUserById(string userId);
        Task<List<GetUserDTO>> GetUsers();
        Task<int> UpdateUser(UpdateUserDto user, string id);
        Task<int> DeleteUser(string userId);


    }
}
