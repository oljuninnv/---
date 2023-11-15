﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WpfApp1
{
    public class Requester
    {
        private static string GetBaseUrl()
        {
            return "https://portal.kuzstu.ru/";
        }
        public static async Task<dynamic> Post(string url, dynamic content, bool auth = false, string token = null)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsync(
            $"{GetBaseUrl()}{url}"
            , content);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            dynamic json = JsonConvert.DeserializeObject(responseBody);

            return json;
        }

        public static async Task<dynamic> Get(string url, bool auth = false, string token = null)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(
            $"{GetBaseUrl()}{url}");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            dynamic json = JsonConvert.DeserializeObject(responseBody);

            return json;
        }
    }
}