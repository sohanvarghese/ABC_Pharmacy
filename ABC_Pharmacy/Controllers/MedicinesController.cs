using ABC_Pharmacy.Models;
using ABC_Pharmacy.Services;
using Microsoft.AspNetCore.Mvc;

namespace ABC_Pharmacy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicinesController : ControllerBase
    {
        private readonly JsonDataService _jsonService;

        public MedicinesController(JsonDataService jsonService)
        {
            _jsonService = jsonService;
        }

        [HttpGet]
        public IActionResult GetMedicines(string? search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return Ok(_jsonService.GetMedicines());
            }

            return Ok(_jsonService.SearchMedicines(search));
        }

        [HttpPost]
        public IActionResult AddMedicine(Medicine medicine)
        {
            var addedMedicine = _jsonService.AddMedicine(medicine);

            return Ok(addedMedicine);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMedicine(int id, Medicine medicine)
        {
            var updatedMedicine = _jsonService.UpdateMedicine(id, medicine);

            if (updatedMedicine == null)
            {
                return NotFound("Medicine not found.");
            }

            return Ok(updatedMedicine);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMedicine(int id)
        {
            bool deleted = _jsonService.DeleteMedicine(id);

            if (!deleted)
            {
                return NotFound("Medicine not found.");
            }

            return Ok("Medicine deleted successfully.");
        }
    }
}