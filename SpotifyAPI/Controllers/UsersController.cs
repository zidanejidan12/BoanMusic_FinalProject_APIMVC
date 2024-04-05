using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpotifyAPI.Models;
using SpotifyAPI.Models.DTOs;
using SpotifyAPI.Repository.IRepository;
using SpotifyAPI.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAPI.Controllers
{
    [Route("api/v{version:apiVersion}/users")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly JwtHelper _jwtHelper;

        public UsersController(IUserRepository userRepository, IMapper mapper, JwtHelper jwtHelper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtHelper = jwtHelper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<UserDto>))]
        public IActionResult GetUsers()
        {
            var userList = _userRepository.GetAllUsersAsync().Result;

            var userDtoList = _mapper.Map<List<UserDto>>(userList);
            return Ok(userDtoList);
        }

        [HttpGet("{id}", Name = "GetUser")]
        [ProducesResponseType(200, Type = typeof(UserDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetUser(int id)
        {
            var user = _userRepository.GetUserByIdAsync(id).Result;

            if (user == null)
            {
                return NotFound();
            }

            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(UserDto))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest(ModelState);
            }

            var userObj = _mapper.Map<User>(userDto);

            await _userRepository.AddUserAsync(userObj); // Await the method

            return CreatedAtRoute("GetUser", new { id = userObj.User_ID }, userObj);
        }

        [HttpPost("login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginDto loginDto)
        {
            if (loginDto == null)
            {
                return BadRequest();
            }

            try
            {
                var user = await _userRepository.GetUserByUsernameAndPasswordAsync(loginDto.Username, loginDto.Password);

                if (user == null)
                {
                    return Unauthorized();
                }

                // Assuming you have access to the user type (User_Type) in your user DTO class
                var userType = user.User_Type; // Get the user type from the retrieved user

                // Generate JWT token with user_type claim
                var jwtToken = _jwtHelper.GenerateJwtToken(loginDto.Username, userType);

                // Return token, username, and user_type in response
                return Ok(new
                {
                    Token = jwtToken,
                    Username = loginDto.Username,
                    UserType = userType
                });
            }
            catch (Exception ex)
            {
                // Log the exception and return a generic error response
                // You might want to customize this based on your application's requirements
                return StatusCode(500, "An unexpected error occurred while processing your request.");
            }
        }

        [HttpPatch("{id}", Name = "UpdateUser")]
        [ProducesResponseType(204)]       
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateDto userDto)
        {
            if (userDto == null || id != userDto.User_ID)
            {
                return BadRequest(ModelState);
            }

            var userObj = _mapper.Map<User>(userDto);

            await _userRepository.UpdateUserAsync(userObj); // Await the asynchronous method

            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteUser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteUser(int id)
        {
            var user = _userRepository.GetUserByIdAsync(id).Result;

            if (user == null)
            {
                return NotFound();
            }

            _userRepository.DeleteUserAsync(id);

            return NoContent();
        }
    }
}
