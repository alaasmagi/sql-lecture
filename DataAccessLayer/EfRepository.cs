using Domain;

namespace Repository;
public class EfRepository(AppDbContext db) : IRepository
{
    public bool CreateCurriculum(Curriculum curriculum)
    {
        db.Curriculums.Add(curriculum);
        return db.SaveChanges() > 0;
    }

    public bool CreateSubject(Subject subject)
    {
        db.Subjects.Add(subject);
        return db.SaveChanges() > 0;
    }

    public Curriculum? GetCurriculumByName(string curriculumName)
    {
        return db.Curriculums
            .FirstOrDefault(c => c.EnName == curriculumName || c.EtName == curriculumName);
    }

    public List<Curriculum> GetAllCurriculums()
    {
        return db.Curriculums.ToList();
    }

    public List<Curriculum> GetCurriculumsBySubject(Guid subjectId)
    {
        var curriculumIds = db.CurriculumSubjects
            .Where(cs => cs.SubjectId == subjectId)
            .Select(cs => cs.CurriculumId);

        return db.Curriculums
            .Where(c => curriculumIds.Contains(c.Id))
            .ToList();
    }

    public Subject? GetSubjectByName(string subjectName)
    {
        return db.Subjects
            .FirstOrDefault(s => s.EnName == subjectName || s.EtName == subjectName);
    }

    public List<Subject> GetAllSubjects()
    {
        return db.Subjects.ToList();
    }

    public List<Subject> GetSubjectsByCurriculum(Guid curriculumId)
    {
        var subjectIds = db.CurriculumSubjects
            .Where(cs => cs.CurriculumId == curriculumId)
            .Select(cs => cs.SubjectId);

        return db.Subjects
            .Where(c => subjectIds.Contains(c.Id))
            .ToList();
    }

    public bool UpdateCurriculum(Guid curriculumId, Curriculum curriculum)
    {
        var existing = db.Curriculums.FirstOrDefault(c => c.Id == curriculumId);
        if (existing == null) return false;

        db.Entry(existing).CurrentValues.SetValues(curriculum);
        return db.SaveChanges() > 0;
    }

    public bool UpdateSubject(Guid subjectId, Subject subject)
    {
        var existing = db.Subjects.FirstOrDefault(s => s.Id == subjectId);
        if (existing == null) return false;

        db.Entry(existing).CurrentValues.SetValues(subject);
        return db.SaveChanges() > 0;
    }

    public bool AddSubjectToCurriculum(Guid curriculumId, Guid subjectId)
    {
        bool exists = db.CurriculumSubjects.Any(cs =>
            cs.CurriculumId == curriculumId &&
            cs.SubjectId == subjectId);

        if (exists) return false;

        var cs = new CurriculumSubject
        {
            Id = Guid.NewGuid(),
            CurriculumId = curriculumId,
            SubjectId = subjectId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            CreatedBy = "system",
            UpdatedBy = "system"
        };

        db.CurriculumSubjects.Add(cs);
        return db.SaveChanges() > 0;
    }

    public bool RemoveSubjectFromCurriculum(Guid curriculumId, Guid subjectId)
    {
        var cs = db.CurriculumSubjects.FirstOrDefault(cs =>
            cs.CurriculumId == curriculumId &&
            cs.SubjectId == subjectId);

        if (cs == null) return false;

        db.CurriculumSubjects.Remove(cs);
        return db.SaveChanges() > 0;
    }
    

    public bool DeleteCurriculum(Guid curriculumId)
    {
        var curriculum = db.Curriculums.FirstOrDefault(c => c.Id == curriculumId);
        if (curriculum == null) return false;

        db.Curriculums.Remove(curriculum);
        return db.SaveChanges() > 0;
    }

    public bool DeleteSubject(Guid subjectId)
    {
        var subject = db.Subjects.FirstOrDefault(s => s.Id == subjectId);
        if (subject == null) return false;

        db.Subjects.Remove(subject);
        return db.SaveChanges() > 0;
    }
}