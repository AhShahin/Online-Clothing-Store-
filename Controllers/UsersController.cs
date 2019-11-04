using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Data.Repos;
using OnlineStore.Data.UOW;
using OnlineStore.Dtos;
using OnlineStore.Helpers;
using OnlineStore.Helpers.QueryParams;
using OnlineStore.Models;

namespace OnlineStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IRepository<User> _repository;

        public UsersController(IUnitOfWork unitOfWork, IMapper mapper, IUserRepository userRepository, IRepository<User> repository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery]UserParams userParams)
        {
            var users = await _userRepository.GetUsers(userParams);
            var usersToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);

            Response.AddPagination(users.CurrentPage, users.PageSize,
                users.TotalCount, users.TotalPages);

            return Ok(usersToReturn);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userRepository.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            var userToReturn = _mapper.Map<UserForDetailedDto>(user);

            return Ok(userToReturn);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser([FromRoute] int id, [FromBody] UserForUpdateDto userForUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //if (id != user.Id)
            //{
            //    return BadRequest();
            //}

            var user = await _repository.GetAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            _mapper.Map(userForUpdateDto, user);

            if (await _unitOfWork.SaveAsync())
                return NoContent();

            throw new Exception($"Updating user {id} failed on save");
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _repository.GetAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _repository.Remove(user);
            await _unitOfWork.SaveAsync();

            return Ok(user);
        }

        [HttpGet("gender")]
        public async Task<IActionResult> GetNumOfUsersByGender()
        {
            var users = await _userRepository.GetNumOfUsersByGender();

            return Ok(users);
        }

        [HttpGet("country")]
        public async Task<IActionResult> GetNumOfUsersByCountry()
        {
            var users = await _userRepository.GetNumOfUsersByCountry();

            return Ok(users);
        }
    }
}