using Dapper;
using QuestionnaireWebApp.Models;
using QuestionnaireWebApp.Pages;
using System.Data;
using System.Data.SQLite;
using System.Text.Json;

namespace QuestionnaireWebApp.Services
{
    public interface IQuestionnaireService
    {
        Task<QuestionnaireModel> GetQuestionnaireById(int Id);
    }

    public class QuestionnaireSQLiteService : IQuestionnaireService
    {
        private readonly IConfiguration Configuration;
        private readonly ILogger<ErrorModel> Logger;
        private string ConnectionString { get; }
        public QuestionnaireSQLiteService(IConfiguration configuration , ILogger<ErrorModel> logger)
        {
            Configuration = configuration;
            Logger = logger;
            ConnectionString = $"Data Source={Environment.CurrentDirectory}{Configuration.GetSection("SQLiteConnection").Value}";
        }

        public async Task<QuestionnaireModel> GetQuestionnaireById(int Id)
        {
            QuestionnaireModel result = null;
            using (var conn = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    if (conn.State == ConnectionState.Closed) await conn.OpenAsync();

                    var questionnaire = await conn.QueryAsync< QuestionnaireMapping>(GET_QUESTIONNAIRE_QUERY, new { Id });
                    if (questionnaire.Any())
                    {
                        var questions = new List<QuestionModel>();
                        foreach (var q in questionnaire.DistinctBy(x => x.QuestionId))
                        {
                            var choix = new List<ListeChoixModel>();
                            var listChoix = questionnaire.Where(x => x.QuestionnaireId == q.QuestionnaireId).ToList();
                            foreach (var ch in listChoix)
                            {
                                choix.Add(new ListeChoixModel() { Id = ch.ListeChoixId,Libelle = ch.ListeChoixLibelle });
                            }
                            var question = new QuestionModel() {
                                Id=q.QuestionId,
                                Libelle=q.QuestionLibelle,
                                Valeur=q.QuestionValeur,
                                IsImportant = q.QuestionIsImportant,
                                TypeQuestionId=q.QuestionTypeQuestionId,
                                IdQuestionnaire=q.QuestionnaireId,ListeChoix=choix 
                            };
                            questions.Add(question);
                        }
                        result = new QuestionnaireModel() { 
                            Id = questionnaire.First().QuestionnaireId, 
                            Libelle = questionnaire.First().QuestionLibelle,
                            Questions= questions 
                        };
                    }

                }
                catch (Exception ex)
                {
                    Logger.LogError(ex.ToString());
                }
                finally
                {
                    if (conn.State == ConnectionState.Open) await conn.CloseAsync();
                }
                return result;
            }
        }
    }

    public class QuestionnaireJsonService : IQuestionnaireService
    {
        private readonly IConfiguration Configuration;
        private readonly ILogger<ErrorModel> Logger;
        private string ConnectionString { get; }
        public QuestionnaireJsonService(IConfiguration configuration, ILogger<ErrorModel> logger)
        {
            Configuration = configuration;
            ConnectionString = $"{Environment.CurrentDirectory}{Configuration.GetSection("JsonConnection").Value}";
            Logger = logger;    
        }
        
        public async Task<QuestionnaireModel> GetQuestionnaireById(int Id)
        {
            QuestionnaireModel result = null;
            using (var stream = new FileStream(ConnectionString, FileMode.Open, FileAccess.Read))
            {
                try 
                { 
                    var questionnaires = await JsonSerializer.DeserializeAsync<List<QuestionnaireModel>>(stream);
                    result = questionnaires?.FirstOrDefault(x => x.Id == Id);
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex.ToString());
                }
                finally
                {
                    stream.Close();
                }
                return result;
            }
        }
    }
}
