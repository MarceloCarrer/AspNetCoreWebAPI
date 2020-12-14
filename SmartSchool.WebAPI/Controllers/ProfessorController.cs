using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly SmartContext context;
        public ProfessorController(SmartContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
             return Ok(this.context.Professores);
        }

        // api/professor/ById
        [HttpGet("ById/{id}")]
        public IActionResult GetById(int id)
        {
            var professor = this.context.Professores.FirstOrDefault(a => a.Id == id);
            if (professor == null)
            {
                return BadRequest("professor não encontrado.");
            }
            return Ok(professor);
        }

        // api/professor/nome
        [HttpGet("ByName")]
        public IActionResult GetByName(string nome)
        {
            var professor = this.context.Professores.FirstOrDefault(a => 
                a.Nome.Contains(nome)
            );

            if (professor == null)
            {
                return BadRequest("professor não encontrado.");
            }
            return Ok(professor);
        }

        // api/professor
        [HttpPost]
        public IActionResult Post(Professor professor)
        {
            this.context.Add(professor);
            this.context.SaveChanges();
            return Ok(professor);
        }

        // api/professor
        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor)
        {
            var alu = this.context.Professores.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (alu == null)
            {
                return BadRequest("professor não encontrado.");
            }

            this.context.Update(professor);
            this.context.SaveChanges();
            return Ok(professor);
        }

        // api/professor
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor)
        {
            var alu = this.context.Professores.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (alu == null)
            {
                return BadRequest("professor não encontrado.");
            }

            this.context.Update(professor);
            this.context.SaveChanges();
            return Ok(professor);
        }

        // api/professor
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = this.context.Professores.FirstOrDefault(a => a.Id == id);

            if (professor == null)
            {
                return BadRequest("professor não encontrado.");
            }

            this.context.Remove(professor);
            this.context.SaveChanges();
            return Ok();
        }
    }
}