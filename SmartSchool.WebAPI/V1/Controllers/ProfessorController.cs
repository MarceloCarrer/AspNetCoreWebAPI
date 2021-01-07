using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.V1.Dtos;
using SmartSchool.WebAPI.Models;
using System.Threading.Tasks;

namespace SmartSchool.WebAPI.V1.Controllers
{
    /// <summary>
    /// Classe Professor
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly IRepository repo;
        private readonly IMapper mapper;

        /// <summary>
        /// Construtor da classe Professor
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="mapper"></param>
        public ProfessorController(IRepository repo, IMapper mapper)
        {
            this.mapper = mapper;
            this.repo = repo;
        }

        /// <summary>
        /// Metodo responsavel para retornar todos os professores
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var professores = await this.repo.GetAllProfessoresAsync(true);
            return Ok(this.mapper.Map<IEnumerable<ProfessorDto>>(professores));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("getRegister")]
        public IActionResult GetRegister()
        {
            return Ok(new ProfessorRegistrarDto());
        }

        /// <summary>
        /// Metodo responsavel para retornar apenas um professor por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // api/professor/ById
        [HttpGet("ById/{id}")]
        public IActionResult GetById(int id)
        {
            var professor = this.repo.GetProfessorById(id, true);

            if (professor == null)
            {
                return BadRequest("Professor não encontrado.");
            }

            var professorDto = this.mapper.Map<ProfessorDto>(professor);

            return Ok(professor);
        }

        /// <summary>
        /// Metodo responsavel para retornar apenas um professor por Id
        /// </summary>
        /// <param name="alunoId"></param>
        /// <returns></returns>
        // api/professor/ById
        [HttpGet("ByAluno/{alunoId}")]
        public IActionResult GetByAlunoId(int alunoId)
        {
            var professores = this.repo.GetProfessoresByAlunoId(alunoId, true);

            if (professores == null)
            {
                return BadRequest("Professores não encontrados.");
            }

            return Ok(this.mapper.Map<IEnumerable<ProfessorDto>>(professores));
        }

        /// <summary>
        /// Metodo responsavel para cadastrar um professor
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Metodo responsavel para atualizar um professor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Metodo responsavel para atualizar um professor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Metodo responsavel para deletar um professor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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