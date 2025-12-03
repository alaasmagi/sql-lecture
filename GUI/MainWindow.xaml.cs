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
            LoadAllData();
        }

        private void LoadAllData()
        {
            DataContext = new ViewModel
            {
                Languages = Enum.GetValues(typeof(ELanguage))
                                .Cast<ELanguage>()
                                .Select(l => new KeyValuePair<ELanguage, string>(l, Helpers.GetLanguageAsText(l)))
                                .ToList(),
                SelectedLanguage = ELanguage.Estonian,
                StudyLevels = Enum.GetValues(typeof(EStudyLevel))
                                .Cast<EStudyLevel>()
                                .Select(l => new KeyValuePair<EStudyLevel, string>(l, Helpers.GetStudyLevelAsText(l)))
                                .ToList(),
                SelectedStudyLevel = EStudyLevel.Bachelors,
                Curriculums = repository.GetAllCurriculums(),
                Subjects = repository.GetAllSubjects()
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
            HideAllPanels();
            pnlEditCurriculumView.Visibility = Visibility.Visible;
        }

        private void lwSubjectsDoubleClick(object sender, RoutedEventArgs e)
        {
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