namespace QuestionnaireWebApp.Services
{
    public static class ConstServices
    {
        public enum QUESTION_TYPE: ushort { Checkbox = 1, Inputtext = 2, Selectlist = 3 };
        public const string GET_QUESTIONNAIRE_QUERY = @"SELECT Questionnaire.Id, Questionnaire.Libelle,
                                    Question.Id as QuestionId,  Question.Libelle as QuestionLibelle, Question.Valeur as QuestionValeur,
                                    Question.TypeQuestionId as QuestionTypeQuestionId,Question.IsImportant as QuestionIsImportant,
                                    ListeChoix.Id as ListeChoixId, ListeChoix.Libelle as ListeChoixLibelle
                                    FROM Questionnaire
                                    INNER JOIN Question ON (Question.IdQuestionnaire = Questionnaire.Id)
                                    LEFT JOIN ListeChoix ON (ListeChoix.IdQuestion = Question.Id)
                                    WHERE Questionnaire.Id = @Id";
    }
}