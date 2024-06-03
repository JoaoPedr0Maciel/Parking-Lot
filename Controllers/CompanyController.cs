using Estacionamento.Database;
using Estacionamento.Helpers;
using Estacionamento.Models;
using Estacionamento.ModelView;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Estacionamento.Controllers
{   
    [ApiController]
    [Route("v1")]
    public class CompanyController : ControllerBase
    {
        [HttpGet("companys")]
        public async Task<IActionResult> GetAsync(
            [FromServices] AppDbContext context
        )
        {
            var companys = await context
                .Companys
                .AsNoTracking()
                .ToListAsync();

            return Ok(companys);
        }

        [HttpPost("company")]
        public async Task<IActionResult> PostAsync(
            [FromServices] AppDbContext context,
            [FromBody] CreateCompanyModel model
        )
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var companyExist = await context
                .Companys
                .AsNoTracking()
                .FirstOrDefaultAsync(company => company.Cnpj == model.Cnpj);

            if (companyExist != null)
                return BadRequest("Já existe uma empresa com esse CNPJ");

            var company = new Company
            {
                Name = model.Name,
                Cnpj = model.Cnpj,
                Address = model.Address,
                Phone = model.Phone,
                CarSpace = model.CarSpace,
                BikeSpace = model.BikeSpace,
                CarHour = model.CarHour,
                BikeHour = model.BikeHour
            };
            
            try
            {
                await context.Companys.AddAsync(company);
                await context.SaveChangesAsync();
                return Created("v1/company", company);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        [HttpPut("company/{id}")]
        public async Task<IActionResult> PutAsync(
            [FromServices] AppDbContext context,
            [FromBody] UpdateCompanyModel model,
            [FromRoute] int id
        )
        {
            var company = await context
                .Companys
                .FirstOrDefaultAsync(company => company.Id == id);

            if (company == null)
                return NotFound("Nenhuma empresa com esse id");

            try
            {
                UpdateCompanyHelper.UpdateCompany(company, model);

                context.Companys.Update(company);
                await context.SaveChangesAsync();
                return Ok(company);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("company/{id}")]
        public async Task<IActionResult> DeleteAsync(
            [FromServices] AppDbContext context,
            [FromRoute] int id
        )
        {
            var company = await context
                .Companys
                .FirstOrDefaultAsync(company => company.Id == id);

            if (company == null)
                return NotFound("Empresa não encontrada");
            
            try
            {
                context.Companys.Remove(company);
                await context.SaveChangesAsync();
                return Ok($"Empresa {company.Name} foi deletada");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}