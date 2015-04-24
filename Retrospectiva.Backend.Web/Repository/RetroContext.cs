﻿using Retrospectiva.Backend.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Retrospectiva.Backend.Web.Repository {
    public class RetroContext : DbContext{
        public RetroContext()
            : base("DefaultConnection") {
                Database.SetInitializer<RetroContext>(new DbInitializer());
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Sprint> Sprints { get; set; }
        public DbSet<Member> Members { get; set; }
    } //cass
}