namespace aspnetmvcmethods.Contexts
{
    using aspnetmvcmethods.Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class UserContext : DbContext
    {
        public UserContext()
            : base("name=Model1") { }

        public DbSet<User> Users { get; set; }
    }
}