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
        Model model;
        Quote _currentQuote;
        Quote CurrentQuote
        {
            get { return _currentQuote; }
            set {
                _currentQuote = value;
                AuthorLabel.Text = _currentQuote.author;
                QuoteContainer.Text = _currentQuote.quote;
            }
        }
        GameQuote CurrentGameQuote;
        List<Button> answerButtons;
        Label scoreLabel;
        public static readonly int NUMBER_OF_TURNS = 9;

        public MainPage()
        {
            InitializeComponent();
            InitializeApp();
        }
        
        private void AddButtons()
        {
            string[] answers = CurrentGameQuote.Answers;
            for (int i = 0; i < answers.Length; i++)
            {
                Button button = new Button { Text = answers[i], ClassId = "Answer" + i };
                button.Clicked += GuessBtnClicked;
                // Creating a binding
                button.SetBinding(Button.CommandProperty, new Binding("Answer" + i));
                answerButtons.Add(button);
                MainStackLayout.Children.Add(button);
            }
        }

        private void GuessBtnClicked(object sender, EventArgs args)
        {
            Button b = (Button)sender;
            if (CurrentGameQuote.CheckAnswer(b.Text)) //Returns true if answer matches
            {
                model.Score += 1;
            }
            AuthorLabel.IsVisible = true;
            UpdateScoreLabel();
            if (model.index >= NUMBER_OF_TURNS)     //Returns true if the player is done with all rounds
            {
                for (int i = answerButtons.Count() - 1; i >= 0; i--)
                {
                    MainStackLayout.Children.Remove(answerButtons[i]);
                    answerButtons.Remove(answerButtons[i]);
                }
                Navigation.PushAsync(new ScoreBoardPage());
                NewGame();
            }
            else WaitForNextRound();
        }

        private void UpdateScoreLabel()
        {
            scoreLabel.Text = model.Score.ToString() + " / " + model.index;
        }

        private void InitializeApp()
        {
            model = new Model();
            answerButtons = new List<Button>();
            scoreLabel = new Label();
            MainStackLayout.Children.Add(scoreLabel);
            NewGame();
        }

        private async void NewGame()
        {
            StartButton.IsEnabled = true;
            model.Score = 0;
            model.index = 0;
            UpdateScoreLabel();
            QuoteContainer.Text = "Loading quotes...";
            AuthorLabel.Text = "";
            await model.PingAPI();
            QuoteContainer.Text = "Done. Press Begin to Begin";
        }

        void OnButtonClicked(object sender, EventArgs args)
        {
            //Without GameMode
            //CurrentQuote = model.GetQuote();
            //With Game mode
            Button b = (Button)sender;
            AuthorLabel.IsVisible = false;

            NewQuote();
            AddButtons();

            //int count = 0;
            //if (GQ.Answers.Length > answerButtons.Count()) count = answerButtons.Count();
            //if (GQ.Answers.Length < answerButtons.Count()) count = GQ.Answers.Length;
            //Console.WriteLine(GQ.Answers.Length + " HALLÅÅ " + answerButtons.Count() + GQ.Answers[0]);

            b.IsEnabled = false;
        }

        private void NewQuote()
        {
            AuthorLabel.IsVisible = false;
            GameQuote GQ = model.GetGameQuote();
            CurrentGameQuote = GQ;
            CurrentQuote = GQ.Quote;
            for (int i = 0; i < answerButtons.Count(); i++) 
            {
                answerButtons[i].Text = GQ.Answers[i];
            }
        }

        private void WaitForNextRound()
        {
            // Wait method here
            NewQuote();
        }
    }
}
