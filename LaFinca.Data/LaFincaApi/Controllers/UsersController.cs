using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaFincaApi.Models;
using LaFincaApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LaFincaApi.Controller
{

    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService service)
        {
            _userService = service;
        }

        public ActionResult<List<IUser>> ViewAll() =>
            _userService.Get();

        public ActionResult<IUser> ViewOne(string id)
        {
            var user = _userService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        public ActionResult<List<IUser>> Create(IUser user)
        {
            if(!_userService.DoesUserExist(username: user.username,email: user.email))
            {
            _userService.Create(user);

            }

            return ViewAll();
        }

        

        public IActionResult Update(string id, IUser user)
        {
            IUser foundUser = _userService.Get(user.username);

            if (foundUser == null)
            {
                return NotFound();
            }
            else
            {
                UpdateUser(foundUser, user);

                _userService.Update(id, foundUser);

            }


            return NoContent();
        }

        private void UpdateUser(IUser existingUser, IUser updatedUser)
        {
            if (existingUser.username != updatedUser.username) existingUser.username = updatedUser.username;
            if (existingUser.email != updatedUser.email) existingUser.email = updatedUser.email;
            if (existingUser.role != updatedUser.role) existingUser.role = updatedUser.role;
            if (existingUser.password != updatedUser.password) existingUser.password = updatedUser.password;

        }

        public IActionResult Delete(string id)
        {
            var user = _userService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            _userService.Remove(user.username);
            return NoContent();
        }

    }
}
