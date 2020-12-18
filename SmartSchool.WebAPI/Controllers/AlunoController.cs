using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebAPI.Models;
using System.Linq;
using SmartSchool.WebAPI.Data;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Dtos;
using AutoMapper;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        public readonly IRepository repo;
        private readonly IMapper mapper;

        public AlunoController(IRepository repo, IMapper mapper)
        {
            this.mapper = mapper;
            this.repo = repo;
        }

        [HttpGet]
        public IActionResult Get()
        {

            var alunos = this.repo.GetAllAlunos(true);
            return Ok(this.mapper.Map<IEnumerable<AlunoDto>>(alunos));
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

            var alunoDto = this.mapper.Map<AlunoDto>(alumo);

            return Ok(alunoDto);
        }

        // api/aluno
        [HttpPost]
        public IActionResult Post(AlunoRegistrarDto model)
        {
            var aluno = this.mapper.Map<Aluno>(model);

            this.repo.Add(aluno);

            if (this.repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", this.mapper.Map<AlunoDto>(aluno));
            }

            return BadRequest("Aluno não cadastrado!");
        }

        // api/aluno
        [HttpPut("{id}")]
        public IActionResult Put(int id, AlunoRegistrarDto model)
        {
            var aluno = this.repo.GetAlunoById(id, false);
            if (aluno == null)
            {
                return BadRequest("Aluno não encontrado!");
            }

            this.mapper.Map(model, aluno);

            this.repo.Update(aluno);

            if (this.repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", this.mapper.Map<AlunoDto>(aluno));
            }

            return BadRequest("Aluno não cadastrado!");
        }

        // api/aluno
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, AlunoRegistrarDto model)
        {
            var aluno = this.repo.GetAlunoById(id, false);
            if (aluno == null)
            {
                return BadRequest("Aluno não encontrado!");
            }

            this.mapper.Map(model, aluno);

            this.repo.Update(aluno);

            if (this.repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", this.mapper.Map<AlunoDto>(aluno));
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