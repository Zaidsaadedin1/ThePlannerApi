using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ThePlannerAPI.DTOs.User;
using ThePlannerAPI.DTOs.UserAssignment;
using ThePlannerAPI.Enums;
using ThePlannerAPI.Interface;
using ThePlannerAPI.Model;
using ThePlannerAPI.Services;
using ThePlannerAPI.Shared;
using ThePlannerAPI.Validators.User;

[ApiController]
public class UserController : ControllerBase
{
    private readonly IUser _userService;
    private readonly IAssignmentUser _assignmentUser;
    private readonly IValidator<UpdateUserDto> _updateUserValidator;
    private readonly IValidator<AddUserDto> _addUserValidator;
    public readonly AssignmentValidationQuery _assignmentValidationQuery;
    public UserValidationQuery _userValidationQuery;

    public UserController
        (
        IUser userService,
        IAssignmentUser assignmentUser,
        IValidator<UpdateUserDto> updateUserValidator,
        IValidator<AddUserDto> addUserValidator,
        AssignmentValidationQuery assignmentValidationQuery,
        UserValidationQuery userValidationQuery
        )
    {
        _userService = userService;
        _updateUserValidator = updateUserValidator;
        _addUserValidator = addUserValidator;
        _userValidationQuery = userValidationQuery;
        _assignmentUser = assignmentUser;
        _assignmentValidationQuery = assignmentValidationQuery;
    }

    [HttpPost("/Users")]
    public async Task<IActionResult> AddUser([FromBody] AddUserDto user)
    {
        var validationResult = await _addUserValidator.ValidateAsync(user);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(this.ModelState);
            return BadRequest(validationResult);
        }

        var isUserExist = await _userValidationQuery.IsUserUsernameExist(user.Username);
        if (isUserExist)
        {
            return BadRequest(new
            {
                Message = $"User With This Name: ({user.Username}),Dose Already Exist"
            });
        }

        await _userService.AddUser(user);

        return Ok(new { Message = $"User Added Successfully with Username: {user.Username}" });
    }


    [HttpGet("/Users")]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userService.GetUsers();

        return Ok(new { Message = "Users Retrieved Successfully", Users = users });
    }


    [HttpGet("/User")]
    public async Task<IActionResult> GetUserById([FromQuery] string userId)
    {


        var isUserExist = await _userValidationQuery.IsUserExist(userId);
        if (!isUserExist)
        {
            return BadRequest(new
            {
                Message = $"User With This Name: ({userId}),Dose Not Exist"
            });
        }

        var user = await _userService.GetUserById(userId);

        return Ok(new { Message = "User Retrieved Successfully", User = user });
    }

    [HttpPut("/User")]
    public async Task<IActionResult> UpdateUser([FromQuery] string userId, [FromBody] UpdateUserDto user)
    {
        var validationResult = await _updateUserValidator.ValidateAsync(user);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(this.ModelState);
            return BadRequest(validationResult);
        }

        var isUserExist = await _userValidationQuery.IsUserExist(userId);
        if (!isUserExist)
        {
            return BadRequest(new
            {
                Message = $"User With This Name: ({userId}),Dose Not Exist"
            });
        }

        var result = await _userService.UpdateUser(user, userId);

        return Ok(new
        {
            Message = $"User With This ID: ({userId}),Updated Successfully"
        });
    }

    [HttpDelete("/Users")]
    public async Task<IActionResult> DeleteUser([FromQuery] string userId)
    {
        var isUserExist = await _userValidationQuery.IsUserExist(userId);
        if (!isUserExist)
        {
            return BadRequest(new
            {
                Message = $"User With This Name: ({userId}),Dose Not Exist"
            });
        }

        var result = await _userService.DeleteUser(userId);

        return Ok(new { Message = $"User with ID {userId} deleted successfully" });
    }

    [HttpPost("/AssignAssignment")]
    public async Task<IActionResult> AssignAssignmentToUser([FromBody] List<GetUserDTO> users, [FromQuery] int assignmentId)
    {
        foreach (var user in users)
        {
            var isUserExist = await _userValidationQuery.IsUserExist(user.Id);
            if (!isUserExist)
            {
                return BadRequest(new
                {
                    Message = $"User With This id: ({user.Id}),Dose Not Exist"
                });
            }
        }
       
        var assignmentExist = await _assignmentValidationQuery.IsAssignmentExist(assignmentId);
        if (!assignmentExist)
        {
            return BadRequest(new
            {
                Message = $"Assignment With This id: ({assignmentId}),Dose Not Exist"
            });
        }
        var result = await _assignmentUser.AssignAssignmentToUsers(users, assignmentId);

        return Ok(new { Message = $"Success Assigned Assignment To User " });
    }

    [HttpPost("/UnAssignUser")]
    public async Task<IActionResult> UnAssignUserToAssignment([FromQuery] string userId, [FromQuery] int assignmentId)
    {
        var isUserExist = await _userValidationQuery.IsUserExist(userId);
        if (!isUserExist)
        {
            return BadRequest(new
            {
                Message = $"User With This id: ({userId}),Dose Not Exist"
            });
        }
        var assignmentExist = await _assignmentValidationQuery.IsAssignmentExist(assignmentId);
        if (!assignmentExist)
        {
            return BadRequest(new
            {
                Message = $"Assignment With This id: ({assignmentId}),Dose Not Exist"
            });
        }
        var result = await _assignmentUser.UnAssignUserFromAssignment(userId, assignmentId);

        return Ok(new { Message = $"Success UnAssigne User From Assignment " });
    }

    [HttpGet("/GetAllUsersAssignToAssignment")]
    public async Task<IActionResult> GetAllUsersAssignToAssignment([FromQuery] int assignmentId)
    {
        var assignmentExist = await _assignmentValidationQuery.IsAssignmentExist(assignmentId);
        if (!assignmentExist)
        {
            return BadRequest(new
            {
                Message = $"Assignment With This id: ({assignmentId}),Dose Not Exist"
            });
        }

        var users = await _assignmentUser.GetAllUsersAssignToAssignment(assignmentId);

        return Ok( users );
    }

    [HttpGet("/AllAssignmentAssignToUser")]
    public async Task<IActionResult> GetAllAssignmentAssignToUser([FromQuery] string userId)
    {
        var isUserExist = await _userValidationQuery.IsUserExist(userId);
        if (!isUserExist)
        {
            return BadRequest(new
            {
                Message = $"User With This id: ({userId}),Dose Not Exist"
            });
        }

        var assignments = await _assignmentUser.GetAllAssignmentAssignToUser(userId);

        return Ok(assignments);
    }
}
