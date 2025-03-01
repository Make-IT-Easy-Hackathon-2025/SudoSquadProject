using AutoMapper;
using RankUpp.Application.Interfaces.Repositories;
using RankUpp.Application.Interfaces.Services;
using RankUpp.Core.DTOs.Input;
using RankUpp.Core.DTOs.Input.Gemini;
using RankUpp.Core.DTOs.Output;
using RankUpp.Core.Exceptions;
using RankUpp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RankUpp.Application.Services
{
    public class QuizService : IQuizService, IDisposable
    {
        private readonly IQuizRepository _quizRepository;

        private readonly IUserRepository _userRepository;

        private HttpClient _httpClient;

        private readonly IMapper _mapper;

        public QuizService(IQuizRepository quizRepository, IUserRepository userRepository, IMapper mapper)
        {
            _quizRepository = quizRepository;

            _userRepository = userRepository;

            _mapper = mapper;

            _httpClient = new HttpClient();
        }

        public async Task<List<QuizAttempt>> AddQuizAttemptsAsync(List<QuizAttempt> attempts, CancellationToken cancellationToken = default)
        {
            return await _quizRepository.AddQuizAttemptsAsync(attempts, cancellationToken);
        }

        public async Task<Quiz> CreateQuizAsync(Quiz quiz, CancellationToken cancellationToken)
        {
            return await _quizRepository.CreateQuizAsync(quiz, cancellationToken);
        }

        public async Task<int> EvaluateQuizAsync(int quizId, int userId, CancellationToken cancellationToken = default)
        {
            var quizes = await _quizRepository.GetQuizByIdAsync(quizId, cancellationToken);

            if(quizes == null)
            {
                throw new InvalidIdException();
            }

            var answers = await _quizRepository.GetQuizAttemptsAsync(quizId, userId, cancellationToken);

            int rightAnswers = 0;

            foreach (var item in quizes.Questions)
            {
                foreach (var option in item.Options)
                {
                    if(option.IsCorrect && answers.Any(x => x.QuizOptionId == option.Id))
                    {
                        rightAnswers++;
                    }
                }
            }

            await _userRepository.UpdateUserScoreAsync(userId, rightAnswers - answers.Count);

            return rightAnswers - answers.Count;
        }

        public async Task<List<Quiz>> GetAllQuizsAsync(int? pageNumber = null, int? pageSize = null, CancellationToken cancellationToken = default)
        {
            return await _quizRepository.GetAllQuizsAsync(pageNumber, pageSize, cancellationToken);
        }

        public async Task<Quiz?> GetQuizByIdAsync(int id, CancellationToken cancellation)
        {
            return await _quizRepository.GetQuizByIdAsync(id, cancellation);
        }

        public async Task<QuizReplayDTO> GetQuizReplayByIdAsync(int quizId, int userId, CancellationToken cancellationToken = default)
        {
            var quiz = await this.GetQuizByIdAsync(quizId, cancellationToken);

            var attempts = await _quizRepository.GetQuizAttemptsAsync(quizId, userId, cancellationToken);

            var map = attempts.ToDictionary(x => x.QuizOptionId);

            var result = _mapper.Map<QuizReplayDTO>(quiz);

            foreach (var question in result.Questions)
            {
                foreach (var option in question.Options)
                {
                    if (map.ContainsKey(option.Id))
                    {
                        option.IsSelected = true;
                    }
                }
            }

            return result;
        }

        public async Task<Quiz> GenerateQuizAsync(PromptInputDTO promptInput, CancellationToken cancellationToken = default)
        {

            try
            {
                string key = "AIzaSyC_A1VZXu6A-ap1SCpywNNsTY0IGdg0aUk";
                string url = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key=" + key;


                string jsonData = JsonSerializer.Serialize<QuizDTO>(Exemple());
                string prompt = $"Kérlek készits quizt a következő témában: {promptInput.Keyword}, és tartsd be ezt a Json formát ID nélkül " + jsonData;


                var request = new ContentRequest
                {
                    Contents = new[]
                    {
                        new Core.DTOs.Input.Gemini.Content
                        {
                            Parts = new[]
                            {
                                new Core.DTOs.Input.Gemini.Part
                                {
                                    Text = prompt,
                                }
                            }
                        }
                    }
                };

                string jsonRequest = JsonSerializer.Serialize<ContentRequest>(request);

                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    var geminResponse = JsonSerializer.Deserialize<GeminiResponse>(responseBody);

                    var quizJson = geminResponse.Candidates[0].Content.Parts[0].Text;

                    if(quizJson != null)
                    {
                        int firstIndex = quizJson.IndexOf('{');
                        int lastIndex = quizJson.LastIndexOf('}');

                        if (firstIndex != -1 && lastIndex != -1 && lastIndex > firstIndex)
                        {
                            quizJson = quizJson.Substring(firstIndex, lastIndex - firstIndex + 1);
                        }

                        var quiz = JsonSerializer.Deserialize<QuizDTO>(quizJson);

                        return _mapper.Map<Quiz>(quiz);
                    }
                }
                
            }
            catch (Exception)
            {

                throw;
            }

            return null;
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }

        private QuizDTO Exemple()
        {
            return  new QuizDTO
            {
                Title = "Tudományos Kvíz",
                Description = "Ez a kvíz a tudományos ismereteidet teszteli.",
                Questions = new List<QuizQuestionDTO>
                {
                    new QuizQuestionDTO
                    {
                        Value = "Mi a víz kémiai képlete?",
                        Options = new List<QuizOptionDTO>
                        {
                            new QuizOptionDTO { OptionValue = "H2O", IsCorrect = true },
                            new QuizOptionDTO { OptionValue = "CO2", IsCorrect = false },
                            new QuizOptionDTO { OptionValue = "O2", IsCorrect = false },
                            new QuizOptionDTO { OptionValue = "CH2", IsCorrect = false }
                        }
                    },
                    new QuizQuestionDTO
                    {
                        Value = "Melyik bolygó a Naprendszerben a legnagyobb?",
                        Options = new List<QuizOptionDTO>
                        {
                            new QuizOptionDTO { OptionValue = "Jupiter", IsCorrect = true },
                            new QuizOptionDTO { OptionValue = "Mars", IsCorrect = false },
                            new QuizOptionDTO { OptionValue = "Föld", IsCorrect = false },
                            new QuizOptionDTO { OptionValue = "Szaturnusz", IsCorrect = false },
                        }
                    }
                }
            };
        }

        public async Task<Quiz?> SerachForNewQuizAsync(string keyword, int userId, CancellationToken cancellationToken = default)
        {
            var quizzes = await _quizRepository.SearchQuizByKeywordAsync(keyword, cancellationToken);

            if(quizzes.Count == 0)
            {
                return null;
            }

            var attempts = await _quizRepository.GetAllQuizAttemptsAsync(userId, quizzes.Select(x => x.Id).ToList(), cancellationToken);

            var ids = attempts.Select(x => x.QuizId);

            foreach (var item in quizzes)
            {
                if (ids.Contains(item.Id) == false)
                {
                    return item;
                }
            }

            return null;
        }
    }
}
