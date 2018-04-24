using System.ComponentModel.DataAnnotations;
using System.Linq;
using BeltExam2.Models;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;

namespace BeltExam2.Models
{

    public class User : BaseEntity
    {
        [Key]
        public int UserId { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public List<Like> likes { get; set; }

        public DateTime created_at { get; set; }

        public DateTime updated_at { get; set; }

        public User() {
            likes = new List<Like>();
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }
    }
}

// @order.createdAt.ToString("MMMM dd yyyy")