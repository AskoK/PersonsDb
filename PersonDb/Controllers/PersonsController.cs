using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PersonDb.Models;
using PersonDb.Services.Interfaces;

namespace PersonDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonsController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public ActionResult<List<Persons>> Get()
        {
            return _personService.Get();
        }

        [HttpGet("{id:length(24)}", Name = "GetPerson")]
        public ActionResult<Persons> Get(string id)
        {
            var person = _personService.Get(id);

            if (person == null)
            {
                return NotFound();
            }
            return person;
        }

        [HttpPost]
        public ActionResult<Persons> Create(Persons person)
        {
            _personService.Create(person);

            return CreatedAtRoute("GetPerson", new { id = person.Id.ToString() }, person);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Persons personIn)
        {
            var person = _personService.Get(id);

            if (person == null)
            {
                return NotFound();
            }
            _personService.Update(id, personIn);

            return NoContent();
        }
    }
}