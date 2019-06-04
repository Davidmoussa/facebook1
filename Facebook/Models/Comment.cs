using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Linq;
using System.Web;

namespace Facebook.Models
{
    public class Comment
    {
        [Key]
        public int commentId { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        [ForeignKey("ApplicationUser")]
        public string userId { get; set; }
        [Required]
        [ForeignKey("Post")]
        public int postId { get; set; }
        public bool commentDeleted { get; set; }
        [Required]
        public DateTime commentDate { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public Post Post { get; set; }

    }
}