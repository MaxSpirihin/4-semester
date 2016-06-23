using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UltraNews.Models;

namespace UltraNews.Controllers
{
    public class NewsController : Controller
    {

        private SqlCore db = new SqlCore();

        public ViewResult List()
        {
            return View(db.News);
        }


        public ActionResult Show(int id)
        {
            News news = db.News.Where(n => n.Id == id).FirstOrDefault();
            if (news == null)
                return HttpNotFound();

            ViewBag.News = news;
            IEnumerable<Comment> comments = db.Comments.Where(c => c.NewsId == id);
            return View(comments);

        }


        public string Test()
        {
            User u = db.Users.FirstOrDefault();
            return u.FamilyName;
        }

        [Authorize]
        public ActionResult AddComment(string Text, int NewsId)
        {
            if (Text.Length < 2)
                return PartialView("CommentsView", db.Comments);

            
            User user = db.Users.Where(x=>x.Login == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault();
            
            Comment comment = new Comment();
            comment.Date = DateTime.Now;
            comment.NewsId = NewsId;
            comment.Text = Text;
            comment.UserId = user.Id;
            comment.UserName = user.FullName;
            db.Comments.Add(comment);
            db.SaveChanges();

            return PartialView("CommentsView", db.Comments);
        }

    }
}
