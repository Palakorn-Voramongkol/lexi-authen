// src/LexiAuthenAPI.Api/Controllers/AuthController.cs
using Microsoft.AspNetCore.Mvc;
using LexiAuthenAPI.Api.DTOs.Auth;
using LexiAuthenAPI.Api.Services;

namespace LexiAuthenAPI.Api.Controllers
{
    /// <summary>
    /// Controller for handling user authentication and token management.
    /// Provides endpoints for user registration, login, and token refresh operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="authService">Service for handling user authentication.</param>
        /// <param name="tokenService">Service for handling token generation and refresh.</param>
        public AuthController(IAuthService authService, ITokenService tokenService)
        {
            _authService = authService;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="registerDto">The data transfer object containing user registration details.</param>
        /// <returns>An action result indicating success or failure.</returns>
        /// <response code="200">Returns a success message if registration is successful.</response>
        /// <response code="400">Returns an error message if the user already exists or registration fails.</response>
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            // Attempt to register a new user with the provided details.
            var result = await _authService.RegisterAsync(registerDto);
            if (result == null)
                return BadRequest("User already exists.");

            return Ok("User registered successfully.");
        }

        /// <summary>
        /// Logs in a user and returns an access token.
        /// </summary>
        /// <param name="loginDto">The data transfer object containing user login credentials.</param>
        /// <returns>A token response if login is successful; otherwise, an unauthorized result.</returns>
        /// <response code="200">Returns a token response if login is successful.</response>
        /// <response code="401">Returns an error message if the credentials are invalid.</response>
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            // Attempt to authenticate the user and generate a token.
            var tokenResponse = await _authService.LoginAsync(loginDto);
            if (tokenResponse == null)
                return Unauthorized("Invalid credentials.");

            return Ok(tokenResponse);
        }

        /// <summary>
        /// Refreshes an access token using a refresh token.
        /// </summary>
        /// <param name="refreshToken">The refresh token used to obtain a new access token.</param>
        /// <returns>A new token response if the refresh is successful; otherwise, an unauthorized result.</returns>
        /// <response code="200">Returns a new token response if the refresh token is valid.</response>
        /// <response code="401">Returns an error message if the refresh token is invalid or expired.</response>
        [HttpPost("Refresh")]
        public async Task<IActionResult> Refresh([FromBody] string refreshToken)
        {
            // Attempt to refresh the access token using the provided refresh token.
            var tokenResponse = await _tokenService.RefreshTokenAsync(refreshToken);
            if (tokenResponse == null)
                return Unauthorized("Invalid or expired refresh token.");

            return Ok(tokenResponse);
        }
    }
}
