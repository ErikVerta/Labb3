using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace Labb3.ViewModels
{
    internal sealed class ResultViewModel: ObservableObject
    {
        private int _correctAnswers;

        public int CorrectAnswers
        {
            get => _correctAnswers;
            set => SetProperty(ref _correctAnswers, value);
        }

        private int _totalQuestionsCount;

        public int TotalQuestionCount
        {
            get => _totalQuestionsCount;
            set => SetProperty(ref _totalQuestionsCount, value);
        }

        public ResultViewModel(int correctAnswers, int totalQuestionsCount)
        {
            _correctAnswers = correctAnswers;
            _totalQuestionsCount = totalQuestionsCount;
        }
    }
}
