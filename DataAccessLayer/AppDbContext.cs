using Domain;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    public DbSet<Curriculum> Curriculums { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<CurriculumSubject> CurriculumSubjects { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Initial configurations
        modelBuilder.ApplyConfiguration(new CurriculumConfig());
        modelBuilder.ApplyConfiguration(new SubjectConfig());
        modelBuilder.ApplyConfiguration(new CurriculumSubjectConfig());

        // Relationship configurations
        modelBuilder.Entity<CurriculumSubject>()
            .HasOne<Curriculum>()
            .WithMany()
            .HasForeignKey(s => s.CurriculumId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<CurriculumSubject>()
            .HasOne<Subject>()
            .WithMany()
            .HasForeignKey(s => s.SubjectId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}