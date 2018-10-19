using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace TriviaCrestin.Models
{
    public class TalantContext : DbContext
    {
        public TalantContext() : base("name=DefaultConnection")
        {

        }
        public DbSet<Intrebare> Intrebari { set; get; }
        public DbSet<Referinta> Referinte { get; set; }
    }
}