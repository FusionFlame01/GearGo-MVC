using System;
using Microsoft.AspNetCore.Mvc;
using GearGo.Models;

namespace GearGo.Controllers
{
    public class EquipmentController : Controller
    {
        public IActionResult Index()
        {
            var equipmentList = new List<Equipment>
            {
                new Equipment { Id = 1, Name = "Tent", Description = "4‑person camping tent", Category = "Camping", DailyRate = 150, IsAvailable = true },
                new Equipment { Id = 2, Name = "Drill", Description = "Cordless power drill", Category = "Tools", DailyRate = 120, IsAvailable = false }
            };
            return View(equipmentList);
        }

        public IActionResult Details(int id)
        {
            var equipment = new Equipment { Id = id, Name = "Tent", Description = "4‑person camping tent", Category = "Camping", DailyRate = 150, IsAvailable = true };
            return View(equipment);
        }
    }
}
