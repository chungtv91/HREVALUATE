namespace HR_Evaluate.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Login
    {
        public int Id { get; set; }

        [StringLength(30)]
        [Required(ErrorMessage ="Username is not empty")]
        public string UserName { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage ="Password is not empty")]
        public string Password { get; set; }

        public int? IsEnable { get; set; }

        public int? RoleID { get; set; }

        public int? BODID { get; set; }

        public virtual BOD BOD { get; set; }

        public virtual Role Role { get; set; }
    }
}
