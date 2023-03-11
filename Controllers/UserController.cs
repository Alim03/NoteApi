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

        public UserController(IUserRepository userRepository, IMapper mapper,
                            ILogger<UserController> logger, IHubContext<CallCenterHub,
                            ICallCenterHub> hubContext)
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
                var userDto = _mapper.Map<IEnumerable<UserListDto>>(users);
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

                var userDto = _mapper.Map<UserDto>(user);
                return Ok(userDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, $"Error Performing GET in {nameof(Get)}");
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserCreateDto userDto)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);
                await _userRepository.AddAsync(user);
                await _userRepository.SaveAsync();
                await _hubContext.Clients.All.UserChangeReceived(user);
                return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, $"Error Performing POST in {nameof(Create)}", userDto);
                return StatusCode(500);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserUpdateDto userDto)
        {
            var user = await _userRepository.GetAsync(userDto.Id);
            if (user == null)
            {
                return NotFound();
            }
            _mapper.Map(userDto, user);
            _userRepository.Update(user);
            try
            {
                await _userRepository.SaveAsync();
                await _hubContext.Clients.All.UserChangeReceived(user);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex.Message, $"Error Performing GET in {nameof(Update)}");
                return StatusCode(500);
            }
            return NoContent();
            // var user = await _userRepository.GetAsync(userDto.Id);
            // if (user == null)
            // {
            //     return NotFound();
            // }

            // _userRepository.Update(user);
            // await _userRepository.SaveAsync();
            // return Ok();
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
                await _hubContext.Clients.All.UserChangeReceived(user);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, $"Error Performing DELETE in {nameof(Delete)}");
                return StatusCode(500);
            }
            // var user = await _userRepository.GetAsync(id);
            // if (user == null)
            // {
            //     return NotFound();
            // }
            // _userRepository.Remove(user);
            // await _userRepository.SaveAsync();
            // return Ok();
        }
    }
}
