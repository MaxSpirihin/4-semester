using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UltraNews.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime CreationDate { get; set; }
        public string Name { get; set; }
        public string FamilyName { get; set; }
        public string FullName { get; set; }
        public DateTime? BirthDay { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}