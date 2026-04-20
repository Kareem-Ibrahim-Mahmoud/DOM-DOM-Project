using QudraSaaS.Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QudraSaaS.Application.Repo
{
    public class WhatssApp: IWhatsApp
    {
        private readonly HttpClient _httpClient;
        public WhatssApp()
        {
            _httpClient = new HttpClient();
        }

        public async Task<bool> SendOtpAsync(string phoneNumber, string otp)
        {
            var url = "https://wasenderapi.com/api/send-message";

            // الهيدر (API KEY)
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer b4980048923a762ccf4448bb36bac506f4c44bcacfc8fee588a548c8545abff5");

            // جسم الطلب
            var body = new
            {
                to = phoneNumber,
                text = otp
            };

            var json = JsonSerializer.Serialize(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync(url, content);
                var responseBody = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("خطأ من API: " + responseBody);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("حدث خطأ: " + ex.Message);
                return false;
            }
        }

    }
}
