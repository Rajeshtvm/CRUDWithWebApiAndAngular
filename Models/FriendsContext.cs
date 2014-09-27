using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CRUDWithWebApiAndAngular.Models
{
    public class FriendsContext : DbContext
    {
        public FriendsContext()
            : base("name-DefaultConnection")
        {
            base.Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Friend> Friends { get; set; }
    }
}