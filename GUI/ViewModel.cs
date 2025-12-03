using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    public class ViewModel
    {
        public List<KeyValuePair<ELanguage, string>> Languages { get; set; }
        public ELanguage SelectedLanguage { get; set; }
        public List<KeyValuePair<EStudyLevel, string>> StudyLevels { get; set; }
        public EStudyLevel SelectedStudyLevel { get; set; }
        public List<Curriculum> Curriculums { get; set; }
        public List<Subject> Subjects { get; set; }
    }
}
