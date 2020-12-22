using System.Threading.Tasks;
using SmartSchool.WebAPI.Helpers;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Data
{
    public interface IRepository
    {        
        void Add<T>(T entity) where T : class;

        void Update<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        bool SaveChanges();

        
        Task<PageList<Aluno>> GetAllAlunosAsync(PageParams pageParams, bool includeProfessor = false);

        Aluno[] GetAllAlunos(bool includeProfessor = false);
        
        Task<Aluno[]> GetAllAlunosByDisciplinaIdAsync(int disciplinaId, bool includeProfessor = false);

        Aluno[] GetAllAlunosByDisciplinaId(int disciplinaId, bool includeProfessor = false);

        Task<Aluno> GetAlunoByIdAsync(int alunoId, bool includeProfessor = false);

        Aluno GetAlunoById(int alunoId, bool includeProfessor = false);


        Task<Professor[]> GetAllProfessoresAsync(bool includeAlunos = false);

        Professor[] GetAllProfessores(bool includeAlunos = false);

        Task<Professor[]> GetAllProfessoresByDisciplinaIdAsync(int disciplinaId, bool includeAlunos = false);

        Professor[] GetAllProfessoresByDisciplinaId(int disciplinaId, bool includeAlunos = false);

        Task<Professor> GetProfessorByIdAsync(int professorId, bool includeProfessor = false);

        Professor GetProfessorById(int professorId, bool includeProfessor = false);

    }
}