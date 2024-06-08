using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estacionamento.Database;
using Estacionamento.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Estacionamento.Controllers
{
    [ApiController]
    [Route("v1")]
    public class ReportsDailyController : ControllerBase
    {
        [HttpPost("reports")]
        public async Task<IActionResult> GetReports(
            [FromServices] AppDbContext context
        )
        {
            var today = DateTime.Today;
            var movementsByDay = await context
                .Movements
                .Where(mov => mov.ExitTime.HasValue && mov.ExitTime.Value.Date == today)
                .ToListAsync();

            var amount = movementsByDay.Sum(mov => mov.Amount);
            var entries = movementsByDay.Count(entry => entry.EntryTime.Date == today);
            var exits = movementsByDay.Count(exit => exit.ExitTime.HasValue && exit.ExitTime.Value.Date == today);

            var report = new ReportsDaily
            {
                Entries = entries,
                Exits = exits,
                TotalAmount = amount
            };

            try
            {
                await context.AddAsync(report);
                await context.SaveChangesAsync();
                return Created("v1/reports", report);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

    }
}