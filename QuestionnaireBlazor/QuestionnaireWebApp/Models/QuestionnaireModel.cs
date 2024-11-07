
using QuestionnaireWebApp.Pages;
using System.ComponentModel;

namespace QuestionnaireWebApp.Models
{
    public class QuestionnaireModel
    {
        public int Id { get; set; }
        public string Libelle { get; set; }
        public List<QuestionModel> Questions { get; set; } = [];
    }

    public class QuestionnaireMapping
    {
        public int QuestionnaireId { get; set; }
        public string QuestionnaireLibelle { get; set; }
        public int QuestionId { get; set; }
        public string QuestionLibelle { get; set; }
        public int QuestionTypeQuestionId { get; set; }
        public string QuestionValeur { get; set; }
        public bool QuestionIsImportant { get; set; }
        public int ListeChoixId { get; set; }
        public string ListeChoixLibelle { get; set; }
        

    }
}
