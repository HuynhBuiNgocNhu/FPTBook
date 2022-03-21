namespace FptBook.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class account
    {
        public account()
        {
            orders = new HashSet<order>();
        }
        [Required]
        [Key]
        [StringLength(50)]
        public string username { get; set; }

        [Required]
        [StringLength(50)]
        public string fullname { get; set; }


        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string password { get; set; }
        // confirmpassword
        [NotMapped]
        [Compare("password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string ConfirmPassword { get; set; }
        // update profile
        [NotMapped]
        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Current Password")]
        public string CurrentPassword { get; set; }

        [NotMapped]
        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }



        [NotMapped]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation new password do not match.")]
        [Display(Name = "Confirm New Password")]
        public string ConfirmNewPassword { get; set; }


        [Required]
        [StringLength(20)]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@1-1000000\s]+$", ErrorMessage = "Invalid email number")]
        [EmailAddress(ErrorMessage = "Invalid Email address")]
        [Display(Name = "Email")]
        public string email { get; set; }

        [Required]
        [StringLength(10)]
        [RegularExpression(@"(84|0[3|5|7|8|9])+([0-9]{8})\b", ErrorMessage = "Invalid phone number")]
        public string phone { get; set; }

        [Required]
        [StringLength(200)]
        public string address { get; set; }

        [Range(0, 1, ErrorMessage = "choose 1 and 0")]
        public int state { get; set; }

        public ICollection<order> orders { get; set; }
    }
}
