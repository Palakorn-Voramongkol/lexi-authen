using Microsoft.AspNetCore.Mvc;
using LexiAuthenAPI.Api.Services;
using LexiAuthenAPI.Api.DTOs.Uiitem;

namespace LexiAuthenAPI.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize] // Uncomment to enable authorization for all endpoints
    public class UiitemController : ControllerBase
    {
        // Dependency injection of the IUiitemService
        private readonly IUiitemService _uiitemService;

        /// <summary>
        /// Constructor for the UiitemController.
        /// </summary>
        /// <param name="uiitemService">The service handling UI item operations.</param>
        public UiitemController(IUiitemService uiitemService)
        {
            _uiitemService = uiitemService;
        }

        /// <summary>
        /// Retrieves all UI items.
        /// </summary>
        /// <returns>A list of UI items.</returns>
        /// <response code="200">Returns the list of UI items.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadUiitemDto>>> GetAllUiitems()
        {
            // Retrieve all UI items from the service
            var uiitems = await _uiitemService.GetAllUiitemsAsync();
            // Return the UI items with a 200 OK response
            return Ok(uiitems);
        }

        /// <summary>
        /// Retrieves a specific UI item by ID.
        /// </summary>
        /// <param name="id">The ID of the UI item.</param>
        /// <returns>The UI item if found; otherwise, a 404 status code.</returns>
        /// <response code="200">Returns the requested UI item.</response>
        /// <response code="404">If the UI item is not found.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadUiitemDto>> GetUiitem(int id)
        {
            // Call the service to get the UI item by ID
            var uiitem = await _uiitemService.GetUiitemByIdAsync(id);
            if (uiitem == null)
            {
                // Return a 404 Not Found response if the UI item doesn't exist
                return NotFound();
            }
            // Return the UI item with a 200 OK response
            return Ok(uiitem);
        }

        /// <summary>
        /// Creates a new UI item.
        /// </summary>
        /// <param name="createUiitemDto">The UI item data transfer object.</param>
        /// <returns>The created UI item.</returns>
        /// <response code="201">Returns the newly created UI item.</response>
        /// <response code="400">If the input data is invalid.</response>
        [HttpPost]
        public async Task<ActionResult<ReadUiitemDto>> CreateUiitem([FromBody] CreateUiitemDto createUiitemDto)
        {
            // Validate the input data
            if (createUiitemDto == null)
            {
                // Return a 400 Bad Request response if the data is invalid
                return BadRequest("Invalid data.");
            }

            // Call the service to create a new UI item
            var createdUiitem = await _uiitemService.CreateUiitemAsync(createUiitemDto);
            // Return the created UI item with a 201 Created response
            return CreatedAtAction(nameof(GetUiitem), new { id = createdUiitem.Id }, createdUiitem);
        }

        /// <summary>
        /// Updates an existing UI item.
        /// </summary>
        /// <param name="id">The ID of the UI item to update.</param>
        /// <param name="updateUiitemDto">The updated UI item data transfer object.</param>
        /// <returns>No content if successful; 404 if not found.</returns>
        /// <response code="204">Indicates the update was successful.</response>
        /// <response code="400">If the update data is invalid.</response>
        /// <response code="404">If the UI item is not found.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUiitem(int id, [FromBody] UpdateUiitemDto updateUiitemDto)
        {
            // Validate the input data
            if (updateUiitemDto == null)
            {
                // Return a 400 Bad Request response if the data is invalid
                return BadRequest("Update data is required.");
            }

            // Check if the UI item exists
            var existingUiitem = await _uiitemService.GetUiitemByIdAsync(id);
            if (existingUiitem == null)
            {
                // Return a 404 Not Found response if the UI item doesn't exist
                return NotFound();
            }

            // Call the service to update the UI item
            await _uiitemService.UpdateUiitemAsync(id, updateUiitemDto);
            // Return a 204 No Content response to indicate success
            return NoContent();
        }

        /// <summary>
        /// Deletes a UI item by ID.
        /// </summary>
        /// <param name="id">The ID of the UI item to delete.</param>
        /// <returns>No content if successful; 404 if not found.</returns>
        /// <response code="204">Indicates the deletion was successful.</response>
        /// <response code="404">If the UI item is not found.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUiitem(int id)
        {
            // Check if the UI item exists
            var existingUiitem = await _uiitemService.GetUiitemByIdAsync(id);
            if (existingUiitem == null)
            {
                // Return a 404 Not Found response if the UI item doesn't exist
                return NotFound();
            }

            // Call the service to delete the UI item
            await _uiitemService.DeleteUiitemAsync(id);
            // Return a 204 No Content response to indicate success
            return NoContent();
        }
    }
}
