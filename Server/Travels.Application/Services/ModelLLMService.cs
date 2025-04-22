using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Travels.Application.Interfaces;
using Travels.Domain.Entities;

namespace Travels.Application.Services
{
    public class ModelLLMService : IModelLLMService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string _apiUrl;

        public ModelLLMService(IConfiguration configuration, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            var apiKey = configuration.GetValue<string>("LLMApi:ApiKey");
            _apiUrl = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={apiKey}";
        }

        private readonly string _prompt = @"Wygeneruj listę 5 obiektów turystycznych w formacie JSON.
                Każdy obiekt powinien mieć następujące właściwości:
                - Id (int),
                - Kraj
                - Miasto (miasto gdzie znajduje się atrakcja)
                - Nazwa (nazwa miejsca),
                - Opis (krótki opis miejsca),
                - Kategoria (typ atrakcji: np. Kultura i Historia, Natura i Przygoda, Plaże i Krajobrazy)";

        public async Task<List<MyModel>> GetListFromLLMAsync()
        {
            var responseJson = await GetResponseFromGeminiAsync();
            var list = JsonConvert.DeserializeObject<List<MyModel>>(responseJson);
            return list;
        }

        private async Task<string> GetResponseFromGeminiAsync()
        {
            var requestBody = new
            {
                contents = new[] {
                    new
                    {
                        parts = new[] { new { text = _prompt } }
                    }
                }
            };

            var json = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_apiUrl, content);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            dynamic result = JsonConvert.DeserializeObject(responseBody);

            string text = result.candidates[0].content.parts[0].text;
            text = CleanJson(text);
            return text;
        }

        private string CleanJson(string rawText)
        {
            if (string.IsNullOrWhiteSpace(rawText))
                return rawText;

            rawText = rawText.Trim();

            if (rawText.StartsWith("```json"))
            {
                rawText = rawText.Substring(6).Trim();
            }
            if (rawText.StartsWith("```"))
            {
                rawText = rawText.Substring(3).Trim();
            }
            if (rawText.EndsWith("```"))
            {
                rawText = rawText.Substring(0, rawText.Length - 3).Trim();
            }

            int index = rawText.IndexOfAny(new char[] { '{', '[' });
            if (index >= 0)
            {
                rawText = rawText.Substring(index);
            }

            return rawText;
        }

        public async Task<MyModel> GetObjectByIdAsync(int id)
        {
            var list = await GetListFromLLMAsync();
            return list.FirstOrDefault(x => x.Id == id);
        }

        public async Task<List<MyModel>> GetFilteredObjectsAsync(string filter)
        {
            var list = await GetListFromLLMAsync();
            return list.Where(x => x.Miasto.Contains(filter, StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }
}
