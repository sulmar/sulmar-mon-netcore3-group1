using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sulmar.Shopping.Domain;
using Sulmar.Shopping.Domain.SearchCriterias;
using Sulmar.Shopping.Domain.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Sulmar.Shopping.API.Controllers
{
   // [Authorize(Roles = "developer, trainer")]
    [Route("api/customers")]
    public class CustomersControllerAsync : ControllerBase
    {
        private readonly ICustomerRepositoryAsync customerRepository;

        public CustomersControllerAsync(ICustomerRepositoryAsync customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer =  await customerRepository.GetAsync(id);

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        [AllowAnonymous]

        [HttpGet("{pesel:length(11)}")]
        public async Task<IActionResult> Get(string pesel)
        {
            var customer = await customerRepository.GetAsync(pesel);

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] CustomerSearchCriteria criteria)
        {

            if (!this.User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }

            Trace.WriteLine(this.User.FindFirst(ClaimTypes.Email).Value);

            if (this.User.IsInRole("trainer"))
            {
                Trace.WriteLine(this.User.Identity.Name);
            }

            var customers = await customerRepository.GetAsync(criteria);

            return Ok(customers);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Customer customer)
        {
            if (!this.ModelState.IsValid)
                return BadRequest(ModelState);

            await customerRepository.AddAsync(customer);

            return CreatedAtAction(nameof(GetById), new { Id = customer.Id }, customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Customer customer)
        {
            if (id != customer.Id)
                return BadRequest();

            await customerRepository.UpdateAsync(customer);

            return NoContent();
        }

        // DELETE api/customers/10
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await customerRepository.RemoveAsync(id);

            return NoContent();
        }

    }

    // MVC 4: [RoutePrefix("api/customers")]
    [Route("api/customers/v1")]
  //  [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        // api/customers
        //[HttpGet]
        //public IActionResult Get()
        //{
        //    var customers = customerRepository.Get();

        //    return Ok(customers);
        //}

 

        // api/customers/{id}
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var customer = customerRepository.Get(id);

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        // api/customers/776464464

        [HttpGet("{pesel:length(11)}")]
        public IActionResult Get(string pesel)
        {
            var customer = customerRepository.Get(pesel);

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        // api/customers?firstname=Marcin&lastName=Sulecki

        //[HttpGet]
        //public IActionResult Get([FromQuery] string firstname, [FromQuery] string lastname, [FromQuery] bool isRemoved)
        //{
        //    CustomerSearchCriteria criteria = new CustomerSearchCriteria
        //    {
        //        FirstName = firstname,
        //        LastName = lastname,
        //        IsRemoved = isRemoved
        //    };

        //    var customers = customerRepository.Get(criteria);

        //    return Ok(customers);
        //}
        
        [HttpGet]
        public IActionResult Get([FromQuery] CustomerSearchCriteria criteria)
        {
            var customers = customerRepository.Get(criteria);

            return Ok(customers);
        }

        // własna reguła
        // https://github.com/sulmar/dotnet-core-routecontraint-polish-validators/blob/master/webapi/ConstraintExtensions.cs
   

        [HttpPost]
        public IActionResult Post([FromBody] Customer customer)
        {
            if (!this.ModelState.IsValid)
                return BadRequest(ModelState);

            customerRepository.Add(customer);

            // zla praktyka
            // return Created($"http://localhost:5000/api/customers/{customer.Id}", customer);

            return CreatedAtAction(nameof(GetById), new { Id = customer.Id }, customer);
        }

        // PUT api/customers/10
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Customer customer)
        {
            if (id != customer.Id)
                return BadRequest();

            customerRepository.Update(customer);

            return NoContent();
        }

        // DELETE api/customers/10
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            customerRepository.Remove(id);

            return NoContent();
        }
    
    }
}
