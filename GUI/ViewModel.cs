using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private Curriculum _currentCurriculum;
        public Curriculum CurrentCurriculum
        {
            get => _currentCurriculum;
            set
            {
                if (_currentCurriculum != value)
                {
                    _currentCurriculum = value;
                    OnPropertyChanged(nameof(CurrentCurriculum));
                }
            }
        }

        private Subject _currentSubject;
        public Subject CurrentSubject
        {
            get => _currentSubject;
            set
            {
                if (_currentSubject != value)
                {
                    _currentSubject = value;
                    OnPropertyChanged(nameof(CurrentSubject));
                }
            }
        }

        public List<KeyValuePair<ELanguage, string>> Languages { get; set; }
        public List<KeyValuePair<EStudyLevel, string>> StudyLevels { get; set; }
        public List<KeyValuePair<EAssessmentForm, string>> AssessmentForms { get; set; }
        public List<Curriculum> Curriculums { get; set; }
        public List<Subject> Subjects { get; set; }
        public List<Subject>? CurrentCurriculumSubjects { get; set; }
        public List<Curriculum>? CurrentSubjectCurriculums { get; set; }
    }
}
