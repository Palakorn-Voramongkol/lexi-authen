using Microsoft.AspNetCore.Mvc;
using LexiAuthenAPI.Api.Services;
using LexiAuthenAPI.Api.DTOs.Role;
using Microsoft.AspNetCore.Authorization;

namespace LexiAuthenAPI.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// Retrieves a list of all roles.
        /// </summary>
        /// <returns>A list of roles.</returns>
        /// <response code="200">Returns the list of roles.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadRoleDto>>> GetRoles()
        {
            // Fetch all roles from the service layer
            var roles = await _roleService.GetAllRolesAsync();
            // Return the list of roles with a 200 OK status
            return Ok(roles);
        }

        /// <summary>
        /// Retrieves a specific role by ID.
        /// </summary>
        /// <param name="id">The ID of the role.</param>
        /// <returns>The role if found; otherwise, a 404 status code.</returns>
        /// <response code="200">Returns the requested role.</response>
        /// <response code="404">If the role is not found.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadRoleDto>> GetRole(int id)
        {
            // Fetch the role by ID
            var role = await _roleService.GetRoleByIdAsync(id);
            if (role == null)
            {
                // Return a 404 Not Found if the role does not exist
                return NotFound();
            }
            // Return the role with a 200 OK status
            return Ok(role);
        }

        /// <summary>
        /// Creates a new role.
        /// </summary>
        /// <param name="createRoleDto">The role data transfer object.</param>
        /// <returns>The created role.</returns>
        /// <response code="201">Returns the newly created role.</response>
        /// <response code="400">If the role name is empty.</response>
        [HttpPost]
        public async Task<ActionResult<ReadRoleDto>> CreateRole([FromBody] CreateRoleDto createRoleDto)
        {
            // Check if the role name is provided
            if (string.IsNullOrEmpty(createRoleDto.Name))
            {
                // Return a 400 Bad Request if the role name is empty
                return BadRequest("Role name cannot be empty.");
            }

            // Create the role through the service layer
            var createdRole = await _roleService.CreateRoleAsync(createRoleDto);
            // Return a 201 Created status with the new role
            return CreatedAtAction(nameof(GetRole), new { id = createdRole.Id }, createdRole);
        }

        /// <summary>
        /// Updates an existing role.
        /// </summary>
        /// <param name="id">The ID of the role to update.</param>
        /// <param name="updateRoleDto">The updated role data transfer object.</param>
        /// <returns>No content if successful; 404 if not found.</returns>
        /// <response code="204">Indicates the update was successful.</response>
        /// <response code="400">If the update data is invalid.</response>
        /// <response code="404">If the role is not found.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(int id, [FromBody] UpdateRoleDto updateRoleDto)
        {
            // Check if the provided update data is null
            if (updateRoleDto == null)
            {
                return BadRequest("Update data is required.");
            }

            // Check if the role name is provided
            if (string.IsNullOrEmpty(updateRoleDto.Name))
            {
                return BadRequest("Role name cannot be empty.");
            }

            // Fetch the existing role to check if it exists
            var existingRole = await _roleService.GetRoleByIdAsync(id);
            if (existingRole == null)
            {
                // Return a 404 Not Found if the role does not exist
                return NotFound();
            }

            // Perform the update operation
            await _roleService.UpdateRoleAsync(id, updateRoleDto);
            // Return a 204 No Content status to indicate success
            return NoContent();
        }

        /// <summary>
        /// Deletes a role by ID.
        /// </summary>
        /// <param name="id">The ID of the role to delete.</param>
        /// <returns>No content if successful; 404 if not found.</returns>
        /// <response code="204">Indicates the deletion was successful.</response>
        /// <response code="404">If the role is not found.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            // Fetch the existing role to check if it exists
            var existingRole = await _roleService.GetRoleByIdAsync(id);
            if (existingRole == null)
            {
                // Return a 404 Not Found if the role does not exist
                return NotFound();
            }

            // Perform the deletion operation
            await _roleService.DeleteRoleAsync(id);
            // Return a 204 No Content status to indicate success
            return NoContent();
        }

        /// <summary>
        /// Adds a user to a role.
        /// </summary>
        /// <param name="addUserToRoleDto">The data transfer object containing the user and role IDs.</param>
        /// <returns>Success message if added; otherwise, a bad request.</returns>
        /// <response code="200">Indicates the user was added to the role successfully.</response>
        /// <response code="400">If the operation fails.</response>
        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUserToRole([FromBody] AddUserToRoleDto addUserToRoleDto)
        {
            // Call the service to add the user to the role
            var result = await _roleService.AddUserToRoleAsync(addUserToRoleDto);
            if (!result)
            {
                // Return a 400 Bad Request if the operation fails
                return BadRequest("Failed to add user to role.");
            }

            // Return a 200 OK status with a success message
            return Ok("User added to role successfully.");
        }

        /// <summary>
        /// Removes a user from a role.
        /// </summary>
        /// <param name="removeUserFromRoleDto">The data transfer object containing the user and role IDs.</param>
        /// <returns>Success message if removed; otherwise, a bad request.</returns>
        /// <response code="200">Indicates the user was removed from the role successfully.</response>
        /// <response code="400">If the operation fails.</response>
        [HttpDelete("RemoveUser")]
        public async Task<IActionResult> RemoveUserFromRole([FromBody] RemoveUserFromRoleDto removeUserFromRoleDto)
        {
            // Call the service to remove the user from the role
            var result = await _roleService.RemoveUserFromRoleAsync(removeUserFromRoleDto);
            if (!result)
            {
                // Return a 400 Bad Request if the operation fails
                return BadRequest("Failed to remove user from role.");
            }

            // Return a 200 OK status with a success message
            return Ok("User removed from role successfully.");
        }

        /// <summary>
        /// Adds a permission to a role.
        /// </summary>
        /// <param name="addPermissionToRoleDto">The data transfer object containing the role and permission IDs.</param>
        /// <returns>Success message if added; otherwise, a bad request.</returns>
        /// <response code="200">Indicates the permission was added to the role successfully.</response>
        /// <response code="400">If the operation fails.</response>
        [HttpPost("AddPermission")]
        public async Task<IActionResult> AddPermissionToRole([FromBody] AddPermissionToRoleDto addPermissionToRoleDto)
        {
            // Call the service to add the permission to the role
            var result = await _roleService.AddPermissionToRoleAsync(addPermissionToRoleDto);
            if (!result)
            {
                // Return a 400 Bad Request if the operation fails
                return BadRequest("Failed to add permission to role.");
            }

            // Return a 200 OK status with a success message
            return Ok("Permission added to role successfully.");
        }

        /// <summary>
        /// Removes a permission from a role.
        /// </summary>
        /// <param name="removePermissionFromRoleDto">The data transfer object containing the role and permission IDs.</param>
        /// <returns>Success message if removed; otherwise, a bad request.</returns>
        /// <response code="200">Indicates the permission was removed from the role successfully.</response>
        /// <response code="400">If the operation fails.</response>
        [HttpDelete("RemovePermission")]
        public async Task<IActionResult> RemovePermissionFromRole([FromBody] RemovePermissionFromRoleDto removePermissionFromRoleDto)
        {
            // Call the service to remove the permission from the role
            var result = await _roleService.RemovePermissionFromRoleAsync(removePermissionFromRoleDto);
            if (!result)
            {
                // Return a 400 Bad Request if the operation fails
                return BadRequest("Failed to remove permission from role.");
            }

            // Return a 200 OK status with a success message
            return Ok("Permission removed from role successfully.");
        }
    }
}
