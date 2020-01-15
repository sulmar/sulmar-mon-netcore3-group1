using Microsoft.AspNetCore.Mvc;
using Sulmar.Shopping.Domain;
using Sulmar.Shopping.Domain.SearchCriterias;
using Sulmar.Shopping.Domain.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace Sulmar.Shopping.API.Controllers
{
    // MVC 4: [RoutePrefix("api/customers")]
    [Route("api/customers")]
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
