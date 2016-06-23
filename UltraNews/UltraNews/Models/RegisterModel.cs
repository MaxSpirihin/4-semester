using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace UltraNews.Models
{
    public class RegisterModel
    {
        [Required]
        [Display(Name = "логин")]
        public string Login { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        [StringLength(100, ErrorMessage = "Пароль не может быть меньше 3 и больше 100 символов", MinimumLength = 3)]
        public string Password { get; set; }

        public string RepeatPassword { get; set; }

        [Required]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        public string FamilyName { get; set; }
        public DateTime? BirthDay { get; set; }

    }
}