using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BeltExam2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BeltExam2.Controllers
{
    public class BrightIdeasController : Controller
    {
        private BeltExam2Context _context2;
 
        public BrightIdeasController(BeltExam2Context context)
        {
            _context2 = context;
        }

        [HttpGet]
        [Route("bright_ideas")]
        public IActionResult BrightIdeas()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if(userId.HasValue)
            {   
                User currentUser = _context2.users.SingleOrDefault(user => user.UserId == userId.Value);
                var allPosts = _context2.posts.Include(post => post.CreatedBy).Include(post => post.likes).OrderByDescending(post => post.likes.Count).ToList();
                ViewBag.posts = allPosts;
                ViewBag.name = currentUser.Name;
                ViewBag.userId = userId.Value;
                return View("BrightIdeas");
            } else {
                return Redirect("/main");
            }  
            
        }
        
        [HttpGet]
        [Route("like/{UserId}/{PostId}")]
        public IActionResult NewLike(int UserId, int PostId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if(userId.HasValue)
            {  
                Like exists = _context2.likes.SingleOrDefault(like => like.UserId == UserId && like.PostId == PostId);

                if(exists == null)
                {
                    Like newLike = new Like()
                    {
                        PostId = PostId,
                        UserId  = UserId
                    };

                    _context2.Add(newLike);
                    _context2.SaveChanges();
                   
                }
                 return Redirect($"/bright_ideas");
            } else {
                return Redirect("/main");
            }     
        }

        [HttpGet]
        [Route("bright_ideas/{PostId}")]
        public IActionResult DisplayLikes(int PostId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if(userId.HasValue)
            { 
                List<Like> postLikes = _context2.likes.Include(like => like.User).Where(like => like.PostId == PostId).ToList();
                Post currentpost = _context2.posts.Include(post => post.CreatedBy).SingleOrDefault(post => post.PostId == PostId);
                ViewBag.postLikes = postLikes;
                ViewBag.post = currentpost;
                return View("DisplayLikes");
            }  else {
                return Redirect("/main");
            } 

        }    

        [HttpGet]
        [Route("users/{UserId}")]
        public IActionResult DisplayUser(int UserId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if(userId.HasValue)
            { 
                User exists = _context2.users.SingleOrDefault(user => user.UserId == UserId);
                List<Post> userPosts = _context2.posts.Where(post => post.CreatedById == UserId).ToList();
                List<Like> userLikes = _context2.likes.Where(like => like.UserId == UserId).ToList();

                ViewBag.name = exists.Name;
                ViewBag.alias = exists.Alias;
                ViewBag.email = exists.Email;
                ViewBag.totalPosts = userPosts.Count;
                ViewBag.totalLikes = userLikes.Count;
                return View("DisplayUser");
            } else {
                return Redirect("/main");
            } 
        }

        [HttpPost]
        [Route("create_post")]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePost(Post newPost)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if(userId.HasValue)
            { 
                if(ModelState.IsValid) {
                    _context2.posts.Add(newPost);
                    _context2.SaveChanges();
                    return Redirect("/bright_ideas");
                } else {
                    return View("BrightIdeas");
                }
            } else {
                return Redirect("/main");
            }

        }

        [HttpPost]
        [Route("deletePost")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int PostId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if(userId.HasValue)
            {
                Post postDelete = _context2.posts.SingleOrDefault(post => post.PostId == PostId);
                _context2.posts.Remove(postDelete);
                _context2.SaveChanges();
                return Redirect("/bright_ideas");
            } else {
                return Redirect("/main");
            }
        }     
    }
}