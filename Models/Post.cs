using System.ComponentModel.DataAnnotations;
using System.Linq;
using BeltExam2.Models;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;

namespace BeltExam2.Models
{

    public class Post : BaseEntity
    {
        [Key]
        public int PostId { get; set; }
        
        [Required(ErrorMessage = "You need to type a message to Post!")]
        public string Message { get; set; }

        [Required]
        public int CreatedById { get; set; }

        public User CreatedBy { get; set; }

        public List<Like> likes { get; set; }

        public DateTime created_at { get; set; }

        public DateTime updated_at { get; set; }

        public Post() {
            likes = new List<Like>();
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }
    }
}

// @order.createdAt.ToString("MMMM dd yyyy")