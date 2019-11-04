using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.UOW;
using OnlineStore.Helpers;
using OnlineStore.Helpers.QueryParams;
using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnlineStore.Data.Repos
{
    public class OrderRepository: IOrderRepository
    {
        private readonly DataContext Context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<OrderItem> _repository;
        private readonly IItemRepository _itemRepository;

        public OrderRepository(DataContext context, IUnitOfWork unitOfWork, IRepository<OrderItem> repository, IItemRepository itemRepository) {
            Context = context;
            _unitOfWork = unitOfWork;
            _repository = repository;
            _itemRepository = itemRepository;
        }

        public async Task<IEnumerable<object>> GetNumOfOrdersByPaymentMethod()
        {
            return await Context.Orders
                                .GroupBy(t => t.PaymentMethod)
                                .Select(g => new { Name = g.Key, Count = g.Count() })
                                .ToListAsync();
        }

        public async Task<Order> GetOrder(int id)
        {
            return await Context.Orders
              .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Item)
              .SingleOrDefaultAsync(o => o.Id == id);
        }

        public async Task<PagedList<Order>> GetOrders(OrderParams orderParams)
        {
            var orders = Context.Orders
              .AsQueryable();

            if (!string.IsNullOrEmpty(orderParams.SearchTerm))
            {
                orders = orders.Where(o => o.Comments.Contains(orderParams.SearchTerm));
            }

            if (!string.IsNullOrEmpty(orderParams.Number))
            {
                orders = orders.Where(o => o.Number == orderParams.Number);
            }

            if (!string.IsNullOrEmpty(orderParams.ShippingMethod))
            {
                orders = orders.Where(o => o.ShippingMethod == orderParams.ShippingMethod);
            }

            if (!string.IsNullOrEmpty(orderParams.PaymentMethod))
            {
                orders = orders.Where(o => o.PaymentMethod == orderParams.PaymentMethod);
            }

            if (!string.IsNullOrEmpty(orderParams.OrdersStatus))
            {
                orders = orders.Where(o => o.OrdersStatus == orderParams.OrdersStatus);
            }

            var columnsMap = new Dictionary<string, Expression<Func<Order, object>>>()
            {
                ["ShippingMethod"] = o => o.ShippingMethod,
                ["PaymentMethod"] = o => o.PaymentMethod,
                ["OrdersStatus"] = o => o.OrdersStatus,
                ["Number"] = o => o.Number,
            };
            orders = orders.ApplyOrdering(orderParams, columnsMap);

            return await PagedList<Order>.CreateAsync(orders, orderParams.PageNumber, orderParams.PageSize);
        }

        public async Task<OrderItem> PlaceOrder(int itemId, Order order, int UserId)
        {
            var item = await _itemRepository.GetItem(itemId);
            item.Item_Details.Where(id => id.Size.ToString() == "L").FirstOrDefault().Quantity --;

            var orderItem = new OrderItem();
            orderItem.Quantity = 1;
            orderItem.Price = 79.99m;
            orderItem.Item = item;
            orderItem.Order = order;
            orderItem.Tax = 09.99m;
            orderItem.FinalePrice = 99.99m;

            _repository.Add(orderItem);
            await _unitOfWork.SaveAsync();

            return orderItem;
        }
    }
}
