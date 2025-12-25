

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreFlow.Context;
using StoreFlow.Entities;
using StoreFlow.Models;

namespace StoreFlow.Controllers
{
    public class OrderController : Controller
    {
        private readonly StoreContext _context;

        public OrderController(StoreContext context)
        {
            _context = context;
        }

        public IActionResult AllStockSmallerThen5()
        {
            bool orderStockCount = _context.orders.All(x => x.OrderCount <= 5);
            if (orderStockCount)
            {
                ViewBag.v = "Tüm Siparişler 5 adetten küçüktür!";
            }
            else
            {
                ViewBag.v = "Tüm Siparişler 5 adetten küçük değildir!";
            }
            return View();
        }
        public IActionResult OrderListByStatus(string status)
        {
            var values = _context.orders.Where(x => x.Status.Contains(status)).ToList();
            if (!values.Any())
            {
                ViewBag.v = "Bu Statüs ile ilgili veri bulunamadı!";
            }
            return View(values);
        }
        public IActionResult OrderListSearch(string name, string filterType)
        {
            if (filterType == "start")
            {
                var values = _context.orders.Where(x => x.Status.StartsWith(name)).ToList();
                return View(values);
            }
            else if (filterType == "end")
            {
                var values = _context.orders.Where(x => x.Status.EndsWith(name)).ToList();
                return View(values);
            }
            var ordervalues = _context.orders.ToList();
            return View(ordervalues);
        }
        public async Task<IActionResult> OrderList()
        {
            var values = _context.orders.Include(x => x.product).Include(y => y.Customer).ToList();
            return View(values);
        }
        [HttpGet]
        public async Task<IActionResult> CreateOrder()
        {
            var products = _context.Products
                                .Select(p => new SelectListItem
                                {
                                    Value = p.ProductId.ToString(),
                                    Text = p.ProductName
                                }).ToList();
            ViewBag.products = products;

            var customers = _context.Customers
                                .Select(c => new SelectListItem
                                {
                                    Value = c.CustomerId.ToString(),
                                    Text = c.CustomerName + " " + c.CustomerSurname
                                }).ToList();
            ViewBag.customers = customers;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder(Order order)
        {
            order.Status = "Sipariş Alındı";
            order.OrderDate = DateTime.Now;
            await _context.orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return RedirectToAction("OrderList");
        }
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var value = await _context.orders.FindAsync(id);
            _context.orders.Remove(value);
            await _context.SaveChangesAsync();
            return RedirectToAction("OrderList");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateOrder(int id)
        {

            var products = _context.Products
                               .Select(p => new SelectListItem
                               {
                                   Value = p.ProductId.ToString(),
                                   Text = p.ProductName
                               }).ToList();
            ViewBag.products = products;

            var customers = _context.Customers
                                .Select(c => new SelectListItem
                                {
                                    Value = c.CustomerId.ToString(),
                                    Text = c.CustomerName + " " + c.CustomerSurname
                                }).ToList();
            ViewBag.customers = customers;
            var value = await _context.orders.FindAsync(id);
            return View(value);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateOrder(Order order)
        {
            _context.orders.Update(order);
            await _context.SaveChangesAsync();
            return RedirectToAction("OrderList");
        }
        public IActionResult OrderListWithCustomerGroup()
        {
            var result = from customer in _context.Customers
                         join order in _context.orders
                         on customer.CustomerId equals order.CustomerId
                         into orderGroup
                         select new CustomerOrderViewModel
                         {
                             CustomerName = customer.CustomerName + " " + customer.CustomerSurname,
                             Orders = orderGroup.ToList()

                         };
            return View(result.ToList());
        }
    }
}