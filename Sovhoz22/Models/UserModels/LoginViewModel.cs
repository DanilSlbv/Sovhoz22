using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sovhoz22.Models;
using System.ComponentModel.DataAnnotations;
namespace Sovhoz22.Models.UserModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Пожалуйста введите Email")]
        [DataType(DataType.EmailAddress)]
        [Display(Name ="Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Пожалуйста введите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}
