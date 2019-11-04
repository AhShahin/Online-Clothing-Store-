using OnlineStore.Helpers;
using OnlineStore.Helpers.QueryParams;
using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Data.Repos
{
    public interface IOrderRepository
    {
        Task<PagedList<Order>> GetOrders(OrderParams orderParams);
        Task<Order> GetOrder(int id);
        Task<OrderItem> PlaceOrder(int itemId, Order order, int UserId);
        Task<IEnumerable<object>> GetNumOfOrdersByPaymentMethod();

    }
}
