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
        public readonly IRepository repo; 

        public AlunoController(IRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = this.repo.GetAllAlunos(true);
            return Ok(result);
        }

        // api/aluno/ById
        [HttpGet("ById/{id}")]
        public IActionResult GetById(int id)
        {
            var alumo = this.repo.GetAlunoById(id, false);

            if (alumo == null)
            {
                return BadRequest("Aluno não encontrado!");
            }
            return Ok(alumo);
        }

        // api/aluno
        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            this.repo.Add(aluno);

            if (this.repo.SaveChanges())
            {
                return Ok(aluno);
            }

            return BadRequest("Aluno não cadastrado!");
        }

        // api/aluno
        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            var alu = this.repo.GetAlunoById(id, false);
            if (alu == null)
            {
                return BadRequest("Aluno não encontrado!");
            }

            this.repo.Update(aluno);

            if (this.repo.SaveChanges())
            {
                return Ok(aluno);
            }

            return BadRequest("Aluno não cadastrado!");
        }

        // api/aluno
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            var alu = this.repo.GetAlunoById(id, false);
            if (alu == null)
            {
                return BadRequest("Aluno não encontrado!");
            }

            this.repo.Update(aluno);

            if (this.repo.SaveChanges())
            {
                return Ok(aluno);
            }

            return BadRequest("Aluno não cadastrado!");
        }

        // api/aluno
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = this.repo.GetAlunoById(id, false);

            if (aluno == null)
            {
                return BadRequest("Aluno não encontrado!");
            }

            this.repo.Delete(aluno);

            if (this.repo.SaveChanges())
            {
                return Ok("Aluno deletado!");
            }

            return BadRequest("Aluno não deletado!");
        }
    }
}