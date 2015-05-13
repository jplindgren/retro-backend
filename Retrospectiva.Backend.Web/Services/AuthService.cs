using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Retrospectiva.Backend.Web.Models;
using Retrospectiva.Backend.Web.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Retrospectiva.Backend.Web.Services {
    /// <summary>
    /// http://bitoftech.net/2014/06/01/token-based-authentication-asp-net-web-api-2-owin-asp-net-identity/

    /// </summary>
    public class AuthService : IDisposable{
        private RetroContext _ctx;

        private UserManager<ApplicationUser> _userManager;

        public AuthService(){
            _ctx = new RetroContext();
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_ctx));
        }
 
        public async Task<IdentityResult> RegisterUser(string username, string password, string name){
            ApplicationUser user = new ApplicationUser(username, name); 
            var result = await _userManager.CreateAsync(user, password); 
            return result;
        }
 
        public async Task<IdentityUser> FindUser(string userName, string password){
            IdentityUser user = await _userManager.FindAsync(userName, password); 
            return user;
        }

        public IEnumerable<IdentityUser> GetAll() {
            return _userManager.Users.ToList();
        }
 
        public void Dispose(){
            _ctx.Dispose();
            _userManager.Dispose(); 
        }
    } //class
}