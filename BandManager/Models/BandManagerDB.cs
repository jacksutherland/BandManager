using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace BandManager.Models
{
    public class BandManagerDB : DbContext
    {
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Show> Shows { get; set; }
    }
}