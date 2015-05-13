using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Retrospectiva.Backend.Web.Models {
    public class ApplicationUser : IdentityUser {
        public ApplicationUser() {
        }

        public ApplicationUser(string username, string name) {
            this.UserName = username;
            this.Name = name;
        }
        public string Name { get; set; }
    } //class
}