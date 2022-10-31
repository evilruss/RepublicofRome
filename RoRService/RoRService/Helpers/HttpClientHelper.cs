using RoRService.Models;
using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;


namespace RoRService.Helpers
{
    public static class HttpClientHelper
    {

        public static HttpClient GetHttpClient()
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["RoRServiceURI"]);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
    }
}