using Microsoft.AspNet.Identity;
using Retrospectiva.Backend.Web.Models;
using Retrospectiva.Backend.Web.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Retrospectiva.Backend.Web.Controllers {
    [RoutePrefix("api/accounts")]
    public class AccountsController : ApiController {
        private AuthService _authService = null;

        public AccountsController()
        {
            _authService = new AuthService();
        }
 
        // POST api/Account/Register
        [AllowAnonymous]
        [Route("register")]
        public async Task<IHttpActionResult> Register(UserModel user){
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
 
            IdentityResult result = await _authService.RegisterUser(user.UserName, user.Password, user.Name); 
            IHttpActionResult errorResult = GetErrorResult(result);
 
            if (errorResult != null){
                return errorResult;
            } 
            return Ok();
        }

        // POST api/Account/Register
        [Authorize]
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get() {
            var users = _authService.GetAll();
            return Ok(users);
        }
 
        protected override void Dispose(bool disposing){
            if (disposing){
                _authService.Dispose();
            }
 
            base.Dispose(disposing);
        }
 
        private IHttpActionResult GetErrorResult(IdentityResult result){
            if (result == null){
                return InternalServerError();
            }
 
            if (!result.Succeeded){
                if (result.Errors != null){
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
 
                if (ModelState.IsValid){
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }
 
                return BadRequest(ModelState);
            } 
            return null;
        }

        public class UserModel {
            [Required]
            [Display(Name = "User name")]
            public string UserName { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name = "Name")]
            public string Name { get; set; }
        }
    } //class    
}