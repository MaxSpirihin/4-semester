using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UltraNews.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public int NewsId { get; set; }
        public string UserName { get; set; }
        public DateTime Date { get; set; }
    }
}