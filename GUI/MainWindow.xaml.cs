using Domain;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IRepository repository;
        public MainWindow()
        {
            repository = App.Services.GetRequiredService<IRepository>();
            InitializeComponent();
            LoadInitialData();
        }

        public void LoadInitialData()
        {
            DataContext = new ViewModel
            {
                Languages = Enum.GetValues(typeof(ELanguage))
                               .Cast<ELanguage>()
                               .Select(l => new KeyValuePair<ELanguage, string>(l, Helpers.GetLanguageAsText(l)))
                               .ToList(),
                StudyLevels = Enum.GetValues(typeof(EStudyLevel))
                               .Cast<EStudyLevel>()
                               .Select(l => new KeyValuePair<EStudyLevel, string>(l, Helpers.GetStudyLevelAsText(l)))
                               .ToList(),
                AssessmentForms = Enum.GetValues(typeof(EAssessmentForm))
                               .Cast<EAssessmentForm>()
                               .Select(a => new KeyValuePair<EAssessmentForm, string>(a, Helpers.GetAssessmentFormAsText(a)))
                               .ToList(),
                
                Curriculums = repository.GetAllCurriculums(),
                CurrentCurriculum = null,
                CurrentCurriculumSubjects = null,

                Subjects = repository.GetAllSubjects(),
                CurrentSubject = null,
                CurrentSubjectCurriculums = null,
            };
        }

        private void HideAllPanels()
        {
            pnlInitialView.Visibility = Visibility.Hidden;
            pnlAddCurriculumView.Visibility = Visibility.Hidden;
            pnlAddSubjectView.Visibility = Visibility.Hidden;
            pnlEditCurriculumView.Visibility = Visibility.Hidden;
            pnlEditSubjectView.Visibility = Visibility.Hidden;
        }

        private void btnÓpenAddCurriculumView_Click(object sender, RoutedEventArgs e)
        {
            HideAllPanels();
            pnlAddCurriculumView.Visibility = Visibility.Visible;
        }

        private void btnOpenAddSubjectView_Click(object sender, RoutedEventArgs e)
        {
            HideAllPanels();
            pnlAddSubjectView.Visibility = Visibility.Visible;
        }

        private void lwCurriculumsDoubleClick(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as ViewModel;
            if (vm == null)
                return;

            var selected = lwCurriculums.SelectedItem as Curriculum;
            if (selected == null)
                return;

            vm.CurrentCurriculum = selected;
            pnlEditCurriculumView.DataContext = vm;

            HideAllPanels();
            pnlEditCurriculumView.Visibility = Visibility.Visible;
        }

        private void lwSubjectsDoubleClick(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as ViewModel;
            if (vm == null)
                return;

            var selected = lwSubjects.SelectedItem as Subject;
            if (selected == null)
                return;

            vm.CurrentSubject = selected;
            pnlEditSubjectView.DataContext = vm;

            HideAllPanels();
            pnlEditSubjectView.Visibility = Visibility.Visible;
        }

        private void lnkGoHome_Click(object sender, RoutedEventArgs e)
        {
            HideAllPanels();
            pnlInitialView.Visibility = Visibility.Visible;
        }
    }
}