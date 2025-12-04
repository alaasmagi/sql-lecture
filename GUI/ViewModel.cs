using Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public List<KeyValuePair<ELanguage, string>> Languages { get; set; }
        public List<KeyValuePair<EStudyLevel, string>> StudyLevels { get; set; }
        public List<KeyValuePair<EAssessmentForm, string>> AssessmentForms { get; set; }
        public List<Curriculum> Curriculums { get; set; }
        public List<Subject> Subjects { get; set; }

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

        private ObservableCollection<Subject>? _currentCurriculumSubjects;
        public ObservableCollection<Subject>? CurrentCurriculumSubjects
        {
            get => _currentCurriculumSubjects;
            set { _currentCurriculumSubjects = value; OnPropertyChanged(nameof(CurrentCurriculumSubjects)); }
        }

        private ObservableCollection<Subject>? _currentCurriculumAvailableSubjects;
        public ObservableCollection<Subject>? CurrentCurriculumAvailableSubjects
        {
            get => _currentCurriculumAvailableSubjects;
            set { _currentCurriculumAvailableSubjects = value; OnPropertyChanged(nameof(CurrentCurriculumAvailableSubjects)); }
        }

        private ObservableCollection<Curriculum>? _currentSubjectCurriculums;
        public ObservableCollection<Curriculum>? CurrentSubjectCurriculums
        {
            get => _currentSubjectCurriculums;
            set { _currentSubjectCurriculums = value; OnPropertyChanged(nameof(CurrentSubjectCurriculums)); }
        }

        private ObservableCollection<Curriculum>? _currentSubjectAvailableCurriculums;
        public ObservableCollection<Curriculum>? CurrentSubjectAvailableCurriculums
        {
            get => _currentSubjectAvailableCurriculums;
            set { _currentSubjectAvailableCurriculums = value; OnPropertyChanged(nameof(CurrentSubjectAvailableCurriculums)); }
        }
    }
}
