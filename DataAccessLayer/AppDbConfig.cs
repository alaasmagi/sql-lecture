using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository;

public class CurriculumConfig : IEntityTypeConfiguration<Curriculum>
{
    public void Configure(EntityTypeBuilder<Curriculum> entity)
    {
        entity.ToTable("curriculums");

        entity.Property(e => e.Id).HasConversion(Helpers.GuidToLowerString()).HasColumnName("id").HasColumnType("TEXT");
        entity.Property(e => e.CreatedBy).HasColumnName("created_by");
        entity.Property(e => e.CreatedAt).HasColumnName("created_at");
        entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
        entity.Property(e => e.Code).HasColumnName("code").IsRequired();
        entity.Property(e => e.StudyLevel).HasColumnName("study_level").IsRequired();
        entity.Property(e => e.EtName).HasColumnName("name_et").IsRequired();
        entity.Property(e => e.EnName).HasColumnName("name_en").IsRequired();
        entity.Property(e => e.ManagerName).HasColumnName("manager_name");
        entity.Property(e => e.Language).HasColumnName("language").IsRequired();
        entity.Property(e => e.EapVolume).HasColumnName("eap_volume").IsRequired();
        
        entity.HasIndex(e => e.Code).IsUnique();
    }
}

public class SubjectConfig : IEntityTypeConfiguration<Subject>
{
    public void Configure(EntityTypeBuilder<Subject> entity)
    {
        entity.ToTable("subjects");

        entity.Property(e => e.Id).HasConversion(Helpers.GuidToLowerString()).HasColumnName("id").HasColumnType("TEXT");
        entity.Property(e => e.CreatedBy).HasColumnName("created_by");
        entity.Property(e => e.CreatedAt).HasColumnName("created_at");
        entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
        entity.Property(e => e.Code).HasColumnName("code").IsRequired();
        entity.Property(e => e.EtName).HasColumnName("name_et").IsRequired();
        entity.Property(e => e.EnName).HasColumnName("name_en").IsRequired();
        entity.Property(e => e.TeacherName).HasColumnName("teacher_name");
        entity.Property(e => e.EapVolume).HasColumnName("eap_volume").IsRequired();
        entity.Property(e => e.AssessmentForm).HasColumnName("assessment_form").IsRequired();
        
        entity.HasIndex(e => e.Code).IsUnique();
    }
}

public class CurriculumSubjectConfig : IEntityTypeConfiguration<CurriculumSubject>
{
    public void Configure(EntityTypeBuilder<CurriculumSubject> entity)
    {
        entity.ToTable("curriculum_subjects");

        entity.Property(e => e.Id).HasConversion(Helpers.GuidToLowerString()).HasColumnName("id").HasColumnType("TEXT");
        entity.Property(e => e.CreatedBy).HasColumnName("created_by");
        entity.Property(e => e.CreatedAt).HasColumnName("created_at");
        entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
        entity.Property(e => e.CurriculumId).HasConversion(Helpers.GuidToLowerString()).HasColumnName("curriculum_id");
        entity.Property(e => e.SubjectId).HasConversion(Helpers.GuidToLowerString()).HasColumnName("subject_id");
    }
}