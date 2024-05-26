using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TTMDotNetCore.PizzaApi.Db;
using TTMDotNetCore.PizzaApi.Features.Queries;
using TTMDotNetCore.Shared;

namespace TTMDotNetCore.PizzaApi.Features.Pizza
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly DapperService _dapperService;

        public PizzaController()
        {
            _appDbContext = new AppDbContext();
            _dapperService = new DapperService(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var lst = await _appDbContext.Pizzas.ToListAsync();
            return Ok(lst);
        }

        [HttpGet("Extras")]
        public async Task<IActionResult> GetExtrasAsync()
        {
            var lst = await _appDbContext.PizzaExtras.ToListAsync();
            return Ok(lst);
        }

        /*[HttpGet("Order/{invoiceNo}")]
        public async Task<IActionResult> GetOrderAsync(string invoiceNo)
        {
            var item = await _appDbContext.PizzaOrders.FirstOrDefaultAsync(x => x.InvoiceNo == invoiceNo);
            var itemDetail = await _appDbContext.PizzaOrderDetails.Where(x => x.InvoiceNo == invoiceNo).ToListAsync();
            return Ok(new { Order = item, OrderDetail = itemDetail });
        }*/

        [HttpGet("Order/{invoiceNo}")]
        public IActionResult GetOrder(string invoiceNo)
        {
            var item = _dapperService.QueryFirstOrDefault<PizzaOrderInvoiceHeadModel>
                (
                    PizzaQuery.PizzaOrderQuery,
                    new { PizzaOrderInvoiceNo = invoiceNo }
                );

            var lst = _dapperService.Query<PizzaOrderInvoiceDetailModel>
                (
                    PizzaQuery.PizzaOrderDetailQuery,
                    new { PizzaOrderInvoiceNo = invoiceNo }
                );

            var model = new PizzaOrderInvoiceResponse
            {
                Order = item,
                OrderDetail = lst
            };

            return Ok(model);
        } 

        [HttpPost("Order")]
        public async Task<IActionResult> OrderAsync(OrderRequest orderRequest)
        {
            var itemPizza = await _appDbContext.Pizzas.FirstOrDefaultAsync(x => x.Id == orderRequest.PizzaId);
            var total = itemPizza is null ? 0 : itemPizza.Price;

            if(orderRequest.Extras.Length > 0 )
            {
                var extrasLst = await _appDbContext.PizzaExtras.Where(x => orderRequest.Extras.Contains(x.Id)).ToListAsync();
                total += extrasLst.Sum(x => x.Price);
            }

            string invoiceNo = DateTime.Now.ToString("yyyyMMddHHmmss");

            PizzaOrderModel pizzaOrderModel = new PizzaOrderModel()
            {
                PizzaId = orderRequest.PizzaId,
                InvoiceNo = invoiceNo,
                Total = total
            };

            List<PizzaOrderDetailModel> pizzaOrderDetailModels = orderRequest.Extras.Select(extraId => new PizzaOrderDetailModel()
            {
                PizzaExtraId = extraId,
                InvoiceNo = invoiceNo
            }).ToList();

            await _appDbContext.PizzaOrders.AddAsync(pizzaOrderModel);
            await _appDbContext.PizzaOrderDetails.AddRangeAsync(pizzaOrderDetailModels);
            await _appDbContext.SaveChangesAsync();

            OrderResponse orderResponse = new OrderResponse()
            {
                InvoiceNo = invoiceNo,
                Message = "Thank you for  your order! Enjoy your pizza!",
                Total = total
            };

            return Ok(orderResponse);
        }
    }
}
