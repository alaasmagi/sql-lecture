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
            // TODO: Inject repository through constructor once DI is set up
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

                // TODO: Use GetAllCurriculums() to populate Curriculums
                Curriculums = repository.GetAllCurriculums(),
                CurrentCurriculum = null,
                CurrentCurriculumSubjects = null,
                CurrentCurriculumAvailableSubjects = null,

                // TODO: Use GetAllSubjects() to populate Subjects
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

            // TODO: Use GetSubjectsByCurriculum() to populate CurrentCurriculumSubjects and then uncomment the code block below
            vm.CurrentCurriculumSubjects =
                new ObservableCollection<Subject>(
                    repository.GetSubjectsByCurriculum(selected.Id)
                );

            // TODO: Use GetAllSubjects() to populate CurrentCurriculumAvailableSubjects, use LINQ to filter out subjects already in CurrentCurriculumSubjects, and then uncomment the code block below
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

            // TODO: Use GetCurriculumsBySubject() to populate CurrentSubjectCurriculums and then uncomment the code block below
            vm.CurrentSubjectCurriculums =
                new ObservableCollection<Curriculum>(
                    repository.GetCurriculumsBySubject(selected.Id)
                );

            // TODO: Use GetAllCurriculums() to populate CurrentSubjectAvailableCurriculums, use LINQ to filter out subjects already in CurrentSubjectCurriculums, and then uncomment the code block below
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

            // TODO: Use CreateCurriculum() to add the new curriculum and then uncomment the code block below
            var status = repository.CreateCurriculum(newCurriculum);
            if (status == false)
            {
                MessageBox.Show("Õppekava lisamine ebaõnnestus. Kontrolli õppekava koodi unikaalsust ja õppekava mahtu!", "Tõrge", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            LoadInitialData();
            HideAllPanels();
            pnlInitialView.Visibility = Visibility.Visible;
        }

        private void btnEditCurriculum_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as ViewModel;
            if (vm == null) return;

            var curriculumToEdit = vm.CurrentCurriculum;

            // TODO: Use UpdateCurriculum() to update the curriculum and then uncomment the code block below
            var status = repository.UpdateCurriculum(curriculumToEdit.Id, curriculumToEdit);
            if (status == false)
            {
                MessageBox.Show("Õppekava muutmine ebaõnnestus. Kontrolli õppekava koodi unikaalsust ja õppekava mahtu!", "Tõrge", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

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

            // TODO: Use CreateSubject() to add the new subject and then uncomment the code block below
            var status = repository.CreateSubject(newSubject);
            if (status == false)
            {
                MessageBox.Show("Õppeaine lisamine ebaõnnestus. Kontrolli Õppeaine koodi unikaalsust ja Õppeaine mahtu!", "Tõrge", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            LoadInitialData();
            HideAllPanels();
            pnlInitialView.Visibility = Visibility.Visible;
        }

        private void btnEditSubject_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as ViewModel;
            if (vm == null) return;

            var subjectToEdit = vm.CurrentSubject;

            // TODO: Use UpdateSubject() to update the subject and then uncomment the code block below
            var status = repository.UpdateSubject(subjectToEdit.Id, subjectToEdit);
            if (status == false)
            {
                MessageBox.Show("Õppeaine muutmine ebaõnnestus. Kontrolli Õppeaine koodi unikaalsust ja Õppeaine mahtu!", "Tõrge", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            LoadInitialData();
            HideAllPanels();
            pnlInitialView.Visibility = Visibility.Visible;
        }

        private void btnDeleteSubject_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as ViewModel;
            if (vm == null) return;

            var subjectToDelete = vm.CurrentSubject;

            // TODO: Use DeleteSubject() to delete the subject and then uncomment the code block below
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
                MessageBox.Show("Palun vali õppeaine, mida lisada.", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var curriculumSubject = new CurriculumSubject
            {
                CurriculumId = currentCurriculum.Id,
                SubjectId = subjectToAdd.Id
            };

            repository.AddSubjectToCurriculum(curriculumSubject);

            // TODO: Use GetSubjectsByCurriculum() to update CurrentCurriculumSubjects and then uncomment the code block below
            vm.CurrentCurriculumSubjects =
               new ObservableCollection<Subject>(
                   repository.GetSubjectsByCurriculum(currentCurriculum.Id)
               );

            // TODO: Use GetAllSubjects() to update CurrentCurriculumAvailableSubjects, use LINQ to filter out subjects already in CurrentCurriculumSubjects, and then uncomment the code block below
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
                MessageBox.Show("Palun vali õppeaine, mida lisada.", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            repository.RemoveSubjectFromCurriculum(currentCurriculum.Id, subjectToRemove.Id);

            // TODO: Use GetSubjectsByCurriculum() to update CurrentCurriculumSubjects and then uncomment the code block below
            vm.CurrentCurriculumSubjects =
               new ObservableCollection<Subject>(
                   repository.GetSubjectsByCurriculum(currentCurriculum.Id)
               );

            // TODO: Use GetAllSubjects() to update CurrentCurriculumAvailableSubjects, use LINQ to filter out subjects already in CurrentCurriculumSubjects, and then uncomment the code block 
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