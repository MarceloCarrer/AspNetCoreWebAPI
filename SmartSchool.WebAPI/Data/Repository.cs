using System.Linq;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Data
{
    /// <summary>
    /// Classe do repositorio
    /// </summary>
    public class Repository : IRepository
    {
        private readonly SmartContext context;

        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="context"></param>
        public Repository(SmartContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Metodo de cadastar novo
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public void Add<T>(T entity) where T : class
        {
            this.context.Add(entity);
        }

        /// <summary>
        /// Metodo de atualizar
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public void Update<T>(T entity) where T : class
        {
            this.context.Update(entity);
        }

        /// <summary>
        /// Metodo de deletar
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public void Delete<T>(T entity) where T : class
        {
            this.context.Remove(entity);
        }

        /// <summary>
        /// Metodo de salvar
        /// </summary>
        /// <returns></returns>
        public bool SaveChanges()
        {
            return (this.context.SaveChanges() > 0);
        }

        /// <summary>
        /// Metodo de listar alunos
        /// </summary>
        /// <param name="includeProfessor"></param>
        /// <returns></returns>
        public Aluno[] GetAllAlunos(bool includeProfessor = false)
        {
            IQueryable<Aluno> query = this.context.Alunos;

            if (includeProfessor)
            {
                query = query.Include(a => a.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Disciplina)
                             .ThenInclude(d => d.Professor);
            }

            query = query.AsNoTracking().OrderBy(a => a.Id);

            return query.ToArray();
        }

        /// <summary>
        /// Metodo de listar aluno e disciplinas por Id
        /// </summary>
        /// <param name="disciplinaId"></param>
        /// <param name="includeProfessor"></param>
        /// <returns></returns>
        public Aluno[] GetAllAlunosByDisciplinaId(int disciplinaId, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = this.context.Alunos;

            if (includeProfessor)
            {
                query = query.Include(a => a.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Disciplina)
                             .ThenInclude(d => d.Professor);
            }

            query = query.AsNoTracking()
                         .OrderBy(a => a.Id)
                         .Where(aluno => aluno.AlunosDisciplinas.Any(ad => ad.DisciplinaId == disciplinaId))
            ;

            return query.ToArray();
        }

        /// <summary>
        /// Metodo de listar aluno por Id
        /// </summary>
        /// <param name="alunoId"></param>
        /// <param name="includeProfessor"></param>
        /// <returns></returns>
        public Aluno GetAlunoById(int alunoId, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = this.context.Alunos;

            if (includeProfessor)
            {
                query = query.Include(a => a.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Disciplina)
                             .ThenInclude(d => d.Professor);
            }

            query = query.AsNoTracking()
                         .OrderBy(a => a.Id)
                         .Where(aluno => aluno.Id == alunoId);

            return query.FirstOrDefault();
        }


        /// <summary>
        /// Metodo de listar professores
        /// </summary>
        /// <param name="includeAlunos"></param>
        /// <returns></returns>
        public Professor[] GetAllProfessores(bool includeAlunos = false)
        {
            IQueryable<Professor> query = this.context.Professores;

            if (includeAlunos)
            {
                query = query.Include(p => p.Disciplinas)
                             .ThenInclude(d => d.AlunoDisciplina)
                             .ThenInclude(ad => ad.Aluno);
            }

            query = query.AsNoTracking().OrderBy(p => p.Id);

            return query.ToArray();
        }

        /// <summary>
        /// Metodo de listar professor e disciplinas por Id
        /// </summary>
        /// <param name="disciplinaId"></param>
        /// <param name="includeAlunos"></param>
        /// <returns></returns>
        public Professor[] GetAllProfessoresByDisciplinaId(int disciplinaId, bool includeAlunos = false)
        {
            IQueryable<Professor> query = this.context.Professores;

            if (includeAlunos)
            {
                query = query.Include(p => p.Disciplinas)
                             .ThenInclude(d => d.AlunoDisciplina)
                             .ThenInclude(ad => ad.Aluno);
            }

            query = query.AsNoTracking()
                         .OrderBy(aluno => aluno.Id)
                         .Where(aluno => aluno.Disciplinas.Any(
                            d => d.AlunoDisciplina.Any(ad => ad.DisciplinaId == disciplinaId) 
                        ));
            ;

            return query.ToArray();
        }

        /// <summary>
        /// Metodo de listar professor por Id
        /// </summary>
        /// <param name="professorId"></param>
        /// <param name="includeProfessor"></param>
        /// <returns></returns>
        public Professor GetProfessorById(int professorId, bool includeProfessor = false)
        {
            IQueryable<Professor> query = this.context.Professores;

            if (includeProfessor)
            {
                query = query.Include(p => p.Disciplinas)
                             .ThenInclude(d => d.AlunoDisciplina)
                             .ThenInclude(ad => ad.Aluno);
            }

            query = query.AsNoTracking()
                         .OrderBy(a => a.Id)
                         .Where(professor => professor.Id == professorId);
            ;

            return query.FirstOrDefault();
        }
    }
}