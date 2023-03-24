using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Challenge.Dtos.User;
using Challenge.Hubs;
using Challenge.Models;
using Challenge.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Challenge.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;
        private readonly IHubContext<CallCenterHub, ICallCenterHub> _hubContext;

        public UserController(
            IUserRepository userRepository,
            IMapper mapper,
            ILogger<UserController> logger,
            IHubContext<CallCenterHub, ICallCenterHub> hubContext
        )
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                // Get all users from the repository asynchronously.
                var users = await _userRepository.GetAllAsync();
                // Map the users to user DTOs using AutoMapper.
                var userDto = _mapper.Map<IEnumerable<UserDto>>(users);
                // Return the list of user DTOs as an OK response.
                return Ok(userDto);
            }
            catch (Exception ex)
            {
                // If an exception is caught, log the error message and return a 500 internal server error response.
                _logger.LogError(ex.Message, $"Error Performing GET in {nameof(GetAll)}");
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                // Get the user with the given ID and eager load their notes.
                var user = await _userRepository.GetEagerLoadAsync(id);
                // If the user is not found, return a 404 status code.
                if (user == null)
                {
                    return NotFound();
                }
                // Map the user to UserDtoWithNotes.
                var userDto = _mapper.Map<UserDtoWithNotes>(user);
                // Return the UserDtoWithNotes object with a 200 status code.
                return Ok(userDto);
            }
            catch (Exception ex)
            {
                // Log the error message and return a 500 status code.
                _logger.LogError(ex.Message, $"Error Performing GET in {nameof(Get)}");
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserCreateDto userCreateDto)
        {
            try
            {
                // Map the UserCreateDto to a User entity
                var user = _mapper.Map<User>(userCreateDto);
                // Add the User to the database
                await _userRepository.AddAsync(user);
                await _userRepository.SaveAsync();

                // Map the User to a UserDto
                var userDto = _mapper.Map<UserDto>(user);
                // Notify all connected clients that a user was created
                await _hubContext.Clients.All.UserCreate(userDto);
                // Return a response indicating that the user was successfully created
                return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex.Message,
                    $"Error Performing POST in {nameof(Create)}",
                    userCreateDto
                );
                return StatusCode(500);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserUpdateDto userUpdateDto)
        {
            // Get the User entity with the specified ID
            var user = await _userRepository.GetAsync(userUpdateDto.Id);
            // If the User entity is not found, return a 404 status code
            if (user == null)
            {
                return NotFound();
            }
            // Map the UserUpdateDto to the existing User entity
            _mapper.Map(userUpdateDto, user);
            // Update the User entity in the database
            _userRepository.Update(user);
            try
            {
                await _userRepository.SaveAsync();
                var userDto = _mapper.Map<UserDto>(user);
                // Notify all connected clients that a user was updated
                await _hubContext.Clients.All.UserUpdate(userDto);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex.Message, $"Error Performing GET in {nameof(Update)}");
                return StatusCode(500);
            }
            // Return a response indicating that the user was successfully updated
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // Get the User entity with the specified ID
                var user = await _userRepository.GetAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                // Remove the User entity from the database
                _userRepository.Remove(user);
                await _userRepository.SaveAsync();
                // Notify all connected clients that a user was deleted
                await _hubContext.Clients.All.UserDelete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, $"Error Performing DELETE in {nameof(Delete)}");
                return StatusCode(500);
            }
        }
    }
}
