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
    public class HomeController : Controller
    {
        private BeltExam2Context _context;
 
        public HomeController(BeltExam2Context context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("main")]
        public IActionResult Main()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if(userId.HasValue)
            {
                return Redirect("/bright_ideas");
            } else {
                return View("Index");
            }  
        }

        [HttpPost]
        [Route("registration")]
        [ValidateAntiForgeryToken]
        public IActionResult Registration(UserViewModel model, User newUser)
        {
            
            if(ModelState.IsValid)
            {
                
                User exists = _context.users.SingleOrDefault(user => user.Email == newUser.Email);
                if(exists == null){
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    newUser.Password = Hasher.HashPassword(exists, model.Password);
                   
                    _context.users.Add(newUser);
                    _context.SaveChanges();
                    HttpContext.Session.SetInt32("UserId", newUser.UserId);
                    // TempData.name = $"{newUser.FirstName} {newUser.LastName}";
                    return Redirect("/bright_ideas");
                } else {
                    ViewBag.error_exists = "This email already exists in the database. Try Logging in.";
                    return View("Index");
                }
            
            } else {
                return View("Index");
            }
            
        }

        
        [HttpPost]
        [Route("login")]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string Email, string Password)
        {
            
            User exists = _context.users.SingleOrDefault(user => user.Email == Email);
            if(exists != null && Password != null)
            {
                var Hasher = new PasswordHasher<User>();
                if(0 != Hasher.VerifyHashedPassword(exists,exists.Password,Password))
                {
                    HttpContext.Session.SetInt32("UserId", exists.UserId);
                    return Redirect("/bright_ideas");
                }
            }
            ViewBag.error = $"Must submit an email and a password";
            return View("Index"); 
            
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/main");
        }

    }
}
