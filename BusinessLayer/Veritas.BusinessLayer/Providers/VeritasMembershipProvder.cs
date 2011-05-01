using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using Veritas.DataLayer;
using Veritas.BusinessLayer.Caching;
using Veritas.DataLayer.Models;

namespace Veritas.BusinessLayer.Providers
{
    public class VeritasMembershipProvider : MembershipProvider
    {
        VeritasRepository repo = VeritasRepository.GetInstance();

        #region overriden methods

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {            
            return repo.ChangePassword(CacheHandler.BlogConfigId, username, oldPassword, newPassword);
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            //This method was left unimplmented on purpose, we require different information then the inherited MembershipProvider
            throw new NotImplementedException();
        }

        public VeritasMembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, bool isAdmin, string about, out MembershipCreateStatus status)
        {            
            BlogUser user = repo.GetBlogUserByUserName(CacheHandler.BlogConfigId, username);
            if (user != null)
            {
                status = MembershipCreateStatus.DuplicateUserName;
                return null;
            }
            user = repo.GetBlogUserByEmail(CacheHandler.BlogConfigId, email);
            if (user != null)
            {
                status = MembershipCreateStatus.DuplicateEmail;
                return null;
            }


            user = new BlogUser();
            user.BlogConfigId = CacheHandler.BlogConfigId;
            user.Username = username;
            user.Password = password;
            user.EmailAddress = email;
            //author.IsAdmin = isAdmin;
            user.About = about;
            repo.Add(user);
            repo.Save();
            

            VeritasMembershipUser vmUser = new VeritasMembershipUser();
            vmUser.Email = email;
            vmUser.CreationDate = DateTime.Now;
            vmUser.UserName = username;
            vmUser.About = about;
            vmUser.IsLockedOut = false;
            vmUser.IsAdmin = isAdmin;

            status = MembershipCreateStatus.Success;
            return vmUser;
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        protected override byte[] DecryptPassword(byte[] encodedPassword)
        {
            return base.DecryptPassword(encodedPassword);
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        protected override byte[] EncryptPassword(byte[] password)
        {
            return base.EncryptPassword(password);
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

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
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

        public override bool ValidateUser(string username, string password)
        {
            return repo.ValidateUser(CacheHandler.BlogConfigId, username, password);
        }

        #endregion

        #region overridden properties

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
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

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}
