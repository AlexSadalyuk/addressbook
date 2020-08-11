using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private string domain => Request.Host.Host;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {

            IEnumerable<UserShortInfo> users = _userService.GetUsers(domain);
            return View(users);
        }

        public IActionResult GetUser(int id)
        {

            UserDetails user = _userService.GetUser(id, domain);

            if(user == null)
            {
                return BadRequest("no such user found");
            }


            return PartialView("_ModelDetails", user);
        }

        [HttpDelete]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                _userService.DeleteUser(id, domain);

                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public IActionResult CreateUser(UserDetails user)
        {

            UserDetails newUser = _userService.AddUser(user, domain);

            return PartialView("_userDetails", newUser);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _userService.Dispose();
        }

    }
}
