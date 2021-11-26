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

namespace MEWeb.Account
{
  public partial class Home : MEPageBase<HomeVM>
  {
    protected void Page_Load(object sender, EventArgs e)
    {
    }
  }
  public class HomeVM : MEStatelessViewModel<HomeVM>
  {
        // Declare your page variables/properties here
    public PagedDataManager<HomeVM> UserListPageManager { get; set; }
    public ROUserPagedList.Criteria UserCriteria { get; set; }
        public bool FoundUserMoviesInd { get; set; }

    public string LoggedInUserName { get; set; }

    public MELib.Movies.UserMovieList UserMovieList { get; set; }

    public MELib.Accounts.AccountList UserAccountList { get; set; }
    public MELib.Accounts.Account UserAccount { get; set; }
   
    public MELib.AccountTypes.AccountTypes AccountTypes { get; set; }
    public MELib.AccountTypes.AccountTypesList AccountTypesList { get; set; }

    public ROUserPagedList UserList { get; set; }

    public MELib.Security.User EditingUser { get; set; }

    public MELib.RO.ROUser User { get; set; }
     public ROUserList Userlist { get; set; }

        public HomeVM()
    {
            this.UserListPageManager = new PagedDataManager<HomeVM>((c) => c.UserList, (c) => c.UserCriteria, "UserName", 20);
            this.UserCriteria = new ROUserPagedList.Criteria();
            this.UserList = new ROUserPagedList();

        }

    protected override void Setup()
    {
      base.Setup();

            // On page load initiate/set your data/variables and or properties here
            // Should pass in criteria for the specific user that is viewing the page, however using current identity
            Userlist = MELib.RO.ROUserList.GetROUserList();
            User = Userlist.FirstOrDefault();

            UserMovieList = MELib.Movies.UserMovieList.GetUserMovieList();

      UserAccountList = MELib.Accounts.AccountList.GetAccountList();
      UserAccount = UserAccountList.FirstOrDefault();

            AccountTypesList = MELib.AccountTypes.AccountTypesList.GetAccountTypesList();
            AccountTypes = AccountTypesList.FirstOrDefault();




      if (UserMovieList.Count() > 0)
      {
        FoundUserMoviesInd = true;
      }
      else
      {
        FoundUserMoviesInd = false;
      }


      LoggedInUserName = Singular.Security.Security.CurrentIdentity.UserName;

            this.ValidationDisplayMode = ValidationDisplayMode.Controls | ValidationDisplayMode.SubmitMessage;

            this.UserList = (ROUserPagedList)UserListPageManager.GetInitialData();
        }

        /// <summary>
        /// Gets the Security Group List (For drop down)
        /// </summary>
        [Browsable(false)]
        public SecurityGroupList SecurityGroupList
        {
            get
            {
                var ROSecurityRoles = MELib.Security.ROSecurityGroupList.GetROSecurityGroupList(true);

                var SecurityRoles = SecurityGroupList.GetSecurityGroupList();
                var clonedList = SecurityRoles.Clone();

                foreach (var item in SecurityRoles)
                {
                    if ((!ROSecurityRoles.Any(c => c.SecurityGroupID == item.SecurityGroupID)) || (item.SecurityGroup == "Administrator"))
                    {
                        clonedList.Remove(item);
                    }
                }

                SecurityRoles = clonedList;

                return SecurityRoles;
            }
        }

        // Place your page's WebCallable methods here

        // Example WebCallable Method called GetSomeData layout/structure

        /// <summary>
        /// This is a very basic example of a WebCallable method
        /// </summary>
        /// <param name="SomeReferenceID"></param>
        /// <returns></returns>
    [Singular.Web.WebCallable(LoggedInOnly = true)]
    public static Singular.Web.Result GetSomeData(int SomeReferenceID)
    {
      Result sr = new Result();
      try
      {
        // Perform some action here and return the result
        // sr.Data = "";
        sr.Success = true;
      }
      catch (Exception e)
      {
        sr.Data = e.InnerException;
        sr.Success = false;
      }
      return sr;
    }



        [WebCallable]
        public static MELib.Security.User GetUser(int userId)
        {
            return MELib.Security.UserList.GetUserList(userId).First();
        }

        /// <summary>
        /// Save changes to a user
        /// </summary>
        /// <param name="user">A user instance</param>
        /// <returns>The save result</returns>
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



