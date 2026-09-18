using Microsoft.AspNetCore.Mvc;
using company_backend.Domain.Entities;
using company_backend.Infrastructure.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace company_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly company_backend.Application.Interfaces.ICompanyRepository _repo;

        public CompanyController(company_backend.Application.Interfaces.ICompanyRepository repo)
        {
            _repo = repo;
        }

        // GET: api/<CompanyController>
        // Optional query parameter 'q' filters by partial/full company name or website domain
        [HttpGet]
        public async System.Threading.Tasks.Task<IActionResult> Get([FromQuery] string? q)
        {
            if (string.IsNullOrWhiteSpace(q))
            {
                var companies = await _repo.GetAllAsync();
                return Ok(companies);
            }

            var filtered = await _repo.SearchAsync(q);
            return Ok(filtered);
        }

        // GET api/<CompanyController>/5
        [HttpGet("{id}")]
        public async System.Threading.Tasks.Task<IActionResult> Get(int id)
        {
            var company = await _repo.GetAsync(id);
            if (company is null) return NotFound();
            return Ok(company);
        }

        // POST api/<CompanyController>
        [HttpPost]
        public async System.Threading.Tasks.Task<IActionResult> Post([FromBody] Company company)
        {
            if (company == null)
                return BadRequest();

            var id = await _repo.SaveAsync(company);

            return Created($"/api/Company/{id}", company);
        }

        // PUT api/<CompanyController>/5
        [HttpPut("{id}")]
        public async System.Threading.Tasks.Task<IActionResult> Put(int id, [FromBody] Company company)
        {
            if (company == null) return BadRequest();

            var existing = await _repo.GetAsync(id);
            if (existing is null) return NotFound();

            await _repo.SaveAsync(company);
            return NoContent();
        }

        // DELETE api/<CompanyController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Not implemented in interface; keep simple
            return NoContent();
        }
    }
}
