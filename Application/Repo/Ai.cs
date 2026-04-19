using Microsoft.Extensions.Configuration;
using QudraSaaS.Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QudraSaaS.Application.Repo
{
    public class Ai: IAi
    {
        private readonly IConfiguration config;

        public Ai(IConfiguration config)
        {
            this.config = config;
        }
        public async Task<string>AskAi(string userPrompt)
        {
            string apiKey = config["JWT:apiKey"];
            // System prompt
            string systemPrompt = @"You are a smart assistant specialized in car engine oils, oil change services, and general car maintenance.
                Your goal is to accurately and clearly answer any question related to:

                Engine oils, types of oils, oil change intervals, benefits, and how to choose the right oil for any car model.

                General car maintenance, including checking brakes, tires, battery, filters, fluids, and routine servicing tips.

                Guardrails:

                Never go outside the topic of car maintenance or engine oils.

                If asked a question unrelated to cars or car maintenance, politely respond:

                ""I specialize only in car maintenance and engine oils. How can I assist you in this area?""

                Do not provide advice in medical, legal, or any other unrelated fields.

                Focus on giving practical, accurate, and reliable information strictly about cars.

                If you don’t know the answer, say:

                ""Sorry, I don't have accurate information on that topic. Do you have another question about car maintenance or engine oils?""

                Be friendly, helpful, and always open to any Arabic dialect.";

            var requestBody = new
            {
                model = "openai/gpt-4o-mini",
                messages = new[]
            {
                new { role = "system", content = systemPrompt },
                new { role = "user", content = userPrompt }
            }
            };

            string jsonBody = JsonSerializer.Serialize(requestBody);

            using HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://openrouter.ai/api/v1/");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            client.DefaultRequestHeaders.Add("HTTP-Referer", "http://localhost"); // optional but recommended
            client.DefaultRequestHeaders.Add("X-Title", "CSharp Chatbot");

            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("chat/completions", content);
            response.EnsureSuccessStatusCode();

            string responseJson = await response.Content.ReadAsStringAsync();

            // Parse response
            using JsonDocument doc = JsonDocument.Parse(responseJson);
            string reply = doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();
            return reply;

        }
        


    }
}
