using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebAPI.Models;
using System.Linq;
using SmartSchool.WebAPI.Data;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.V2.Dtos;
using AutoMapper;

namespace SmartSchool.WebAPI.V2.Controllers
{
    /// <summary>
    /// Classe Aluno V2
    /// </summary>
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly IRepository repo;
        private readonly IMapper mapper;

        /// <summary>
        /// Construtor da classe Aluno
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="mapper"></param>
        public AlunoController(IRepository repo, IMapper mapper)
        {
            this.mapper = mapper;
            this.repo = repo;
        }

        /// <summary>
        /// Metodo responsavel para retornar todos os alunos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {

            var alunos = this.repo.GetAllAlunos(true);
            return Ok(this.mapper.Map<IEnumerable<AlunoDto>>(alunos));
        }

        /// <summary>
        /// Metodo responsavel para retornar apenas um aluno por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Metodo responsavel para cadastrar um aluno
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Metodo responsavel para atualizar um aluno
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Metodo responsavel para deletar um aluno
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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