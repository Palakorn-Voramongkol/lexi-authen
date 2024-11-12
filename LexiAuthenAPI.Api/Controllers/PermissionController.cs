using Microsoft.AspNetCore.Mvc;
using LexiAuthenAPI.Api.Services;
using LexiAuthenAPI.Api.DTOs.Permission;

namespace LexiAuthenAPI.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize] // Uncomment to enable authorization for all endpoints
    public class PermissionController : ControllerBase
    {
        // Dependency injection of the IPermissionService
        private readonly IPermissionService _permissionService;

        /// <summary>
        /// Constructor for the PermissionController.
        /// </summary>
        /// <param name="permissionService">The service handling permission operations.</param>
        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        /// <summary>
        /// Retrieves a list of all permissions.
        /// </summary>
        /// <returns>A list of permissions.</returns>
        /// <response code="200">Returns the list of permissions.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadPermissionDto>>> GetPermissions()
        {
            // Call the service to get all permissions
            var permissions = await _permissionService.GetAllPermissionsAsync();
            // Return the permissions with a 200 OK response
            return Ok(permissions);
        }

        /// <summary>
        /// Retrieves a specific permission by ID.
        /// </summary>
        /// <param name="id">The ID of the permission.</param>
        /// <returns>The permission if found; otherwise, a 404 status code.</returns>
        /// <response code="200">Returns the requested permission.</response>
        /// <response code="404">If the permission is not found.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadPermissionDto>> GetPermission(int id)
        {
            // Call the service to get the permission by ID
            var permission = await _permissionService.GetPermissionByIdAsync(id);
            if (permission == null)
            {
                // Return a 404 Not Found response if the permission doesn't exist
                return NotFound();
            }
            // Return the permission with a 200 OK response
            return Ok(permission);
        }

        /// <summary>
        /// Creates a new permission.
        /// </summary>
        /// <param name="createPermissionDto">The permission data transfer object.</param>
        /// <returns>The created permission.</returns>
        /// <response code="201">Returns the newly created permission.</response>
        /// <response code="400">If the permission name is empty.</response>
        [HttpPost]
        public async Task<ActionResult<ReadPermissionDto>> CreatePermission([FromBody] CreatePermissionDto createPermissionDto)
        {
            // Check if the permission name is provided
            if (string.IsNullOrEmpty(createPermissionDto.Name))
            {
                // Return a 400 Bad Request if the name is empty
                return BadRequest("Permission name cannot be empty.");
            }

            // Call the service to create a new permission
            var createdPermission = await _permissionService.CreatePermissionAsync(createPermissionDto);
            // Return the created permission with a 201 Created response
            return CreatedAtAction(nameof(GetPermission), new { id = createdPermission.Id }, createdPermission);
        }

        /// <summary>
        /// Updates an existing permission.
        /// </summary>
        /// <param name="id">The ID of the permission to update.</param>
        /// <param name="updatePermissionDto">The updated permission data transfer object.</param>
        /// <returns>No content if successful; 404 if not found.</returns>
        /// <response code="204">Indicates the update was successful.</response>
        /// <response code="400">If the update data is invalid.</response>
        /// <response code="404">If the permission is not found.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePermission(int id, [FromBody] UpdatePermissionDto updatePermissionDto)
        {
            // Validate the update data
            if (updatePermissionDto == null)
            {
                return BadRequest("Update data is required.");
            }

            // Check if the permission name is provided
            if (string.IsNullOrEmpty(updatePermissionDto.Name))
            {
                return BadRequest("Permission name cannot be empty.");
            }

            // Check if the permission exists
            var existingPermission = await _permissionService.GetPermissionByIdAsync(id);
            if (existingPermission == null)
            {
                // Return a 404 Not Found response if the permission doesn't exist
                return NotFound();
            }

            // Call the service to update the permission
            await _permissionService.UpdatePermissionAsync(id, updatePermissionDto);
            // Return a 204 No Content response to indicate success
            return NoContent();
        }

        /// <summary>
        /// Deletes a permission by ID.
        /// </summary>
        /// <param name="id">The ID of the permission to delete.</param>
        /// <returns>No content if successful; 404 if not found.</returns>
        /// <response code="204">Indicates the deletion was successful.</response>
        /// <response code="404">If the permission is not found.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePermission(int id)
        {
            // Check if the permission exists
            var existingPermission = await _permissionService.GetPermissionByIdAsync(id);
            if (existingPermission == null)
            {
                // Return a 404 Not Found response if the permission doesn't exist
                return NotFound();
            }

            // Call the service to delete the permission
            await _permissionService.DeletePermissionAsync(id);
            // Return a 204 No Content response to indicate success
            return NoContent();
        }

        /// <summary>
        /// Adds a UI permission to an existing permission.
        /// </summary>
        /// <param name="addUipermissionPermissionDto">The data transfer object containing IDs of the permission and UI permission to add.</param>
        /// <returns>Success message if added; otherwise, a bad request.</returns>
        /// <response code="200">Indicates the UI permission was added successfully.</response>
        /// <response code="400">If the operation fails.</response>
        [HttpPost("AddUipermission")]
        public async Task<IActionResult> AddUipermissionToPermission([FromBody] AddUipermissionPermissionDto addUipermissionPermissionDto)
        {
            // Call the service to add the UI permission to the permission
            var result = await _permissionService.AddUipermissionPermissionAsync(addUipermissionPermissionDto);
            if (!result)
            {
                // Return a 400 Bad Request if the operation fails
                return BadRequest("Failed to add UI permission to permission.");
            }

            // Return a 200 OK status with a success message
            return Ok("UI permission added to permission successfully.");
        }

        /// <summary>
        /// Removes a UI permission from an existing permission.
        /// </summary>
        /// <param name="removeUipermissionPermissionDto">The data transfer object containing IDs of the permission and UI permission to remove.</param>
        /// <returns>Success message if removed; otherwise, a bad request.</returns>
        /// <response code="200">Indicates the UI permission was removed successfully.</response>
        /// <response code="400">If the operation fails.</response>
        [HttpDelete("RemoveUipermission")]
        public async Task<IActionResult> RemoveUipermissionFromPermission([FromBody] RemoveUipermissionPermissionDto removeUipermissionPermissionDto)
        {
            // Call the service to remove the UI permission from the permission
            var result = await _permissionService.RemoveUipermissionPermissionAsync(removeUipermissionPermissionDto);
            if (!result)
            {
                // Return a 400 Bad Request if the operation fails
                return BadRequest("Failed to remove UI permission from permission.");
            }

            // Return a 200 OK status with a success message
            return Ok("UI permission removed from permission successfully.");
        }
    }
}
