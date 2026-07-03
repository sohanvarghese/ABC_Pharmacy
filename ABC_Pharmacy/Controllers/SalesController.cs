using ABC_Pharmacy.Models;
using ABC_Pharmacy.Services;
using Microsoft.AspNetCore.Mvc;

namespace ABC_Pharmacy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly JsonDataService _jsonService;

        public SalesController(JsonDataService jsonService)
        {
            _jsonService = jsonService;
        }

        [HttpGet]
        public IActionResult GetSales()
        {
            return Ok(_jsonService.GetSales());
        }

        [HttpPost]
        public IActionResult AddSale(Sale sale)
        {
            var result = _jsonService.AddSale(sale);

            if (result == null)
            {
                return BadRequest("Invalid medicine or insufficient stock.");
            }

            return Ok(result);
        }
    }
}