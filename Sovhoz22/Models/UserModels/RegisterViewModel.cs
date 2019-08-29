using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Sovhoz22.Models.UserModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Пожалуйста введите Email")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Пожалуйста введите год рождения")]
        
        [Display(Name = "Год рождения")]
        public int Year { get; set; }
        [Required(ErrorMessage = "Пожалуйста введите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare("Password",ErrorMessage = "Пожалуйста подтвердите пароль")]
        public string PasswordConfirm { get; set; }
    }
}
