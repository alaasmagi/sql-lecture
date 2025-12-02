using Domain;

namespace Repository;

public interface IRepository
{
    // CREATE methods
    bool CreateCurriculum(Curriculum curriculum);
    bool CreateSubject(Subject subject);
    
    // READ methods
    Curriculum? GetCurriculumById(Guid curriculumId);
    List<Curriculum> GetAllCurriculums();
    List<Curriculum> GetCurriculumsBySubject(Guid subjectId);
    Subject? GetSubjectById(Guid subjectId);
    List<Subject> GetAllSubjects();
    List<Subject> GetSubjectsByCurriculum(Guid curriculumId);
    
    // UPDATE methods
    bool UpdateCurriculum(Guid curriculumId, Curriculum curriculum);
    bool UpdateSubject(Guid subjectId, Subject subject);
    bool AddSubjectToCurriculum(Guid curriculumId, Guid subjectId);
    bool RemoveSubjectFromCurriculum(Guid curriculumId, Guid subjectId);
    
    // DELETE methods
    bool DeleteCurriculum(Guid curriculumId);
    bool DeleteSubject(Guid subjectId);
}