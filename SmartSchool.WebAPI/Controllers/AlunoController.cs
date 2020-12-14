using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebAPI.Models;
using System.Linq;
using SmartSchool.WebAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly SmartContext context;

        public AlunoController(SmartContext context)
        {
            this.context = context;
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(this.context.Alunos);
        }

        // api/aluno/ById
        [HttpGet("ById/{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = this.context.Alunos.FirstOrDefault(a => a.Id == id);
            if (aluno == null)
            {
                return BadRequest("Aluno não encontrado.");
            }
            return Ok(aluno);
        }

        // api/aluno/nome
        [HttpGet("ByName")]
        public IActionResult GetByName(string nome, string sobrenome)
        {
            var aluno = this.context.Alunos.FirstOrDefault(a => 
                a.Nome.Contains(nome) && a.Sobrenome.Contains(sobrenome)
            );

            if (aluno == null)
            {
                return BadRequest("Aluno não encontrado.");
            }
            return Ok(aluno);
        }

        // api/aluno
        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            this.context.Add(aluno);
            this.context.SaveChanges();
            return Ok(aluno);
        }

        // api/aluno
        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            var alu = this.context.Alunos.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (alu == null)
            {
                return BadRequest("Aluno não encontrado.");
            }

            this.context.Update(aluno);
            this.context.SaveChanges();
            return Ok(aluno);
        }

        // api/aluno
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            var alu = this.context.Alunos.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (alu == null)
            {
                return BadRequest("Aluno não encontrado.");
            }

            this.context.Update(aluno);
            this.context.SaveChanges();
            return Ok(aluno);
        }

        // api/aluno
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = this.context.Alunos.FirstOrDefault(a => a.Id == id);

            if (aluno == null)
            {
                return BadRequest("Aluno não encontrado.");
            }

            this.context.Remove(aluno);
            this.context.SaveChanges();
            return Ok();
        }
    }
}