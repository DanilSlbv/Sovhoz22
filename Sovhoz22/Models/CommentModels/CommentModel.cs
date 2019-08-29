using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Sovhoz22.Models.CommentModels
{
    public class CommentModel
    {
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public int Phone { get; set; }
        public DateTime Time  { get; set; }
        public string UserName{ get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
    }
}
