using System;
namespace WhoSpilledTheBeans
{
    public class Quote
    {
        public string quote { get; set; }
        public string author { get; set; }
        public string category { get; set; }
        public Quote(string A, string Q)
        {
            author = A;
            quote = Q;
        }
    }
}
