using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiPaises.Models;

namespace WebApiPaises.Controllers
{
    [Produces("application/json")]
    [Route("api/Pais/{PaisId}/Provincia")]
    public class ProvinciaController : Controller
    {
        private readonly ApplicationDbContext context;
        public ProvinciaController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<Provincia> GetAll(int PaisId)
        {
            return context.Provincias.Where(x => x.PaisId == PaisId).ToList();
        }

        [HttpGet("{id}", Name = "ProvinciaById")]
        public IActionResult GetById(int id)
        {
            var pais = context.Provincias.FirstOrDefault(x => x.Id == id);
            if (pais == null)
            {
                return NotFound();
            }
            return new ObjectResult(pais);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Provincia provincia,  int PaisId)
        {
           provincia.PaisId = PaisId;

           if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Provincias.Add(provincia);
            context.SaveChanges();

            return new CreatedAtRouteResult("ProvinciaById", new { id = provincia.Id });
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromBody] Provincia provincia, int id)
        {
            if (provincia.Id != id)
            {
                return BadRequest();
            }

            context.Entry(provincia).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var provincia = context.Provincias.FirstOrDefault(x => x.Id == id);
            //if (provincia == null)
            if (String.IsNullOrEmpty(id.ToString()))
            {
                    return NotFound();
            }
            context.Provincias.Remove(provincia);
            context.SaveChanges();
            return Ok(provincia);

        }

    }
}