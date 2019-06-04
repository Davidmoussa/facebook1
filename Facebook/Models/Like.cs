using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


using System.Linq;
using System.Web;

namespace Facebook.Models
{
    public class Like
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("ApplicationUser")]
        public string  userID { get; set; }
        [Key]
        [Column(Order = 2)]
        [ForeignKey("Post")]
        public int postid { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public  Post Post { get; set; }

        
    }
}