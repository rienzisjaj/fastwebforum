using System.Data.Entity;
using WebForum.Models;

namespace WebForum.Context
{
    public class WebForumContext : DbContext
    {
        public WebForumContext() : base("WebForumContext")
        {

        }

        public DbSet<Topic> Topics { get; set; }

    }
}