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

            // Pobierz API Key z konfiguracji
            var apiKey = configuration.GetValue<string>("LLMApi:ApiKey");
            // URL API Gemini do generowania treści
            _apiUrl = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={apiKey}";
                        
        }

        // Metoda do pobrania odpowiedzi od Gemini z danymi turystycznymi
        public async Task<List<MyModel>> GetListFromLLMAsync()
        {
            // Przykładowy prompt do Gemini
            var prompt = @"Wygeneruj listę 5 obiektów turystycznych w formacie JSON.
                            Każdy obiekt powinien mieć następujące właściwości:
                            - Id (string),
                            - Index (int),
                            - Guid (format GUID),
                            - IsActive (true/false),
                            - Balance (string z $),
                            - Picture (url do zdjęcia),
                            - Name (nazwa miejsca),
                            - Location (nazwa miasta i kraju),
                            - Height (int w metrach),
                            - Length (int w metrach),
                            - YearBuilt (rok budowy),
                            - Type (typ atrakcji: muzeum, park, pomnik itp.),
                            - Description (krótki opis),
                            - Tags (lista 3 tagów w formie listy tekstów)";

            // Pobierz odpowiedź z Gemini
            var responseJson = await GetResponseFromGeminiAsync(prompt);

            // Zdeserializuj odpowiedź w JSON-ie do listy obiektów MyModel
            var list = JsonConvert.DeserializeObject<List<MyModel>>(responseJson);
            return list;
        }

        // Metoda do pobrania odpowiedzi z Gemini, zwracająca treść
        public async Task<string> GetResponseFromGeminiAsync(string prompt)
        {
            // Tworzenie ciała zapytania
            var requestBody = new
            {
                contents = new[] {
                    new
                    {
                        parts = new[] { new { text = prompt } }
                    }
                }
            };

            // Serializowanie do JSON
            var json = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Wykonanie zapytania POST do API
            var response = await _httpClient.PostAsync(_apiUrl, content);
            response.EnsureSuccessStatusCode();

            // Odczytanie odpowiedzi i deserializacja
            var responseBody = await response.Content.ReadAsStringAsync();
            dynamic result = JsonConvert.DeserializeObject(responseBody);

            // Zwrócenie tekstu z odpowiedzi
            string text = result.candidates[0].content.parts[0].text;

            // Zwrócenie odpowiedzi w postaci JSON
            return text;
        }

        // Metoda do pobrania obiektu po ID
        public async Task<MyModel> GetObjectByIdFromLLMAsync(int id)
        {
            var list = await GetListFromLLMAsync();
            return list.FirstOrDefault(x => x.Index == id);
        }

        // Metoda do filtrowania obiektów na podstawie typu
        public async Task<List<MyModel>> GetFilteredObjectsFromLLMAsync(string filter)
        {
            var list = await GetListFromLLMAsync();
            return list.Where(x => x.Type.Contains(filter, StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }
}
