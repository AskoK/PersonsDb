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
    public class FieldController : ControllerBase
    {
        private readonly IFieldService _fieldService;

        public FieldController(IFieldService fieldService)
        {
            _fieldService = fieldService;
        }

        [HttpGet]
        public ActionResult<List<Fields>> Get()
        {
            return _fieldService.Get();
        }

        [HttpGet("{id:length(24)}", Name = "GetField")]
        public ActionResult<Fields> Get(string id)
        {
            var field = _fieldService.Get(id);

            if (field == null)
            {
                return NotFound();
            }
            return field;
        }
    }
}