using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace WhoSpilledTheBeans
{
    public partial class MainPage : ContentPage
    {

        private string API_KEY = "59b44a5303msh83c407f6203c2ffp10467ajsnc4ae2fa612a1";
        private string API_URL = "https://andruxnet-random-famous-quotes.p.rapidapi.com/?count=10&cat=Movies";

        public MainPage()
        {
            InitializeComponent();
            GetQuotes();
        }

        private async void GetQuotes()
        {
            
            var httpMethod = HttpMethod.Get; //or Get, or whatever HTTP verb your API endpoint needs

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(API_URL),
                Method = httpMethod,
            };
            request.Headers.Add("X-RapidAPI-Key", API_KEY);

            HttpClient client = new HttpClient();

            var response = await client.SendAsync(request);


            
            if (response.StatusCode == HttpStatusCode.OK)
            {
                //It worked, so do something

                var leif = await response.Content.ReadAsStringAsync();

                //JsonConvert.DeserializeObject<Quote>(leif);
                var Quote = JsonConvert.DeserializeObject<List<Quote>>(leif);

                QuoteLabel.Text = Quote[0].quote;
                AuthorLabel.Text = Quote[0].author;
            }
            else
            {
                //It didn't work, so do something else
            }

        }
    }
}
