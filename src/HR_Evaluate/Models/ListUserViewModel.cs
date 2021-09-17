namespace HR_Evaluate.Models
{
    public class ListUserViewModel
    {
        public int EmployeeId { get; set; }

        public string EmployeeCode { get; set; }

        public string EmployeeName { get; set; }

        public string ProfileImage { get; set; }

        public string Department { get; set; }

        public string Position { get; set; }

        public string Level { get; set; }

        public int IsEnable { get; set; }

        public int EvaluateTimes { get; set; }

        // Computed field
        public bool HasSummary { get; set; }

        public string Status
        {
            get
            {
                if (HasSummary) return "Completed";
                return "Progressing";
            }
        }

        public string StatusCss
        {
            get
            {
                if (HasSummary) return "badge badge-info";
                return "badge badge-white";
            }
        }
    }
}