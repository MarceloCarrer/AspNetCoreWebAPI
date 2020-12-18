using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Dtos;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly IRepository repo;
        private readonly IMapper mapper;

        public ProfessorController(IRepository repo, IMapper mapper)
        {
            this.mapper = mapper;
            this.repo = repo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var professores = this.repo.GetAllProfessores(true);
            return Ok(this.mapper.Map<IEnumerable<ProfessorDto>>(professores));
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

            var professorDto = this.mapper.Map<ProfessorDto>(professor);

            return Ok(professorDto);
        }

        // api/professor
        [HttpPost]
        public IActionResult Post(ProfessorRegistrarDto model)
        {
            var professor = this.mapper.Map<Professor>(model);
            
            this.repo.Add(professor);

            if (this.repo.SaveChanges())
            {
                return Created($"/api/professor/{model.Id}", this.mapper.Map<ProfessorDto>(professor));
            }

            return BadRequest("Professor não cadastrado!");
        }

        // api/professor
        [HttpPut("{id}")]
        public IActionResult Put(int id, ProfessorRegistrarDto model)
        {
            var professor = this.repo.GetProfessorById(id, false);
            if (professor == null)
            {
                return BadRequest("Professor não encontrado.");
            }

            this.mapper.Map(model, professor);

            this.repo.Update(professor);

            if (this.repo.SaveChanges())
            {
                return Created($"/api/professor/{model.Id}", this.mapper.Map<ProfessorDto>(professor));
            }

            return BadRequest("Professor não cadastrado!");
        }

        // api/professor
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, ProfessorRegistrarDto model)
        {
            var professor = this.repo.GetProfessorById(id, false);
            if (professor == null)
            {
                return BadRequest("Professor não encontrado.");
            }

            this.mapper.Map(model, professor);

            this.repo.Update(professor);

            if (this.repo.SaveChanges())
            {
                return Created($"/api/professor/{model.Id}", this.mapper.Map<ProfessorDto>(professor));
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