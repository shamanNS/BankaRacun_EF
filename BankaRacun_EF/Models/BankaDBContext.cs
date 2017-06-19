using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data;
using System.Linq;
using System.Web;

namespace BankaRacun_EF.Models
{
    public class BankaDBContext : DbContext
    {
        public virtual DbSet<Racun> Racuni { get; set; }
        public virtual DbSet<Uplatnica> Uplatnice { get; set; }

        public BankaDBContext():base("name=BankaDBContext")
        {

        }
    }
}