namespace Domain;

public class CurriculumSubject : BaseEntity
{
    public Guid CurriculumId { get; set; }
    public Guid SubjectId { get; set; }
}