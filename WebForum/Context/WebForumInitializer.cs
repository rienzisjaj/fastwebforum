using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebForum.Models;

namespace WebForum.Context
{
    public class WebForumInitializer : System.Data.Entity.CreateDatabaseIfNotExists<WebForumContext>
    {

    }
}