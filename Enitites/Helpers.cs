using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Helpers
    {
        public static string GetStudyLevelAsText(EStudyLevel level)
        {
            switch (level)
            {
                case EStudyLevel.Bachelors:
                    return "Bakalaureus";
                case EStudyLevel.Masters:
                    return "Magister";
                case EStudyLevel.Doctors:
                    return "Doktor";
                default:
                    return level.ToString();
            }
        }
        public static string GetAssessmentFormAsText(EAssessmentForm assessmentForm)
        {
            switch (assessmentForm)
            {
                case EAssessmentForm.Exam:
                    return "Eksam";
                case EAssessmentForm.Test:
                    return "Kontrolltöö";
                case EAssessmentForm.Task:
                    return "Hindeline ülesanne";
                case EAssessmentForm.Practice:
                    return "Praktiline ülesanne";
                default:
                    return assessmentForm.ToString();
            }
        }

        public static string GetLanguageAsText(ELanguage language)
        {
            switch (language)
            {
                case ELanguage.Estonian:
                    return "Eesti";
                case ELanguage.English:
                    return "Inglise";
                default:
                    return language.ToString();
            }
        }
    }
}
