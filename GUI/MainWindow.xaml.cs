using Domain;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
                CurrentCurriculumAvailableSubjects = null,

                Subjects = repository.GetAllSubjects(),
                CurrentSubject = null,
                CurrentSubjectCurriculums = null,
                CurrentSubjectAvailableCurriculums = null
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
            var vm = DataContext as ViewModel;
            if (vm == null)
                return;

            vm.CurrentCurriculum = new Curriculum();
            pnlAddCurriculumView.DataContext = vm;

            HideAllPanels();
            pnlAddCurriculumView.Visibility = Visibility.Visible;
        }

        private void btnOpenAddSubjectView_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as ViewModel;
            if (vm == null)
                return;

            vm.CurrentSubject = new Subject();
            pnlAddCurriculumView.DataContext = vm;

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

            vm.CurrentCurriculumSubjects =
                new ObservableCollection<Subject>(
                    repository.GetSubjectsByCurriculum(selected.Id)
                );

            vm.CurrentCurriculumAvailableSubjects =
                new ObservableCollection<Subject>(
                    repository.GetAllSubjects()
                              .Where(s => !vm.CurrentCurriculumSubjects.Any(cs => cs.Id == s.Id))
                );

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

            vm.CurrentSubjectCurriculums =
                new ObservableCollection<Curriculum>(
                    repository.GetCurriculumsBySubject(selected.Id)
                );

            vm.CurrentSubjectAvailableCurriculums =
                new ObservableCollection<Curriculum>(
                    repository.GetAllCurriculums()
                              .Where(c => !vm.CurrentSubjectCurriculums.Any(sc => sc.Id == c.Id))
                );

            pnlEditSubjectView.DataContext = vm;

            HideAllPanels();
            pnlEditSubjectView.Visibility = Visibility.Visible;
        }

        private void btnAddCurriculum_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as ViewModel;
            if (vm == null) return;

            var newCurriculum = vm.CurrentCurriculum;

            repository.CreateCurriculum(newCurriculum);

            LoadInitialData();
            HideAllPanels();
            pnlInitialView.Visibility = Visibility.Visible;
        }

        private void btnEditCurriculum_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as ViewModel;
            if (vm == null) return;

            var curriculumToEdit = vm.CurrentCurriculum;

            repository.UpdateCurriculum(curriculumToEdit.Id, curriculumToEdit);

            LoadInitialData();
            HideAllPanels();
            pnlInitialView.Visibility = Visibility.Visible;
        }

        private void btnDeleteCurriculum_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as ViewModel;
            if (vm == null) return;

            var curriculumToDelete = vm.CurrentCurriculum;

            repository.DeleteCurriculum(curriculumToDelete.Id);

            LoadInitialData();
            HideAllPanels();
            pnlInitialView.Visibility = Visibility.Visible;
        }

        private void btnAddSubject_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as ViewModel;
            if (vm == null) return;

            var newSubject = vm.CurrentSubject;

            repository.CreateSubject(newSubject);

            LoadInitialData();
            HideAllPanels();
            pnlInitialView.Visibility = Visibility.Visible;
        }

        private void btnEditSubject_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as ViewModel;
            if (vm == null) return;

            var subjectToEdit = vm.CurrentSubject;

            repository.UpdateSubject(subjectToEdit.Id, subjectToEdit);

            LoadInitialData();
            HideAllPanels();
            pnlInitialView.Visibility = Visibility.Visible;
        }
        private void btnDeleteSubject_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as ViewModel;
            if (vm == null) return;

            var subjectToDelete = vm.CurrentSubject;

            repository.DeleteSubject(subjectToDelete.Id);

            LoadInitialData();
            HideAllPanels();
            pnlInitialView.Visibility = Visibility.Visible;
        }

        private void btnAddSubjectToCurriculum_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as ViewModel;
            if (vm == null) return;

            var currentCurriculum = vm.CurrentCurriculum;
            var subjectToAdd = lwAvailableSubjects.SelectedItem as Subject;

            if (subjectToAdd == null)
            {
                MessageBox.Show("Palun vali õppeaine, mida lisada.");
                return;
            }

            repository.AddSubjectToCurriculum(currentCurriculum.Id, subjectToAdd.Id);

            vm.CurrentCurriculumSubjects =
               new ObservableCollection<Subject>(
                   repository.GetSubjectsByCurriculum(currentCurriculum.Id)
               );

            vm.CurrentCurriculumAvailableSubjects =
                new ObservableCollection<Subject>(
                    repository.GetAllSubjects()
                              .Where(s => !vm.CurrentCurriculumSubjects.Any(cs => cs.Id == s.Id))
                );

            pnlEditCurriculumView.DataContext = vm;
        }

        private void btnRemoveSubjectFromCurriculum_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as ViewModel;
            if (vm == null) return;

            var currentCurriculum = vm.CurrentCurriculum;
            var subjectToRemove = lwCurrentCurriculumSubjects.SelectedItem as Subject;

            if (subjectToRemove == null)
            {
                MessageBox.Show("Palun vali õppeaine, mida lisada.");
                return;
            }

            repository.RemoveSubjectFromCurriculum(currentCurriculum.Id, subjectToRemove.Id);

            vm.CurrentCurriculumSubjects =
               new ObservableCollection<Subject>(
                   repository.GetSubjectsByCurriculum(currentCurriculum.Id)
               );

            vm.CurrentCurriculumAvailableSubjects =
                new ObservableCollection<Subject>(
                    repository.GetAllSubjects()
                              .Where(s => !vm.CurrentCurriculumSubjects.Any(cs => cs.Id == s.Id))
                );

            pnlEditCurriculumView.DataContext = vm;
        }

        private void lnkGoHome_Click(object sender, RoutedEventArgs e)
        {
            HideAllPanels();
            pnlInitialView.Visibility = Visibility.Visible;
        }
    }
}