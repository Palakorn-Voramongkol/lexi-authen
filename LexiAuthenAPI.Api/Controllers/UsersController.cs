using Microsoft.AspNetCore.Mvc;
using LexiAuthenAPI.Api.Services;
using LexiAuthenAPI.Api.DTOs.User;

namespace LexiAuthenAPI.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userService">The user service to handle business logic.</param>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Retrieves a list of all users.
        /// </summary>
        /// <returns>A list of users.</returns>
        /// <response code="200">Returns the list of users.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadUserDto>>> GetUsers()
        {
            // Retrieve all users from the service
            var users = await _userService.GetAllUsersAsync();
            // Return the list of users with a 200 OK status
            return Ok(users);
        }

        /// <summary>
        /// Retrieves a specific user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>The user if found; otherwise, a 404 status code.</returns>
        /// <response code="200">Returns the requested user.</response>
        /// <response code="404">If the user is not found.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadUserDto>> GetUser(int id)
        {
            // Retrieve the user by ID
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                // Return 404 Not Found if the user doesn't exist
                return NotFound();

            // Return the user with a 200 OK status
            return Ok(user);
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="createUserDto">The user data transfer object for creation.</param>
        /// <returns>The created user.</returns>
        /// <response code="201">Returns the newly created user.</response>
        /// <response code="400">If the user data is invalid.</response>
        [HttpPost]
        public async Task<ActionResult<ReadUserDto>> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            // Check if the provided DTO is null
            if (createUserDto == null)
                return BadRequest("User data is required.");

            // Call the service to create the user
            var createdUser = await _userService.CreateUserAsync(createUserDto);
            // Return the created user with a 201 Created status
            return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
        }

        /// <summary>
        /// Updates an existing user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="updateUserDto">The DTO containing the updated user data.</param>
        /// <returns>No content if successful; 404 if the user is not found.</returns>
        /// <response code="204">Indicates the update was successful.</response>
        /// <response code="400">If the update data is invalid.</response>
        /// <response code="404">If the user is not found.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto updateUserDto)
        {
            // Check if the provided DTO is null
            if (updateUserDto == null)
                return BadRequest("Update data is required.");

            try
            {
                // Call the service to update the user
                await _userService.UpdateUserAsync(id, updateUserDto);
                // Return 204 No Content if the update was successful
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                // Return 404 Not Found if the user doesn't exist
                return NotFound();
            }
        }

        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>No content if successful; 404 if the user is not found.</returns>
        /// <response code="204">Indicates the deletion was successful.</response>
        /// <response code="404">If the user is not found.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                // Call the service to delete the user
                await _userService.DeleteUserAsync(id);
                // Return 204 No Content if the deletion was successful
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                // Return 404 Not Found if the user doesn't exist
                return NotFound();
            }
        }

        /// <summary>
        /// Adds a role to a specific user.
        /// </summary>
        /// <param name="addUserRoleDto">The DTO containing the user and role IDs.</param>
        /// <returns>A success message if the role was added; otherwise, a bad request.</returns>
        /// <response code="200">Indicates the role was added successfully.</response>
        /// <response code="400">If the operation fails.</response>
        [HttpPost("AddRole")]
        public async Task<IActionResult> AddUserRole([FromBody] AddUserRoleDto addUserRoleDto)
        {
            // Call the service to add the role to the user
            var result = await _userService.AddUserRoleAsync(addUserRoleDto);
            if (!result)
                // Return 400 Bad Request if the operation fails
                return BadRequest("Failed to add role to user.");

            // Return 200 OK response indicating success
            return Ok("Role added to user successfully.");
        }

        /// <summary>
        /// Removes a role from a specific user.
        /// </summary>
        /// <param name="removeUserRoleDto">The DTO containing the user and role IDs.</param>
        /// <returns>A success message if the role was removed; otherwise, a bad request.</returns>
        /// <response code="200">Indicates the role was removed successfully.</response>
        /// <response code="400">If the operation fails.</response>
        [HttpDelete("RemoveRole")]
        public async Task<IActionResult> RemoveUserRole([FromBody] RemoveUserRoleDto removeUserRoleDto)
        {
            // Call the service to remove the role from the user
            var result = await _userService.RemoveUserRoleAsync(removeUserRoleDto);
            if (!result)
                // Return 400 Bad Request if the operation fails
                return BadRequest("Failed to remove role from user.");

            // Return 200 OK response indicating success
            return Ok("Role removed from user successfully.");
        }

        /// <summary>
        /// Adds a permission to a specific user.
        /// </summary>
        /// <param name="addPermissionToUserDto">The DTO containing the user and permission IDs.</param>
        /// <returns>A success message if the permission was added; otherwise, a bad request.</returns>
        /// <response code="200">Indicates the permission was added successfully.</response>
        /// <response code="400">If the operation fails.</response>
        [HttpPost("AddPermission")]
        public async Task<IActionResult> AddPermissionToUser([FromBody] AddPermissionUserDto addPermissionToUserDto)
        {
            // Call the service to add the permission to the user
            var result = await _userService.AddPermissionUserAsync(addPermissionToUserDto);
            if (!result)
                // Return 400 Bad Request if the operation fails
                return BadRequest("Failed to add permission to user.");

            // Return 200 OK response indicating success
            return Ok("Permission added to user successfully.");
        }

        /// <summary>
        /// Removes a permission from a specific user.
        /// </summary>
        /// <param name="removePermissionFromUserDto">The DTO containing the user and permission IDs.</param>
        /// <returns>A success message if the permission was removed; otherwise, a bad request.</returns>
        /// <response code="200">Indicates the permission was removed successfully.</response>
        /// <response code="400">If the operation fails.</response>
        [HttpDelete("RemovePermission")]
        public async Task<IActionResult> RemovePermissionFromUser([FromBody] RemovePermissionUserDto removePermissionFromUserDto)
        {
            // Call the service to remove the permission from the user
            var result = await _userService.RemovePermissionUserAsync(removePermissionFromUserDto);
            if (!result)
                // Return 400 Bad Request if the operation fails
                return BadRequest("Failed to remove permission from user.");

            // Return 200 OK response indicating success
            return Ok("Permission removed from user successfully.");
        }
    }
}
