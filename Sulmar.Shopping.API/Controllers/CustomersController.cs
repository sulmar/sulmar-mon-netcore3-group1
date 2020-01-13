using Microsoft.AspNetCore.Mvc;
using Sulmar.Shopping.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sulmar.Shopping.API.Controllers
{
    // MVC 4: [RoutePrefix("api/customers")]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        // api/customers
        [HttpGet]
        public IActionResult Get()
        {
            var customers = customerRepository.Get();

            return Ok(customers);
        }

        // api/customers/{id}
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var customer = customerRepository.Get(id);

            return Ok(customer);
        }
    }
}
