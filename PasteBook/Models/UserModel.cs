using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PasteBook.Models
{
    public class UserModel
    {
        public int ID { get; set; }

        [Required]
        [DisplayName("Username")]
        public string USERNAME { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string FIRST_NAME { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string LAST_NAME { get; set; }

        [Required]
        [DisplayName("Email Address")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string EMAIL_ADDRESS { get; set; }

        [DisplayName("Gender")]
        public string GENDER { get; set; }

        [Required]
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        public string PASSWORD { get; set; }

        [Required]
        [DisplayName("Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("PASSWORD")]
        public string CONFIRM_PASSWORD { get; set; }

        [Required]
        [DisplayName("Birthday")]
        [DisplayFormat(DataFormatString = "{0:MMMM dd, yyyy}")]
        public DateTime BIRTHDAY { get; set; }

        [DisplayName("Country")]
        public Nullable<int> COUNTRY { get; set; }

        [DisplayName("Mobile Number")]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        public string MOBILE_NUMBER { get; set; }

        public byte[] PROFILE_PIC { get; set; }

        [DisplayName("Date Created")]
        [DisplayFormat(DataFormatString = "{0:MMMM dd, yyyy}")]
        public DateTime DATE_CREATED { get; set; }

        [DisplayName("About Me")]
        public string ABOUT_ME { get; set; }
    }
}

    