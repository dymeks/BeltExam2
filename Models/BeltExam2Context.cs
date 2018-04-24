using Microsoft.EntityFrameworkCore;
using System.Linq;
using BeltExam2.Models; 

namespace BeltExam2.Models
{
    public class BeltExam2Context : DbContext
    {
        
        public BeltExam2Context(DbContextOptions<BeltExam2Context> options) : base(options) { }
        public DbSet<User> users { get; set; }
        public DbSet<Post> posts { get; set; }
        public DbSet<Like> likes { get; set; }
    }
}