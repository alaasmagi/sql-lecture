using Domain;

namespace Repository;

public class SqlRepository : IRepository
{
    public bool CreateCurriculum(Curriculum curriculum)
    {
        throw new NotImplementedException();
    }

    public bool CreateSubject(Subject subject)
    {
        throw new NotImplementedException();
    }

    public Curriculum GetCurriculumByName(string curriculumName)
    {
        throw new NotImplementedException();
    }

    public List<Curriculum> GetAllCurriculums()
    {
        throw new NotImplementedException();
    }

    public List<Curriculum> GetCurriculumsBySubject(Guid subjectId)
    {
        throw new NotImplementedException();
    }

    public Subject GetSubjectByName(string subjectName)
    {
        throw new NotImplementedException();
    }

    public List<Curriculum> GetAllSubjects()
    {
        throw new NotImplementedException();
    }

    public List<Curriculum> GetSubjectsByCurriculum(Guid curriculumId)
    {
        throw new NotImplementedException();
    }

    public bool UpdateCurriculum(Guid curriculumId, Curriculum curriculum)
    {
        throw new NotImplementedException();
    }

    public bool UpdateSubject(Guid subjectId, Subject subject)
    {
        throw new NotImplementedException();
    }

    public bool AddSubjectToCurriculum(Guid curriculumId, Guid subjectId)
    {
        throw new NotImplementedException();
    }

    public bool RemoveSubjectFromCurriculum(Guid curriculumId, Guid subjectId)
    {
        throw new NotImplementedException();
    }

    public bool DeleteCurriculum(Guid curriculumId)
    {
        throw new NotImplementedException();
    }

    public bool DeleteSubject(Guid subjectId)
    {
        throw new NotImplementedException();
    }
}