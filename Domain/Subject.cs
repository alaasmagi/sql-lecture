using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class Subject : BaseEntity
{
    public string Code { get; set; } = string.Empty;
    public string EtName { get; set; } = string.Empty;
    public string EnName { get; set; } = string.Empty;
    public string? TeacherName { get; set; }
    public int EapVolume { get; set; }
    public EAssessmentForm AssessmentForm { get; set; } = EAssessmentForm.Exam;

    [NotMapped]
    public string AssessmentFormText
        => Helpers.GetAssessmentFormAsText(AssessmentForm);
}