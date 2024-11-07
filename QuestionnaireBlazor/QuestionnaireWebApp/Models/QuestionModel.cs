namespace QuestionnaireWebApp.Models
{
    public class QuestionModel
    {
        public int Id { get; set; }
        public int IdQuestionnaire { get; set; }
        public string Libelle { get; set; }
        public int TypeQuestionId { get; set; }
        public List<ListeChoixModel> ListeChoix { get; set; } = [];
        public string Valeur { get; set; }
        public bool IsImportant { get; set; }
    }
}
