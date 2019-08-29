using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
namespace Sovhoz22.Models.PhoneModels
{
    public class EditPhoneViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public int Price { get; set; }
        public IFormFile PhoneImage { get; set; } 
    }
}
