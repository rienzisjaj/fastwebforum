using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebForum.Context;
using WebForum.Models;

namespace WebForum.Controllers
{
    public class TopicController : Controller
    {
        private WebForumContext db = new WebForumContext();

        // GET: Topic
        public ActionResult Index()
        {
            return View(db.Topics.ToList());
        }


        public ActionResult MyTopics()
        {
            var user = User.Identity.Name;

            if (string.IsNullOrEmpty(user) && !User.Identity.IsAuthenticated)
            {
                return RedirectToAction("LogIn", "Account", null);
            }
            return View(db.Topics.Where(x => string.IsNullOrEmpty(user) || x.Creator == user).ToList());
        }

        // GET: Topic/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // GET: Topic/Create
        public ActionResult Create()
        {
            if(string.IsNullOrEmpty(User.Identity.Name) && !User.Identity.IsAuthenticated)
            {
                return RedirectToAction("LogIn", "Account", null);
            }

            return View();
        }

        // POST: Topic/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,CreationDate")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                var creator = User.Identity.Name;
                topic.Creator = creator;

                db.Topics.Add(topic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(topic);
        }

        // GET: Topic/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = User.Identity.Name;

            if (!string.IsNullOrEmpty(user) && User.Identity.IsAuthenticated)
            { 
                Topic topic = db.Topics.Find(id);
                if (topic == null)
                {
                    return HttpNotFound();
                }
                if (topic.Creator != user)
                {
                    return RedirectToAction("Index", "Topic", null);
                }
                return View(topic);
            }
            return RedirectToAction("Index", "Topic", null);
        }

        // POST: Topic/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,CreationDate,Creator")] Topic topic)
        {
            var user = User.Identity.Name;

            if (!string.IsNullOrEmpty(user) && User.Identity.IsAuthenticated && topic.Creator == user)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(topic).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(topic);
            }
            return RedirectToAction("LogIn", "Account", null);
        }

        // GET: Topic/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = User.Identity.Name;

            if (!string.IsNullOrEmpty(user) && User.Identity.IsAuthenticated)
            {
                Topic topic = db.Topics.Find(id);
                if (topic == null)
                {
                    return HttpNotFound();
                }
                if (topic.Creator != user)
                {
                    return RedirectToAction("Index", "Topic", null);
                }
                return View(topic);
            }
            return RedirectToAction("LogIn", "Account", null);
        }

        // POST: Topic/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var user = User.Identity.Name;
            Topic topic = db.Topics.Find(id);

            if (!string.IsNullOrEmpty(user) && User.Identity.IsAuthenticated && topic.Creator == user)
            {
                db.Topics.Remove(topic);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
