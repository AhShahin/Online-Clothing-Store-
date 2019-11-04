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
using OnlineStore.Helpers.QueryParams;
using OnlineStore.Helpers;

namespace OnlineStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IItemRepository _itemRepository;
        private readonly IRepository<Item> _repository;

        public ItemsController(IUnitOfWork unitOfWork, IMapper mapper, IItemRepository itemRepository, IRepository<Item> repository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _itemRepository = itemRepository;
            _repository = repository;
        }

        // GET: api/Items
        [HttpGet]
        public async Task<IActionResult> GetItems([FromQuery]ItemParams itemParams)
        {
            var items = await _itemRepository.GetItems(itemParams);

            var itemsToReturn = _mapper.Map<IEnumerable<ItemForListDto>>(items);

            Response.AddPagination(items.CurrentPage, items.PageSize,
                items.TotalCount, items.TotalPages);

            return Ok(itemsToReturn);
        }

        // GET: api/Items/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem([FromRoute] int id)
        {
            var item = await _itemRepository.GetItem(id);

            if (item == null)
            {
                return NotFound();
            }

            var itemToReturn = _mapper.Map<ItemForDetailsDto>(item);

            return Ok(itemToReturn);
        }

        // PUT: api/Items/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem([FromRoute] int id, [FromBody] ItemForUpdateDto itemForUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //if (id != item.Id)
            //{
            //    return BadRequest();
            //}

            var item = await _repository.GetAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            _mapper.Map(itemForUpdateDto, item);

            if (await _unitOfWork.SaveAsync())
                return NoContent();

            throw new Exception($"Updating item {id} failed on save");
        }

        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = await _repository.GetAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _repository.Remove(item);
            await _unitOfWork.SaveAsync();

            return Ok(item);
        }

        [HttpGet("qty")]
        public async Task<IActionResult> GetItemsWithLowQty([FromQuery]ItemParams itemParams)
        {
            var items = await _itemRepository.GetItemsWithLowQty(itemParams);

            Response.AddPagination(items.CurrentPage, items.PageSize,
                items.TotalCount, items.TotalPages);

            return Ok(items);
        }

        [HttpGet("size")]
        public async Task<IActionResult> GetNumOfItemsBySize([FromQuery]ItemParams itemParams)
        {
            var items = await _itemRepository.GetNumOfItemsBySize(itemParams);

            Response.AddPagination(items.CurrentPage, items.PageSize,
                items.TotalCount, items.TotalPages);

            return Ok(items);
        }

        [HttpGet("top")]
        public async Task<IActionResult> GetTopSellingItem([FromQuery]ItemParams itemParams)
        {
            var items = await _itemRepository.GetTopSellingItem(itemParams);

            //var itemsToReturn = _mapper.Map<IEnumerable<ItemForListDto>>(items);

            //Response.AddPagination(items.CurrentPage, items.PageSize,
            //    items.TotalCount, items.TotalPages);

            return Ok(items);
        }
    }
}