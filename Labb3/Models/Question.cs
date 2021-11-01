namespace Labb3.Models
{
    internal sealed class Question
    {
        public string Statement { get; }
        public string[] Answers { get; }
        public int CorrectAnswer { get; }
        public Category Category { get; }

        public Question(string statement, string[] answers, Category category ,int correctAnswer)
        {
            Statement = statement;
            Answers = answers;
            CorrectAnswer = correctAnswer;
            Category = category;
        }
    }
}
