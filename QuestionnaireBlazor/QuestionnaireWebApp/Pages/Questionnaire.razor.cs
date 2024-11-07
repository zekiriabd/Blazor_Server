using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Protocol;
using QuestionnaireWebApp.Models;
using System.Text.Json;

namespace QuestionnaireWebApp.Pages
{
    public partial class Questionnaire
    {
        [Parameter] public int Id { get; set; }
        [Inject] public IQuestionnaireService QuestionnaireService { get; set; }

        private QuestionnaireModel SelectedQuestionnaire { get; set; }
        private string Result { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            SelectedQuestionnaire = await QuestionnaireService.GetQuestionnaireById(Id);
        }
        private void Save()
        {
            var resultobj = SelectedQuestionnaire.Questions.Select(q => new { q.Id, q.Valeur });
            Result = JsonSerializer.Serialize(resultobj);
        }
    }
}
