using AutoMapper;
using Microsoft.Extensions.Options;
using RankUpp.Application.Interfaces.Repositories;
using RankUpp.Application.Interfaces.Services;
using RankUpp.Core.Configurations;
using RankUpp.Core.DTOs.Input;
using RankUpp.Core.DTOs.Input.Gemini;
using RankUpp.Core.DTOs.Output;
using RankUpp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RankUpp.Application.Services
{
    public class RoadMapService : IRoadMapService, IDisposable
    {
        private readonly IRoadMapRepository _roadMapRepository;

        private readonly IOptions<GeminiSettings> _geminiSettings;

        private readonly IMapper _mapper;

        private HttpClient _httpClient;

        public RoadMapService(IRoadMapRepository roadMapRepository, IOptions<GeminiSettings> geminiSettings, IMapper mapper)
        {
            _roadMapRepository = roadMapRepository;

            _geminiSettings = geminiSettings;

            _mapper = mapper;

            _httpClient = new HttpClient();
        }

        public async Task<RoadMap> CreateRoadMapAsync(RoadMap roadMap, CancellationToken cancellation = default)
        {
            return await _roadMapRepository.CreateRoadMapAsync(roadMap, cancellation);
        }

        public async Task<RoadMap> GenerateRoadMapAsync(string keyword, CancellationToken cancellation = default)
        {
            try
            {
                string key = _geminiSettings.Value.ApiKey;
                string url = _geminiSettings.Value.Url + key;


                string jsonData = JsonSerializer.Serialize<CreateRoadMapDTO>(Exemple());
                string prompt = $"Kérlek készits roadmapet a következő témában: {keyword}, és tartsd be ezt a Json formát ID nélkül " + jsonData;


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

                    if (quizJson != null)
                    {
                        int firstIndex = quizJson.IndexOf('{');
                        int lastIndex = quizJson.LastIndexOf('}');

                        if (firstIndex != -1 && lastIndex != -1 && lastIndex > firstIndex)
                        {
                            quizJson = quizJson.Substring(firstIndex, lastIndex - firstIndex + 1);
                        }

                        var quiz = JsonSerializer.Deserialize<CreateRoadMapDTO>(quizJson);

                        return _mapper.Map<RoadMap>(quiz);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return null;

        }

        private CreateRoadMapDTO Exemple()
        {
            return new CreateRoadMapDTO
            {
                Title = "ASP.NET Core Backend Developer Roadmap",
                Description = "Essential steps to becoming a proficient ASP.NET Core backend developer.",
                Items = new List<CreateRoadMapItemDTO>
                {
                    new CreateRoadMapItemDTO { Value = "Learn C# Fundamentals", Order = 1 },
                    new CreateRoadMapItemDTO { Value = "Understand ASP.NET Core Basics", Order = 2 },
                    new CreateRoadMapItemDTO { Value = "Work with Entity Framework Core", Order = 3 },
                    new CreateRoadMapItemDTO { Value = "Build a REST API", Order = 4 },
                    new CreateRoadMapItemDTO { Value = "Authentication & Authorization (JWT, OAuth)", Order = 5 }
                }
            };
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
