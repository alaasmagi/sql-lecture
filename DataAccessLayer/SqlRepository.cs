using System.Data;
using Domain;
using Microsoft.Data.Sqlite;

namespace Repository;

public class SqlRepository(string connectionString) : IRepository
{
    private SqliteConnection GetConnection()
        => new SqliteConnection(connectionString);
    
    public bool CreateCurriculum(Curriculum curriculum)
    {
        const string sql = @"
            INSERT INTO curriculums
            (id, code, study_level, name_et, name_en, manager_name, language,
             eap_volume, created_by, created_at, updated_by, updated_at)
            VALUES
            ($id, $code, $sl, $et, $en, $mgr, $lang, $eap, $cby, $cat, $uby, $uat);
        ";

        using var conn = GetConnection();
        using var cmd = new SqliteCommand(sql, conn);

        cmd.Parameters.AddWithValue("$id", curriculum.Id.ToString());
        cmd.Parameters.AddWithValue("$code", curriculum.Code);
        cmd.Parameters.AddWithValue("$sl", curriculum.StudyLevel);
        cmd.Parameters.AddWithValue("$et", curriculum.EtName);
        cmd.Parameters.AddWithValue("$en", curriculum.EnName);
        cmd.Parameters.AddWithValue("$mgr", curriculum.ManagerName ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("$lang", curriculum.Language);
        cmd.Parameters.AddWithValue("$eap", curriculum.EapVolume);
        cmd.Parameters.AddWithValue("$cby", curriculum.CreatedBy);
        cmd.Parameters.AddWithValue("$cat", curriculum.CreatedAt);
        cmd.Parameters.AddWithValue("$uby", curriculum.UpdatedBy);
        cmd.Parameters.AddWithValue("$uat", curriculum.UpdatedAt);

        conn.Open();
        return cmd.ExecuteNonQuery() > 0;
    }

    public bool CreateSubject(Subject subject)
    {
        const string sql = @"
            INSERT INTO subjects
            (id, code, name_et, name_en, teacher_name, eap_volume, assessment_form,
             created_by, created_at, updated_by, updated_at)
            VALUES
            ($id, $code, $et, $en, $teacher, $eap, $form, $cby, $cat, $uby, $uat);
        ";

        using var conn = GetConnection();
        using var cmd = new SqliteCommand(sql, conn);

        cmd.Parameters.AddWithValue("$id", subject.Id.ToString());
        cmd.Parameters.AddWithValue("$code", subject.Code);
        cmd.Parameters.AddWithValue("$et", subject.EtName);
        cmd.Parameters.AddWithValue("$en", subject.EnName);
        cmd.Parameters.AddWithValue("$teacher", subject.TeacherName ?? null);
        cmd.Parameters.AddWithValue("$eap", subject.EapVolume);
        cmd.Parameters.AddWithValue("$form", subject.AssessmentForm);
        cmd.Parameters.AddWithValue("$cby", subject.CreatedBy);
        cmd.Parameters.AddWithValue("$cat", subject.CreatedAt);
        cmd.Parameters.AddWithValue("$uby", subject.UpdatedBy);
        cmd.Parameters.AddWithValue("$uat", subject.UpdatedAt);

        conn.Open();
        return cmd.ExecuteNonQuery() > 0;
    }

    public Curriculum? GetCurriculumById(Guid curriculumId)
    {
        const string sql = "SELECT * FROM curriculums WHERE id = $id";

        using var conn = GetConnection();
        using var cmd = new SqliteCommand(sql, conn);

        cmd.Parameters.AddWithValue("$id", curriculumId);

        conn.Open();
        using var reader = cmd.ExecuteReader();
        return reader.Read() ? MapCurriculum(reader) : null;
    }

    public List<Curriculum> GetAllCurriculums()
    {
        const string sql = "SELECT * FROM curriculums";

        using var conn = GetConnection();
        using var cmd = new SqliteCommand(sql, conn);

        conn.Open();
        using var reader = cmd.ExecuteReader();

        var list = new List<Curriculum>();
        while (reader.Read())
            list.Add(MapCurriculum(reader));

        return list;
    }

    public List<Curriculum> GetCurriculumsBySubject(Guid subjectId)
    {
        const string sql = @"
            SELECT c.*
            FROM curriculums c
            JOIN curriculum_subjects cs ON cs.curriculum_id = c.id
            WHERE cs.subject_id = $sid;
        ";

        using var conn = GetConnection();
        using var cmd = new SqliteCommand(sql, conn);

        cmd.Parameters.AddWithValue("$sid", subjectId.ToString());

        conn.Open();
        using var reader = cmd.ExecuteReader();

        var list = new List<Curriculum>();
        while (reader.Read())
            list.Add(MapCurriculum(reader));

        return list;
    }

    public Subject? GetSubjectById(Guid subjectId)
    {
        const string sql = "SELECT * FROM subjects WHERE id = $id";

        using var conn = GetConnection();
        using var cmd = new SqliteCommand(sql, conn);

        cmd.Parameters.AddWithValue("$id", subjectId);

        conn.Open();
        using var reader = cmd.ExecuteReader();
        return reader.Read() ? MapSubject(reader) : null;
    }

    public List<Subject> GetAllSubjects()
    {
        const string sql = "SELECT * FROM subjects";

        using var conn = GetConnection();
        using var cmd = new SqliteCommand(sql, conn);

        conn.Open();
        using var reader = cmd.ExecuteReader();

        var list = new List<Subject>();
        while (reader.Read())
            list.Add(MapSubject(reader));

        return list;
    }

    public List<Subject> GetSubjectsByCurriculum(Guid curriculumId)
    {
        const string sql = @"
            SELECT s.*
            FROM subjects s
            JOIN curriculum_subjects cs ON cs.subject_id = s.id
            WHERE cs.curriculum_id = $cid;
        ";

        using var conn = GetConnection();
        using var cmd = new SqliteCommand(sql, conn);

        cmd.Parameters.AddWithValue("$cid", curriculumId.ToString());

        conn.Open();
        using var reader = cmd.ExecuteReader();

        var list = new List<Subject>();
        while (reader.Read())
            list.Add(MapSubject(reader));

        return list;
    }
    
    public bool UpdateCurriculum(Guid curriculumId, Curriculum c)
    {
        const string sql = @"
            UPDATE curriculums
            SET code=$code, study_level=$sl, name_et=$et, name_en=$en,
                manager_name=$mgr, language=$lang, eap_volume=$eap,
                updated_by=$uby, updated_at=$uat
            WHERE id=$id;
        ";

        using var conn = GetConnection();
        using var cmd = new SqliteCommand(sql, conn);

        cmd.Parameters.AddWithValue("$id", curriculumId.ToString());
        cmd.Parameters.AddWithValue("$code", c.Code);
        cmd.Parameters.AddWithValue("$sl", (int)c.StudyLevel);
        cmd.Parameters.AddWithValue("$et", c.EtName);
        cmd.Parameters.AddWithValue("$en", c.EnName);
        cmd.Parameters.AddWithValue("$mgr", c.ManagerName ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("$lang", c.Language);
        cmd.Parameters.AddWithValue("$eap", c.EapVolume);
        cmd.Parameters.AddWithValue("$uby", c.UpdatedBy);
        cmd.Parameters.AddWithValue("$uat", c.UpdatedAt);

        conn.Open();
        return cmd.ExecuteNonQuery() > 0;
    }

    public bool UpdateSubject(Guid subjectId, Subject s)
    {
        const string sql = @"
            UPDATE subjects
            SET code=$code, name_et=$et, name_en=$en, teacher_name=$teacher,
                eap_volume=$eap, assessment_form=$form,
                updated_by=$uby, updated_at=$uat
            WHERE id=$id;
        ";

        using var conn = GetConnection();
        using var cmd = new SqliteCommand(sql, conn);

        cmd.Parameters.AddWithValue("$id", subjectId.ToString());
        cmd.Parameters.AddWithValue("$code", s.Code);
        cmd.Parameters.AddWithValue("$et", s.EtName);
        cmd.Parameters.AddWithValue("$en", s.EnName);
        cmd.Parameters.AddWithValue("$teacher", s.TeacherName ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("$eap", s.EapVolume);
        cmd.Parameters.AddWithValue("$form", (int)s.AssessmentForm);
        cmd.Parameters.AddWithValue("$uby", s.UpdatedBy);
        cmd.Parameters.AddWithValue("$uat", s.UpdatedAt);

        conn.Open();
        return cmd.ExecuteNonQuery() > 0;
    }

    public bool AddSubjectToCurriculum(Guid curriculumId, Guid subjectId)
    {
        const string sql = @"
            INSERT INTO curriculum_subjects
            (id, curriculum_id, subject_id, created_by, created_at, updated_by, updated_at)
            VALUES ($id, $cid, $sid, 'sys', CURRENT_TIMESTAMP, 'sys', CURRENT_TIMESTAMP)
        ";

        using var conn = GetConnection();
        using var cmd = new SqliteCommand(sql, conn);

        cmd.Parameters.AddWithValue("$id", Guid.NewGuid().ToString());
        cmd.Parameters.AddWithValue("$cid", curriculumId.ToString());
        cmd.Parameters.AddWithValue("$sid", subjectId.ToString());

        conn.Open();
        return cmd.ExecuteNonQuery() > 0;
    }

    public bool RemoveSubjectFromCurriculum(Guid curriculumId, Guid subjectId)
    {
        const string sql = @"
            DELETE FROM curriculum_subjects
            WHERE curriculum_id=$cid AND subject_id=$sid;
        ";

        using var conn = GetConnection();
        using var cmd = new SqliteCommand(sql, conn);

        cmd.Parameters.AddWithValue("$cid", curriculumId.ToString());
        cmd.Parameters.AddWithValue("$sid", subjectId.ToString());

        conn.Open();
        return cmd.ExecuteNonQuery() > 0;
    }
    
    public bool DeleteCurriculum(Guid id)
    {
        const string sql = "DELETE FROM curriculums WHERE id=$id";

        using var conn = GetConnection();
        using var cmd = new SqliteCommand(sql, conn);
        cmd.Parameters.AddWithValue("$id", id.ToString());

        conn.Open();
        return cmd.ExecuteNonQuery() > 0;
    }

    public bool DeleteSubject(Guid id)
    {
        const string sql = "DELETE FROM subjects WHERE id=$id";

        using var conn = GetConnection();
        using var cmd = new SqliteCommand(sql, conn);
        cmd.Parameters.AddWithValue("$id", id.ToString());

        conn.Open();
        return cmd.ExecuteNonQuery() > 0;
    }
    
    
    private Curriculum MapCurriculum(IDataRecord r)
    {
        return new Curriculum
        {
            Id = Guid.Parse(r.GetString(r.GetOrdinal("id"))),

            Code = r.GetString(r.GetOrdinal("code")),
            StudyLevel = (EStudyLevel)r.GetInt32(r.GetOrdinal("study_level")),
            EtName = r.GetString(r.GetOrdinal("name_et")),
            EnName = r.GetString(r.GetOrdinal("name_en")),

            ManagerName = r.IsDBNull(r.GetOrdinal("manager_name"))
                ? null
                : r.GetString(r.GetOrdinal("manager_name")),

            Language = r.GetString(r.GetOrdinal("language")),
            EapVolume = r.GetInt32(r.GetOrdinal("eap_volume")),

            CreatedBy = r.GetString(r.GetOrdinal("created_by")),
            CreatedAt = DateTime.Parse(r.GetString(r.GetOrdinal("created_at"))),

            UpdatedBy = r.GetString(r.GetOrdinal("updated_by")),
            UpdatedAt = DateTime.Parse(r.GetString(r.GetOrdinal("updated_at")))
        };
    }

    private Subject MapSubject(IDataRecord r)
    {
        return new Subject
        {
            Id = Guid.Parse(r.GetString(r.GetOrdinal("id"))),

            Code = r.GetString(r.GetOrdinal("code")),
            EtName = r.GetString(r.GetOrdinal("name_et")),
            EnName = r.GetString(r.GetOrdinal("name_en")),

            TeacherName = r.IsDBNull(r.GetOrdinal("teacher_name"))
                ? null
                : r.GetString(r.GetOrdinal("teacher_name")),

            EapVolume = r.GetInt32(r.GetOrdinal("eap_volume")),
            AssessmentForm = (EAssessmentForm)r.GetInt32(r.GetOrdinal("assessment_form")),

            CreatedBy = r.GetString(r.GetOrdinal("created_by")),
            CreatedAt = DateTime.Parse(r.GetString(r.GetOrdinal("created_at"))),

            UpdatedBy = r.GetString(r.GetOrdinal("updated_by")),
            UpdatedAt = DateTime.Parse(r.GetString(r.GetOrdinal("updated_at")))
        };
    }
}