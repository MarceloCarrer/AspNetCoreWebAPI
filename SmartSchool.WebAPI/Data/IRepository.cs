using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Data
{
    /// <summary>
    /// Interface da classe de repositorio
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Metodo de cadastar novo
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        void Add<T>(T entity) where T : class;

        /// <summary>
        /// Metodo de atualizar
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        void Update<T>(T entity) where T : class;

        /// <summary>
        /// Metodo de deletar
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        void Delete<T>(T entity) where T : class;

        /// <summary>
        /// Metodo de salvar
        /// </summary>
        /// <returns></returns>
        bool SaveChanges();


        /// <summary>
        /// Metodo de listar alunos
        /// </summary>
        /// <param name="includeProfessor"></param>
        /// <returns></returns>
        Aluno[] GetAllAlunos(bool includeProfessor = false);

        /// <summary>
        /// Metodo de listar aluno e disciplinas por Id
        /// </summary>
        /// <param name="disciplinaId"></param>
        /// <param name="includeProfessor"></param>
        /// <returns></returns>
        Aluno[] GetAllAlunosByDisciplinaId(int disciplinaId, bool includeProfessor = false);

        /// <summary>
        /// Metodo de listar aluno por Id
        /// </summary>
        /// <param name="alunoId"></param>
        /// <param name="includeProfessor"></param>
        /// <returns></returns>
        Aluno GetAlunoById(int alunoId, bool includeProfessor = false);


        /// <summary>
        /// Metodo de listar professores
        /// </summary>
        /// <param name="includeAlunos"></param>
        /// <returns></returns>
        Professor[] GetAllProfessores(bool includeAlunos = false);

        /// <summary>
        /// Metodo de listar professor e disciplinas por Id
        /// </summary>
        /// <param name="disciplinaId"></param>
        /// <param name="includeAlunos"></param>
        /// <returns></returns>
        Professor[] GetAllProfessoresByDisciplinaId(int disciplinaId, bool includeAlunos = false);

        /// <summary>
        /// Metodo de listar professor por Id
        /// </summary>
        /// <param name="professorId"></param>
        /// <param name="includeProfessor"></param>
        /// <returns></returns>
        Professor GetProfessorById(int professorId, bool includeProfessor = false);

    }
}