using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Data.UOW;
using OnlineStore.Dtos;
using OnlineStore.Models;
using OnlineStore.Data.Repos;


namespace OnlineStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemDetailsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IItemDetailsRepository _itemDetailsRepository;
        private readonly IRepository<Item_details> _repository;

        public ItemDetailsController(IUnitOfWork unitOfWork, IMapper mapper, IItemDetailsRepository itemDetailsRepository, IRepository<Item_details> repository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _itemDetailsRepository = itemDetailsRepository;
            _repository = repository;
        }

        // GET: api/Itemdetails
        [HttpGet]
        public async Task<IActionResult> GetItemDetails()
        {
            var ItemsDetails = await _itemDetailsRepository.GetItems_details();

            var ItemsDetailsToReturn = _mapper.Map<IEnumerable<Item_detailsForListDto>>(ItemsDetails);

            return Ok(ItemsDetailsToReturn);
        }

        // GET: api/Itemdetails/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemDetails([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item_details = await _itemDetailsRepository.GetItem_details(id);

            if (item_details == null)
            {
                return NotFound();
            }

            var item_detailsToReturn = _mapper.Map<Item_detailsForDetailsDto>(item_details);

            return Ok(item_detailsToReturn);
        }

        // PUT: api/Itemdetails/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemDetails([FromRoute] int id, [FromBody] Item_detailsForUpdateDto item_detailsForUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //if (id != item_details.Id)
            //{
            //    return BadRequest();
            //}

            var item_details = await _repository.GetAsync(id);

            if (item_details == null)
            {
                return NotFound();
            }

            _mapper.Map(item_detailsForUpdateDto, item_details);

            if (await _unitOfWork.SaveAsync())
                return NoContent();

            throw new Exception($"Updating item_details {id} failed on save");
        }

        // DELETE: api/Itemdetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemDetails([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item_details = await _repository.GetAsync(id);
            if (item_details == null)
            {
                return NotFound();
            }

            _repository.Remove(item_details);
            await _unitOfWork.SaveAsync();

            return Ok(item_details);
        }
    }
}