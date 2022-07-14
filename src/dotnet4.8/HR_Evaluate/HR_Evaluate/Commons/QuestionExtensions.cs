using HR_Evaluate.Models;
using System.Collections.Generic;
using System.Linq;

namespace HR_Evaluate.Commons
{
    public static class QuestionExtensions
    {
        public static string GetAnswer(this Question question, List<Sumary> summaries, string defaultvalue = "")
        {
            var summary = summaries.FirstOrDefault(x => x.QuestionID == question.Id);
            return summary?.AnswerName ?? defaultvalue;
        }

        public static int GetScore(this Question question, List<Sumary> summaries, int defaultvalue = 0)
        {
            var summary = summaries.FirstOrDefault(x => x.QuestionID == question.Id);
            return summary?.Score ?? defaultvalue;
        }
    }
}