using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Sovhoz22.Models.CommentModels
{
    public class CreateCommentModel
    {
        public int Phone { get; set; }
        public string UserName { get; set; }
        [Required(ErrorMessage ="Введите комментарий")]
        [DataType(DataType.Text)]
        public string Text { get; set; }
        public DataType Time { get; set; }
        [Required(ErrorMessage = "Оцените товар")]
        public int Rating { get; set; }
    }
}
