using System.Collections.ObjectModel;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace Labb3.Models
{
    internal sealed class PlayModel : ObservableObject
    {
        private Question _currentQuestion;

        public Question CurrentQuestion
        {
            get => _currentQuestion;
            set => SetProperty(ref _currentQuestion, value);
        }

        public Quiz CurrentQuiz { get; }

        private ObservableCollection<Question> _answeredQuestions;

        public ObservableCollection<Question> AnsweredQuestions
        {
            get => _answeredQuestions;
            set => SetProperty(ref _answeredQuestions, value);
        }

        private ObservableCollection<string> _chosenCategories;

        public ObservableCollection<string> ChosenCategories
        {
            get => _chosenCategories;
            set => SetProperty(ref _chosenCategories, value);
        }

        private int _validQuestionCount = 0;

        public int ValidQuestionCount
        {
            get => _validQuestionCount;
            set => SetProperty(ref _validQuestionCount, value);
        }

        public PlayModel(Quiz currentQuiz)
        {
            CurrentQuiz = currentQuiz;
            AnsweredQuestions = new ObservableCollection<Question>();
            ChosenCategories = new ObservableCollection<string>();
        }

        //Makes sure that not all questions has been answered and that the question hasn't been answered or is the wrong category. Then sets the currentQuestion.
        public bool GetQuestion()
        {
            ValidQuestionCount = GetValidQuestionCount();
            if (AnsweredQuestions.Count == ValidQuestionCount)
            {
                return false;
            }

            var question = CurrentQuiz.GetRandomQuestion();
            while (AnsweredQuestions.Contains(question) || !ChosenCategories.Contains(question.Category.Name.ToLower()))
            {
                question = CurrentQuiz.GetRandomQuestion();
            }

            CurrentQuestion = question;
            return true;
        }

        //Checks if the parameter answer is the same as CorrectAnswer.
        public bool ValidateAnswer(int answer)
        {
            return answer == CurrentQuestion.CorrectAnswer;
        }

        public int GetValidQuestionCount()
        {
            var tempcount = 0;
            foreach (var question in CurrentQuiz.Questions)
            {
                if (ChosenCategories.Contains(question.Category.Name.ToLower()))
                {
                    tempcount++;
                }
            }

            return tempcount;
        }
    }
}
