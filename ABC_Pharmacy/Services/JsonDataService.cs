using System.Text.Json;
using ABC_Pharmacy.Models;
using Microsoft.AspNetCore.Hosting;

namespace ABC_Pharmacy.Services
{
    public class JsonDataService
    {
        private readonly string medicineFilePath;
        private readonly string salesFilePath;

        public JsonDataService(IWebHostEnvironment env)
        {
            medicineFilePath = Path.Combine(env.ContentRootPath, "Data", "medicines.json");
            salesFilePath = Path.Combine(env.ContentRootPath, "Data", "sales.json");
        }

        //Medicines
        public List<Medicine> GetMedicines()
        {
            if (!File.Exists(medicineFilePath))
                return new List<Medicine>();

            var json = File.ReadAllText(medicineFilePath);

            if (string.IsNullOrWhiteSpace(json))
                return new List<Medicine>();

            return JsonSerializer.Deserialize<List<Medicine>>(json) ?? new List<Medicine>();
        }

        public void SaveMedicines(List<Medicine> medicines)
        {
            var json = JsonSerializer.Serialize(medicines,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                });

            File.WriteAllText(medicineFilePath, json);
        }

        public Medicine AddMedicine(Medicine medicine)
        {
            var medicines = GetMedicines();

            medicine.Id = medicines.Any()
                ? medicines.Max(m => m.Id) + 1
                : 1;

            medicines.Add(medicine);

            SaveMedicines(medicines);

            return medicine;
        }

        public Medicine? UpdateMedicine(int id, Medicine updatedMedicine)
        {
            var medicines = GetMedicines();

            var medicine = medicines.FirstOrDefault(m => m.Id == id);

            if (medicine == null)
                return null;

            medicine.FullName = updatedMedicine.FullName;
            medicine.Notes = updatedMedicine.Notes;
            medicine.ExpiryDate = updatedMedicine.ExpiryDate;
            medicine.Quantity = updatedMedicine.Quantity;
            medicine.Price = updatedMedicine.Price;
            medicine.Brand = updatedMedicine.Brand;

            SaveMedicines(medicines);

            return medicine;
        }

        public bool DeleteMedicine(int id)
        {
            var medicines = GetMedicines();

            var medicine = medicines.FirstOrDefault(m => m.Id == id);

            if (medicine == null)
                return false;

            medicines.Remove(medicine);

            SaveMedicines(medicines);

            return true;
        }

        public List<Medicine> SearchMedicines(string search)
        {
            var medicines = GetMedicines();

            if (string.IsNullOrWhiteSpace(search))
                return medicines;

            return medicines
                .Where(m => m.FullName.Contains(search,
                       StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        //Sales
        public List<Sale> GetSales()
        {
            if (!File.Exists(salesFilePath))
                return new List<Sale>();

            var json = File.ReadAllText(salesFilePath);

            if (string.IsNullOrWhiteSpace(json))
                return new List<Sale>();

            return JsonSerializer.Deserialize<List<Sale>>(json) ?? new List<Sale>();
        }

        public void SaveSales(List<Sale> sales)
        {
            var json = JsonSerializer.Serialize(sales,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                });

            File.WriteAllText(salesFilePath, json);
        }

        public Sale? AddSale(Sale sale)
        {
            var medicines = GetMedicines();

            var medicine = medicines.FirstOrDefault(m => m.Id == sale.MedicineId);

            if (medicine == null)
                return null;

            if (medicine.Quantity < sale.QuantitySold)
                return null;

            // Reduce stock
            medicine.Quantity -= sale.QuantitySold;

            SaveMedicines(medicines);

            var sales = GetSales();

            sale.Id = sales.Any()
                ? sales.Max(s => s.Id) + 1
                : 1;

            sale.SaleDate = DateTime.Now;

            sales.Add(sale);

            SaveSales(sales);

            return sale;
        }


    }
}