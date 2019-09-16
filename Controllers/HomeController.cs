using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LogReg.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace LogReg.Controllers
{
    public class HomeController : Controller
    {
        private LogRegContext dbContext;
     
        public HomeController(LogRegContext context)
        {
            dbContext = context;
        }

        public IActionResult Index()
        {
            return View();
        }



        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet("Success")]
        public IActionResult Success()
        {
            return View();
        }

        [HttpPost("Register")]
        public IActionResult Registered(User user)
        {
            System.Console.WriteLine(user.Email);
            System.Console.WriteLine(user.Password);
            System.Console.WriteLine(ModelState.IsValid);
            if(ModelState.IsValid)
            {
                if(dbContext.Users.Any(u=> u.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "Email already in use!");
                    return RedirectToAction("");
                }
                else
                {

                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    user.Password = Hasher.HashPassword(user,user.Password);

                    dbContext.Add(user);
                    user.CreatedAt = DateTime.Now;
                    dbContext.SaveChanges();
                    return RedirectToAction("Success");
                }
            }

            else
            {
                return View("Index");
            }
        }



        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("Login")]
        public IActionResult Login(User userSubmission)
        {
            if(ModelState.IsValid)
            {
                User userInDb = dbContext.Users.FirstOrDefault(u => u.Email == userSubmission.Email);
                if(userInDb == null)
                {
                    ModelState.AddModelError("Email", "Invalid Email/Password");
                    return View("Login");
                }

                else
                {
                PasswordHasher<User>  hasher = new PasswordHasher<User>();

                PasswordVerificationResult result = hasher.VerifyHashedPassword(userSubmission, userInDb.Password, userSubmission.Password);

                if(result == 0)
                {
                    ModelState.AddModelError("Email", "Invalid Email/Password");
                    return View("Login");
                }
                    return RedirectToAction("Success");
                }
            }
            else
            {
                return View("Index");
            }
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
