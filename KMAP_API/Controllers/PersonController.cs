using KMAP_API.Data;
using KMAP_API.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace KMAP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly KmapContext _context;

        public PersonController(KmapContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpGet("{id}", Name = "GetById")]
        public IActionResult Get(string id)
        {
            var student = _context.Find<Person>(Guid.Parse(id));

            return Ok(student);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Person student)
        {
            _context.Add(student);

            _context.SaveChanges();

            return Get(student.Id.ToString());
        }
    }
}
