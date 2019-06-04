using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Facebook.Models
{
    public class Post
    {
        [Key]
        public int postId { get; set; }
        [Required]
        public string postContent  { get; set; }
        [Required]
        public DateTime postDate { get; set; }
        public bool postDelete { get; set; }
        [ForeignKey("ApplicationUser")]
        public string userId { get; set; }
        public byte[] postImg { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<Like> Like { get; set; }
        public virtual ICollection<Comment> Comment { get; set; }




    }
}