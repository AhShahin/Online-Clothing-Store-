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
    public class AddressesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAddressRepository _addressRepository;
        private readonly IRepository<Address> _repository;

        public AddressesController(IUnitOfWork unitOfWork, IMapper mapper, IAddressRepository addressRepository, IRepository<Address> repository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _addressRepository = addressRepository;
            _repository = repository;
        }

        // GET: api/Addresses
        [HttpGet]
        public async Task<IActionResult> GetAddresses([FromQuery]AddressParams addressParams)
        {
            var addresses = await _addressRepository.GetAddresses(addressParams);

            var addressesToReturn = _mapper.Map<IEnumerable<AddressForListDto>>(addresses);

            Response.AddPagination(addresses.CurrentPage, addresses.PageSize,
                addresses.TotalCount, addresses.TotalPages);

            return Ok(addressesToReturn);
        }

        // GET: api/Addresses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAddress([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var address = await _addressRepository.GetAddress(id);

            if (address == null)
            {
                return NotFound();
            }

            var addressToReturn = _mapper.Map<AddressForDetailsDto>(address);

            return Ok(addressToReturn);
        }

        // PUT: api/Addresses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress([FromRoute] int id, [FromBody] AddressForUpdateDto addressForUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //if (id != address.Id)
            //{
            //    return BadRequest();
            //}

            var address = await _repository.GetAsync(id);

            if (address == null)
            {
                return NotFound();
            }

            _mapper.Map(addressForUpdateDto, address);

            if (await _unitOfWork.SaveAsync())
                return NoContent();

            throw new Exception($"Updating address {id} failed on save");
        }

        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var address = await _repository.GetAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            _repository.Remove(address);
            await _unitOfWork.SaveAsync();

            return Ok(address);
        }

        [HttpGet("country")]
        public async Task<IActionResult> GetNumOfAddressesByCountry()
        {
            var addresses = await _addressRepository.GetNumOfAddressesByCountry();

            return Ok(addresses);
        }
    }
}