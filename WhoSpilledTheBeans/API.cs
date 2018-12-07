using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WhoSpilledTheBeans
{
    class API
    {
        private string API_KEY = "59b44a5303msh83c407f6203c2ffp10467ajsnc4ae2fa612a1";
        private string API_URL = "https://andruxnet-random-famous-quotes.p.rapidapi.com/?count=10&cat=Movies";
        public async Task GetQuotes(Model model)
        {
            
            var httpMethod = HttpMethod.Get;

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(API_URL),
                Method = httpMethod,
            };
            request.Headers.Add("X-RapidAPI-Key", API_KEY);

            HttpClient client = new HttpClient();
            var response = await client.SendAsync(request);

            List<Quote> Quotes = new List<Quote>();
            if (response.StatusCode == HttpStatusCode.OK) 
            {
                var leif = await response.Content.ReadAsStringAsync();
                Quotes = JsonConvert.DeserializeObject<List<Quote>>(leif);
                model.Quotes = Quotes;
            }
            else
            {
                Console.WriteLine("Error getting Quotes from API");
            }
        }
    }
}
