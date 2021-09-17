using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRE.Web.UI.Models
{
    public class HrEvaluateViewModel
    {
        // public int EmployeeId { get; set; }

        public List<UserQuestion> VMlstQuestions { get; set; }

        public class UserQuestion
        {
            public int VMId { get; set; }
            public string VMQuestionName { get; set; }

            [Required(ErrorMessage = "The Comment is not null")]
            public string VMAnswerName { get; set; }

            [Required(ErrorMessage = "Select From 1-5")]
            [Range(1, 5, ErrorMessage = "Select From 1-5")]
            //[StringLength(1, ErrorMessage = "Select From 1-5")]
            public int? NumberScore { get; set; }

        }
    }
}
