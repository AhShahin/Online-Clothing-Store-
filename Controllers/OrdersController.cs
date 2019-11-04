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
    public class OrdersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly IRepository<Order> _repository;

        public OrdersController(IUnitOfWork unitOfWork, IMapper mapper, IOrderRepository orderRepository, IRepository<Order> repository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _orderRepository = orderRepository;
            _repository = repository;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<IActionResult> GetOrders([FromQuery]OrderParams orderParams)
        {
            var orders = await _orderRepository.GetOrders(orderParams);

            var ordersToReturn = _mapper.Map<IEnumerable<OrderForListDto>>(orders);

            Response.AddPagination(orders.CurrentPage, orders.PageSize,
                orders.TotalCount, orders.TotalPages);

            return Ok(ordersToReturn);
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await _orderRepository.GetOrder(id);

            if (order == null)
            {
                return NotFound();
            }

            var orderToReturn = _mapper.Map<OrderForDetailsDto>(order);

            return Ok(orderToReturn);
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder([FromRoute] int id, [FromBody] OrderForUpdateDto orderForUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //if (id != order.Id)
            //{
            //    return BadRequest();
            //}

            var order = await _repository.GetAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            _mapper.Map(orderForUpdateDto, order);

            if (await _unitOfWork.SaveAsync())
                return NoContent();

            throw new Exception($"Updating order {id} failed on save");
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await _repository.GetAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _repository.Remove(order);
            await _unitOfWork.SaveAsync();

            return Ok(order);
        }

        [HttpGet("paymentMethod")]
        public async Task<IActionResult> GetNumOfOrdersByPaymentMethod()
        {
            var orders = await _orderRepository.GetNumOfOrdersByPaymentMethod();

            return Ok(orders);
        }

        [HttpPut("{itemId}/placeOrder/{UserId}")]
        public async Task<IActionResult> PlaceOrder([FromBody] OrderForCreationDto o, [FromRoute] int itemId, [FromRoute] int UserId)
        {
            //replace itemId param by [] UserItemOptions

            var orderToPlace = new Order();
            orderToPlace = _mapper.Map(o, orderToPlace);
            orderToPlace.OrdersStatus = "In process";
            orderToPlace.Number = "KJSD7777";
            orderToPlace.UserId = UserId;
            orderToPlace.ModifiedAt = DateTime.Now;

            _repository.Add(orderToPlace);
     
            await _orderRepository.PlaceOrder(itemId, orderToPlace, UserId);

            return Ok(_mapper.Map<OrderForDetailsDto>(orderToPlace));
        }

    }
}