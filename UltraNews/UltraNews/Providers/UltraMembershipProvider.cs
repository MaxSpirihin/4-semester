using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Helpers;
using System.Security.Cryptography;
using System.Web.WebPages;
using Microsoft.Internal.Web.Utils;
using UltraNews.Models;

namespace UltraNews.Providers
{
    public class UltraMembershipProvider : MembershipProvider
    {
        public override bool ValidateUser(string username, string password)
        {
            bool isValid = false;

            using (SqlCore _db = new SqlCore())
            {
                try
                {
                    User user = _db.Users.Where(u=>u.Login == username).FirstOrDefault();

                    if (user != null && Crypto.VerifyHashedPassword(user.Password, password))
                    {
                        isValid = true;
                    }
                }
                catch
                {
                    isValid = false;
                }
            }
            return isValid;
        }

        public MembershipUser CreateUser(string login, string password, string Name, string FamilyName, DateTime? BirthDay)
        {
            //ищем нет ли уже юзера с этим логином
            MembershipUser membershipUser = GetUser(login, false);

            if (membershipUser == null)
            {
                try
                {
                    using (SqlCore _db = new SqlCore())
                    {
                        //создаем юзера
                        User user = new User();
                        user.Login = login;
                        user.Password = Crypto.HashPassword(password);
                        user.CreationDate = DateTime.Now;
                        user.Name = Name;
                        user.FamilyName = FamilyName;
                        user.FullName = Name + " " + FamilyName;
                        user.BirthDay = BirthDay;

                        if (_db.Roles.Find(1) != null)
                        {
                            user.RoleId = 1;
                        }

                        _db.Users.Add(user);
                        _db.SaveChanges();
                        membershipUser = GetUser(login, false);
                        return membershipUser;
                    }
                }
                catch
                {
                    return null;
                }
            }
            //уже есть  юзер с этим логином
            return null;
        }

        public override MembershipUser GetUser(string email, bool userIsOnline)
        {
            try
            {
                using (SqlCore _db = new SqlCore())
                {
                    var users = from u in _db.Users
                                where u.Login == email
                                select u;
                    if (users.Count() > 0)
                    {
                        User user = users.First();
                        MembershipUser memberUser = new MembershipUser("MyMembershipProvider", user.Login, null, null, null, null,
                            false, false, user.CreationDate, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);
                        return memberUser;
                    }
                }
            }
            catch
            {
                return null;
            }
            return null;
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }
        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }
        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }
        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }
        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }
        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }
        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }
        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }
        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }
        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }
        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }
        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }
        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }
        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }
        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }
        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }
        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }
    }
}