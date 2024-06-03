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
    [Route("v1")]
    public class MovementsController : ControllerBase
    {
        [HttpGet("movements")]
        public async Task<IActionResult> GetAsync(
            [FromServices] AppDbContext context
        )
        {
           var movements = await context
                .Movements   
                .ToListAsync();

            return movements == null ? NotFound() : Ok(movements);

        }

        [HttpGet("movements/{id}")]
        public async Task<IActionResult> GetMovementById(
            [FromServices] AppDbContext context,
            [FromRoute] int id
        )
        {
            
            var movementById = await context
                .Movements
                .Where(mov => mov.VehicleId == id)
                .ToListAsync();

            return  Ok(movementById);
        }




    
        [HttpPost("movements/entry")]
        public async Task<IActionResult> EntryAction(
            [FromServices] AppDbContext context,
            [FromBody] Movements model
        )
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var vehicle = await context
                .Vehicles
                .FirstOrDefaultAsync(car => car.Id == model.VehicleId);

            if (vehicle == null)
                return NotFound("Cadastre esse veiculo primeiro");

            var company = await context
                .Companys
                .FirstOrDefaultAsync(comp => comp.Id == vehicle.CompanyId);

            if (company == null)
                return NotFound("Empresa não existe");


            if(model.MovementType == "Entrada")
            {
                if (vehicle.Type == "Carro" && company.CarSpace > 0)
                {
                    company.CarSpace--;
                } 
                else if (vehicle.Type == "Moto" && company.BikeSpace > 0) 
                {
                    company.BikeSpace--;
                } else 
                {
                    return NotFound("Nenhuma vaga disponível");
                }
               
            }
             

            var newMovement = new Movements
            {
                MovementType = model.MovementType ?? "Entrada",
                VehicleId = model.VehicleId,
                ExitTime = null,
                Amount = 0

            };


            try
            {
                await context.AddAsync(newMovement);
                await context.SaveChangesAsync();
                
                context.Companys.Update(company);
                await context.SaveChangesAsync();
                return Created("v1/movements", newMovement);
            }
            catch (Exception)
            {
                return BadRequest();
            }
           
        }

        [HttpPut("movements/exit/{id}")]
        public async Task<IActionResult> ExitAction(
            [FromServices] AppDbContext context,
            [FromRoute] int id
        )
        { 
            var lastMovement = await context
                .Movements
                .Where(mov => mov.VehicleId == id && mov.MovementType == "Entrada")
                .OrderByDescending(mov => mov.EntryTime)
                .FirstOrDefaultAsync();

            if (lastMovement == null)
                return NotFound(new {msg = "Esse veiculo não entrou no estacionamento ainda"});

            var vehicle = await context.Vehicles.FirstOrDefaultAsync(v => v.Id == id);

            if (vehicle == null)
                return NotFound("Veículo não encontrado.");

            var company = await context
                .Companys
                .FirstOrDefaultAsync(comp => comp.Id == vehicle.CompanyId);

            if (company == null)
                return NotFound("Empresa não encontrada");

            lastMovement.ExitTime = DateTime.Now;
            TimeSpan? timeElapsed = lastMovement.ExitTime - lastMovement.EntryTime;

            if (!timeElapsed.HasValue)
                return BadRequest("A hora de saída não pode ser anterior à hora de entrada.");

            // Calcular o número total de horas decorridas
            double totalHours = timeElapsed.Value.TotalHours;

            // Calcular o valor do montante com base na taxa e no tempo decorrido
            decimal rateValue = vehicle.Type == "Carro" ? company.CarHour : company.BikeHour;
            decimal amount = Math.Round(rateValue * (decimal)totalHours, 2);

            

            if(lastMovement.MovementType == "Entrada" && vehicle.Type == "Carro")
            {
                company.CarSpace++;
            }
            else if (lastMovement.MovementType == "Entrada" && vehicle.Type == "Moto")
            {
                company.BikeSpace++;
            }
            else
            {
                return BadRequest("Esse veiculo não entrou no estacionamento ainda");
            }

            lastMovement.Amount = amount;
            lastMovement.MovementType = "Saída";

            try
            {
                context.Movements.Update(lastMovement);
                context.Companys.Update(company);
                await context.SaveChangesAsync();

                return Ok(lastMovement);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }


        [HttpDelete("movements/{id}")]
        public async Task<IActionResult> DeleteAsync(
            [FromServices] AppDbContext context,
            [FromRoute] int id
        )
        {
            var movement = await context
                .Movements
                .FirstOrDefaultAsync(mov => mov.Id == id);

            if (movement == null)
                return NotFound("Nenhum movimento encontrado");

            try
            {
                context.Movements.Remove(movement);
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
        }

    }
}