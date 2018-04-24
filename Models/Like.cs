using System.ComponentModel.DataAnnotations;
using System.Linq;
using BeltExam2.Models;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;

namespace BeltExam2.Models
{

    public class Like : BaseEntity
    {
        [Key]
        public int LikeId { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int PostId { get; set; }

        public Post Post { get; set; }

        public DateTime created_at { get; set; }

        public DateTime updated_at { get; set; }

        public Like() {
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }
    }
}

// @order.createdAt.ToString("MMMM dd yyyy")