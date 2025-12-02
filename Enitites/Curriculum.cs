using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class Curriculum : BaseEntity
{
    public string Code { get; set; } = string.Empty;
    public EStudyLevel StudyLevel { get; set; }
    public string EtName { get; set; } = string.Empty;
    public string EnName { get; set; } = string.Empty;
    public string? ManagerName { get; set; }
    public string Language { get; set; } = "et";
    public int EapVolume { get; set; }


    [NotMapped]
    public string StudyLevelText
        => StudyLevel switch
        {
            EStudyLevel.Bachelors => "Bakalaureuseõpe",
            EStudyLevel.Masters => "Magistriõpe",
            EStudyLevel.Doctors => "Doktorantuur",
            _ => "Tundmatu"
        };
}