using System;
using Singular.Web;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Singular.Web.Data;
using MELib.RO;
using MELib.Security;
using Singular;
using System.ComponentModel;
using Singular.Security;

namespace MEWeb.Profile
{
  public partial class Profile : MEPageBase<ProfileVM>
  {

  }
  public class ProfileVM : MEStatelessViewModel<ProfileVM>
  {
        public PagedDataManager<ProfileVM> UserListPageManager { get; set; }
        public ROUserPagedList.Criteria UserCriteria { get; set; }
        public ROUserPagedList UserList { get; set; }

        public MELib.Security.User EditingUser { get; set; }

        public MELib.RO.ROUser User { get; set; }
        public ROUserList Userlist { get; set; }

        public MELib.Accounts.AccountList UserAccountList { get; set; }
        public MELib.Accounts.Account UserAccount { get; set; }

        public MELib.AccountTypes.AccountTypes AccountTypes { get; set; }
        public MELib.AccountTypes.AccountTypesList AccountTypesList { get; set; }
        public ProfileVM()
    {
            this.UserListPageManager = new PagedDataManager<ProfileVM>((c) => c.UserList, (c) => c.UserCriteria, "UserName", 20);
            this.UserCriteria = new ROUserPagedList.Criteria();
            this.UserList = new ROUserPagedList();

        }
        protected override void Setup()
    {
      base.Setup();
            Userlist = MELib.RO.ROUserList.GetROUserList();
            User = Userlist.FirstOrDefault();
            UserAccountList = MELib.Accounts.AccountList.GetAccountList();
            UserAccount = UserAccountList.FirstOrDefault();

            AccountTypesList = MELib.AccountTypes.AccountTypesList.GetAccountTypesList();
            AccountTypes = AccountTypesList.FirstOrDefault();

        }

        [WebCallable]
        public static MELib.Security.User GetUser(int userId)
        {
            return MELib.Security.UserList.GetUserList(userId).First();
        }

        [WebCallable(Roles = new string[] { "Security.Manage Users" })]
        public static Result SaveUser(MELib.Security.User user)
        {
            if (user.SecurityGroupUserList.Count == 0)
            {
                //add a default security group of General User
                SecurityGroupUser securityGroupUser = SecurityGroupUser.NewSecurityGroupUser();
                securityGroupUser.SecurityGroupID = ROSecurityGroupList.GetROSecurityGroupList(true).FirstOrDefault(c => c.SecurityGroup == "General User")?.SecurityGroupID;
                user.SecurityGroupUserList.Add(securityGroupUser);
            }

            user.LoginName = user.EmailAddress;

            Result results = new Singular.Web.Result();
            Result Saveresults = user.SaveUser(user);
            MELib.Security.User SavedUser = (MELib.Security.User)Saveresults.Data;

            if (SavedUser != null)
            {
                results.Success = true;
                results.Data = SavedUser;
            }
            else
            {
                results.Success = false;
                results.ErrorText = Saveresults.ErrorText;
            }
            return results;
        }
        [WebCallable(Roles = new string[] { "Security.Manage Users" })]
        public static Result DeleteUser(int userId, Boolean RemoveAssociations)
        {
            Result results = new Singular.Web.Result();
            try
            {

                //get the user object and soft delete the user
                MELib.Security.UserList userList = MELib.Security.UserList.GetUserList(userId);
                userList.RemoveAt(0);
                userList.Save();

                results.Success = true;

            }
            catch (Exception ex)
            {
                results.ErrorText = ex.Message;
                results.Success = false;
            }

            return results;

        }

        [WebCallable(Roles = new string[] { "Security.Reset Passwords" })]
        public static void ResetPassword(string EmailAddress)
        {
            MELib.Security.User.ResetPassword(EmailAddress);
        }

    }
}

