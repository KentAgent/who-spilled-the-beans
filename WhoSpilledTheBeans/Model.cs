using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoSpilledTheBeans
{
    class Model
    {
        API api = new API();
        public int index = 0;
        public int difficulty = 3;
        public int Score { get; set; }
        private List<Quote> _quotes = new List<Quote>(); // ANvänds inte
        public  List<Quote> Quotes {
            set { _quotes = value; }
            
        }
        private Quote CurrentQuote;

        public Model()
        {

        }

        public GameQuote GetGameQuote()
        {
            Random r = new Random();
            string[] answers = new string[difficulty + 1];
            for (int i = 0; i < difficulty; i++)
            {
                int ranIndex = (r.Next()%_quotes.Count());
                string answer = _quotes[ranIndex].author;
                answers[i] = answer;
            }
            Quote q = GetQuote();
            answers[difficulty] = q.author;
            return new GameQuote(q, answers);
        } // Returns a loaded GameQuoteObject

        public Quote GetQuote()
        {
            if (index >= _quotes.Count()-1) index = 0;
            else index++;
            if (_quotes[index] == null) return new Quote("The ErrorGuy", "No Quote Found");
            else return _quotes[index];
        }

        public async Task PingAPI()
        {
            await Task.Run(async () =>
             {
                 await api.GetQuotes(this);
                 return _quotes;
             });
        } // Notify the API and waits for a reply
    }
}
