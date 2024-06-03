using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Estacionamento.Database;
using Estacionamento.Models;
using Estacionamento.ModelView;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Estacionamento.Controllers
{
    [Route("v1")]
    public class VehicleController : ControllerBase
    {
        [HttpGet("vehicles")]
        public async Task<IActionResult> GetAsync(
            [FromServices] AppDbContext context
        )
        {
            var vehicle = await context
                .Vehicles
                .ToListAsync();

            return vehicle == null ? NotFound() : Ok(vehicle);
        }

        [HttpPost("vehicles")]
        public async Task<IActionResult> PostAsync(
            [FromServices] AppDbContext context,
            [FromBody] CreateVehicleModel model
        ) 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var vehicle = await context
                .Vehicles
                .FirstOrDefaultAsync(vehicle => vehicle.Placa == model.Placa);

            if (vehicle != null)
                return BadRequest();


            var newVehicle = new Vehicles
            {
                Brand = model.Brand,
                Model = model.Model,
                Color = model.Color,
                Placa = model.Placa,
                Type = model.Type,
                CompanyId = model.CompanyId

            };

            try
            {
                await context.Vehicles.AddAsync(newVehicle);
                await context.SaveChangesAsync();
                return Created("v1/vehicles", newVehicle);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpGet("vehicles/{companyId}")]
        public async Task<IActionResult> GetVehicleByCompany(
            [FromServices] AppDbContext context,
            [FromRoute] int companyId
        )
        {
            var vehicle = await context
                .Vehicles
                .Where(v => v.CompanyId == companyId)
                .ToListAsync();

            if (vehicle == null)
                return NotFound(new { msg = "Nenhum veiculo cadastrado a essa empresa" });

            return Ok(vehicle);

        }

    }
}