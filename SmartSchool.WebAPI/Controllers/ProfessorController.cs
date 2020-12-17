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
        private readonly IRepository repo;

        public ProfessorController(IRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = this.repo.GetAllProfessores(true);
            return Ok(result);
        }

        // api/professor/ById
        [HttpGet("ById/{id}")]
        public IActionResult GetById(int id)
        {
            var professor = this.repo.GetProfessorById(id, false);

            if (professor == null)
            {
                return BadRequest("Professor não encontrado.");
            }
            return Ok(professor);
        }        
        
        // api/professor
        [HttpPost]
        public IActionResult Post(Professor professor)
        {
            this.repo.Add(professor);

            if (this.repo.SaveChanges())
            {
                return Ok(professor);
            }

            return BadRequest("Professor não cadastrado!");
        }

        // api/professor
        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor)
        {
            var prof = this.repo.GetProfessorById(id, false);
            if (prof == null)
            {
                return BadRequest("Professor não encontrado.");
            }

            this.repo.Update(professor);

            if (this.repo.SaveChanges())
            {
                return Ok(professor);
            }

            return BadRequest("Professor não cadastrado!");
        }

        // api/professor
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor)
        {
            var prof = this.repo.GetProfessorById(id, false);
            if (prof == null)
            {
                return BadRequest("Professor não encontrado.");
            }

            this.repo.Update(professor);

            if (this.repo.SaveChanges())
            {
                return Ok(professor);
            }

            return BadRequest("Professor não cadastrado!");
        }

        // api/professor
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = this.repo.GetProfessorById(id, false);

            if (professor == null)
            {
                return BadRequest("Professor não encontrado.");
            }

            this.repo.Delete(professor);

            if (this.repo.SaveChanges())
            {
                return Ok("Professor deletado!");
            }

            return BadRequest("Professor não deletado!");
        }
    }
}