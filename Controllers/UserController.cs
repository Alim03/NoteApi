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
                var users = await _userRepository.GetAllAsync();
                var userDto = _mapper.Map<IEnumerable<UserDto>>(users);
                return Ok(userDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, $"Error Performing GET in {nameof(GetAll)}");
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var user = await _userRepository.GetEagerLoadAsync(id);
                if (user == null)
                {
                    return NotFound();
                }

                var userDto = _mapper.Map<UserDtoWithNotes>(user);
                return Ok(userDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, $"Error Performing GET in {nameof(Get)}");
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserCreateDto userCreateDto)
        {
            try
            {
                var user = _mapper.Map<User>(userCreateDto);
                await _userRepository.AddAsync(user);
                await _userRepository.SaveAsync();
                var userDto = _mapper.Map<UserDto>(user);
                await _hubContext.Clients.All.UserCreate(userDto);
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
            var user = await _userRepository.GetAsync(userUpdateDto.Id);
            if (user == null)
            {
                return NotFound();
            }
            _mapper.Map(userUpdateDto, user);
            _userRepository.Update(user);
            try
            {
                await _userRepository.SaveAsync();
                var userDto = _mapper.Map<UserDto>(user);
                await _hubContext.Clients.All.UserUpdate(userDto);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex.Message, $"Error Performing GET in {nameof(Update)}");
                return StatusCode(500);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var user = await _userRepository.GetAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                _userRepository.Remove(user);
                await _userRepository.SaveAsync();
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
