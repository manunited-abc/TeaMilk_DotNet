using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeaMilk.Models;

namespace TeaMilk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private TEA_MILKContext _context;

        public OrderController(TEA_MILKContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrder()
        {
            return await _context.Orders.ToListAsync();
        }

        [HttpGet("orderDetail/{orderId}")]
        public async Task<ActionResult<IEnumerable<OrderDetail>>> GetOrderDetail(int orderId)
        {
            return await _context.OrderDetails.Where(o  => (o.OrderId==orderId)).ToListAsync();
        }

        [HttpGet("toppingDetail/{orderId}/{productId}")]
        public async Task<ActionResult<IEnumerable<ToppingDetail>>> GetToppingDetail(int orderId, int productId)
        {
            return await _context.ToppingDetails.Where(t  => (t.OrderId==orderId && t.ProductId == productId)).ToListAsync();
        }
        [HttpGet("toppingDetail")]
        public async Task<ActionResult<IEnumerable<ToppingDetail>>> GetToppingDetail()
        {
            return await _context.ToppingDetails.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            return order != null ? order : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove (order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            _context.Orders.Add (order);
            await _context.SaveChangesAsync();
            return order;
        }

        [HttpPost("orderDetail")]
        public async Task<ActionResult<OrderDetail>> PostOrderDetail(OrderDetail orderDetail)
        {
            _context.OrderDetails.Add(orderDetail);
            await _context.SaveChangesAsync();
            return orderDetail;
        }

        [HttpPost("toppingDetail")]
        public async Task<ActionResult<ToppingDetail>> PostToppingDetail(ToppingDetail toppingDetail)
        {
            _context.ToppingDetails.Add (toppingDetail);
            await _context.SaveChangesAsync();
            return toppingDetail;
        }
    }
}
