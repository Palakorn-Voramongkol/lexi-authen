using Microsoft.AspNetCore.Mvc;
using LexiAuthenAPI.Api.DTOs.Uipermission;
using AutoMapper;
using LexiAuthenAPI.Api.Services;

namespace LexiAuthenAPI.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UipermissionController : ControllerBase
    {
        private readonly IUipermissionService _uipermissionService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for the UipermissionController.
        /// </summary>
        /// <param name="uipermissionService">The service handling UI permission operations.</param>
        /// <param name="mapper">The AutoMapper instance for DTO mapping.</param>
        public UipermissionController(IUipermissionService uipermissionService, IMapper mapper)
        {
            _uipermissionService = uipermissionService;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all UI permissions.
        /// </summary>
        /// <returns>A list of UI permissions.</returns>
        /// <response code="200">Returns the list of UI permissions.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadUipermissionDto>>> GetAllUipermissions()
        {
            // Retrieve all UI permissions from the service
            var uipermissions = await _uipermissionService.GetAllUipermissionsAsync();
            // Map the entities to DTOs
            var uipermissionDtos = _mapper.Map<IEnumerable<ReadUipermissionDto>>(uipermissions);
            // Return the DTOs with a 200 OK response
            return Ok(uipermissionDtos);
        }

        /// <summary>
        /// Retrieves a specific UI permission by ID.
        /// </summary>
        /// <param name="id">The ID of the UI permission.</param>
        /// <returns>The UI permission if found; otherwise, a 404 status code.</returns>
        /// <response code="200">Returns the requested UI permission.</response>
        /// <response code="404">If the UI permission is not found.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadUipermissionDto>> GetUipermissionById(int id)
        {
            // Retrieve the UI permission by ID
            var uipermission = await _uipermissionService.GetUipermissionByIdAsync(id);
            if (uipermission == null) return NotFound(); // Return 404 if not found

            // Map the entity to a DTO
            var uipermissionDto = _mapper.Map<ReadUipermissionDto>(uipermission);
            // Return the DTO with a 200 OK response
            return Ok(uipermissionDto);
        }

        /// <summary>
        /// Creates a new UI permission.
        /// </summary>
        /// <param name="createUipermissionDto">The DTO containing the UI permission data.</param>
        /// <returns>The created UI permission.</returns>
        /// <response code="201">Returns the newly created UI permission.</response>
        /// <response code="400">If the UI permission creation fails.</response>
        [HttpPost]
        public async Task<ActionResult<ReadUipermissionDto>> CreateUipermission(CreateUipermissionDto createUipermissionDto)
        {
            // Call the service to create a new UI permission
            var createdUipermission = await _uipermissionService.CreateUipermissionAsync(createUipermissionDto);

            // Check if creation was successful
            if (createdUipermission == null)
            {
                // Return 400 Bad Request if the creation failed
                return BadRequest("Failed to create Uipermission.");
            }

            // Return the created UI permission with a 201 Created response
            return CreatedAtAction(nameof(GetUipermissionById), new { id = createdUipermission.Id }, createdUipermission);
        }

        /// <summary>
        /// Updates an existing UI permission.
        /// </summary>
        /// <param name="id">The ID of the UI permission to update.</param>
        /// <param name="updateUipermissionDto">The DTO containing updated UI permission data.</param>
        /// <returns>No content if successful; 404 if not found.</returns>
        /// <response code="200">Indicates the update was successful.</response>
        /// <response code="404">If the UI permission is not found.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUipermission(int id, UpdateUipermissionDto updateUipermissionDto)
        {
            // Call the service to update the UI permission
            var updatedUipermission = await _uipermissionService.UpdateUipermissionAsync(id, updateUipermissionDto);

            // Check if the UI permission was found and updated
            if (updatedUipermission == null)
            {
                // Return 404 Not Found if the UI permission doesn't exist
                return NotFound();
            }

            // Return 200 OK response indicating the update was successful
            return Ok(updatedUipermission);
        }

        /// <summary>
        /// Deletes a UI permission by ID.
        /// </summary>
        /// <param name="id">The ID of the UI permission to delete.</param>
        /// <returns>No content if successful; 404 if not found.</returns>
        /// <response code="204">Indicates the deletion was successful.</response>
        /// <response code="404">If the UI permission is not found.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUipermission(int id)
        {
            // Call the service to delete the UI permission
            var success = await _uipermissionService.DeleteUipermissionAsync(id);
            if (!success) return NotFound(); // Return 404 if not found

            // Return 204 No Content response to indicate success
            return NoContent();
        }

        /// <summary>
        /// Adds a UI item to a specific UI permission.
        /// </summary>
        /// <param name="addUiitemUipermissionDto">The DTO containing the UI permission and UI item IDs.</param>
        /// <returns>A success message if the addition is successful; otherwise, a bad request.</returns>
        /// <response code="200">Indicates the UI item was added successfully.</response>
        /// <response code="400">If the addition fails.</response>
        [HttpPost("AddUiitem")]
        public async Task<IActionResult> AddUiitemToUipermission([FromBody] AddUiitemUipermissionDto addUiitemUipermissionDto)
        {
            // Call the service to add the UI item to the UI permission
            var result = await _uipermissionService.AddUiitemUipermissionAsync(addUiitemUipermissionDto);
            if (!result)
                // Return 400 Bad Request if the operation fails
                return BadRequest("Failed to add Uiitem to Uipermission.");

            // Return 200 OK response indicating success
            return Ok("Uiitem added to Uipermission successfully.");
        }

        /// <summary>
        /// Removes a UI item from a specific UI permission.
        /// </summary>
        /// <param name="removeUiitemUipermissionDto">The DTO containing the UI permission and UI item IDs.</param>
        /// <returns>A success message if the removal is successful; otherwise, a bad request.</returns>
        /// <response code="200">Indicates the UI item was removed successfully.</response>
        /// <response code="400">If the removal fails.</response>
        [HttpDelete("RemoveUiitem")]
        public async Task<IActionResult> RemoveUiitemFromUipermission([FromBody] RemoveUiitemUipermissionDto removeUiitemUipermissionDto)
        {
            // Call the service to remove the UI item from the UI permission
            var result = await _uipermissionService.RemoveUiitemUipermissionAsync(removeUiitemUipermissionDto);
            if (!result)
                // Return 400 Bad Request if the operation fails
                return BadRequest("Failed to remove Uiitem from Uipermission.");

            // Return 200 OK response indicating success
            return Ok("Uiitem removed from Uipermission successfully.");
        }
    }
}
