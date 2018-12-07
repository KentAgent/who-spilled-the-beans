using System;
using System.Collections.Generic;
using System.Text;

namespace WhoSpilledTheBeans
{
    class GameQuote
    {
        public Quote Quote { get; set; }
        public string[] Answers { get; set; }
        public GameQuote(Quote Q, string[] A)
        {
            Quote = Q;
            Answers = ShuffleAnswers<string>(A);
        }

        public bool CheckAnswer(string A)
        {
            if (Quote.author == A) return true;
            return false;
        }

        private T[] ShuffleAnswers<T>(T[] array)
        {
            Random r = new Random();
            for (int i = array.Length; i > 1; i--)
            {
                int j = r.Next(i);                            
                T tmp = array[j];
                array[j] = array[i - 1];
                array[i - 1] = tmp;
            }
            return array;
        }
    }

}
