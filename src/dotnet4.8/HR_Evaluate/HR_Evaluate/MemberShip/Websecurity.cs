

//name//space HR_Evaluate.MemberShip
using HR_Evaluate.MemberShip;
using HR_Evaluate.Models;
namespace HR_Evaluate.Membership
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Routing;
    using System.Web.Security;

    /// <summary>
    /// 
    /// </summary>
    public sealed class WebSecurity
    {
        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        public static HttpContextBase Context
        {
            get { return new HttpContextWrapper(HttpContext.Current); }
        }

        /// <summary>
        /// Gets the request.
        /// </summary>
        /// <value>
        /// The request.
        /// </value>
        public static HttpRequestBase Request
        {
            get { return Context.Request; }
        }

        /// <summary>
        /// Gets the response.
        /// </summary>
        /// <value>
        /// The response.
        /// </value>
        public static HttpResponseBase Response
        {
            get { return Context.Response; }
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public static System.Security.Principal.IPrincipal User
        {
            get { return Context.User; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is authenticated.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is authenticated; otherwise, <c>false</c>.
        /// </value>
        public static bool IsAuthenticated
        {
            get { return User.Identity.IsAuthenticated; }
        }

        /// <summary>
        /// Gets the current user id.
        /// </summary>
        /// <value>
        /// The current user id.
        /// </value>
        public static int CurrentUserId
        {
            get
            {
                ShopMembershipProvider iszMembership = Membership.Provider as ShopMembershipProvider;

                if (iszMembership == null)
                {
                    return -1;
                }

                return iszMembership.CurrentUserId;
            }
        }

        /// <summary>
        /// Registers the specified username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="firstname">The firstname.</param>
        /// <param name="address">The lastname.</param>
        /// <param name="displayname">The displayname.</param>
        /// <param name="email">The email.</param>
        /// <param name="isApproved">if set to <c>true</c> [is approved].</param>
        /// <returns></returns>
        /// 
        //public static MembershipCreateStatus Register(string username, string password, string firstname, string lastname, string displayname, string email, bool isApproved)
        //{
        //    MembershipCreateStatus createStatus;
        //    Membership.CreateUser(username, password, email, null, null, isApproved, Guid.NewGuid(), out createStatus);

        //    if (createStatus == MembershipCreateStatus.Success)
        //    {
        //        if (isApproved)
        //        {n
        //            FormsAuthentication.SetAuthCookie(username, false);
        //        }
        //    }

        //    return createStatus;
        //}

        public static MembershipCreateStatus Register1(string username, string password, string fullname, string address,  int roleid, bool isApproved)
        {
            MembershipCreateStatus createStatus;
            Membership.CreateUser(username, password, null, null, null, isApproved, Guid.NewGuid(), out createStatus);

            if (createStatus == MembershipCreateStatus.Success)
            {
                //var db=new HrEvaluateDatacontext
                using (var db = new HrEvaluateDatacontext())
                {
                    var user = db.Logins.Single(item => item.UserName == username);
                    user.RoleID = 2;
                    db.SaveChanges();
                }

                if (isApproved)
                {
                    FormsAuthentication.SetAuthCookie(username, false);
                }
            }
            return createStatus;
        }

        /// <summary>
        /// Logins the specified username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="persistCookie">if set to <c>true</c> [persist cookie].</param>
        /// <returns></returns>
        public static bool Login(string username, string password, bool persistCookie = false)
        {
            bool success = Membership.ValidateUser(username, password);
            if (success)
            {
                FormsAuthentication.SetAuthCookie(username, persistCookie);
            }

            return success;
        }

        /// <summary>
        /// Logouts this instance.
        /// </summary>
        public static void Logout()
        {
            FormsAuthentication.SignOut();
        }

        /// <summary>
        /// Gets all roles.
        /// </summary>
        /// <returns></returns>
        public static List<Role> GetAllRoles()
        {
            using (HrEvaluateDatacontext model = new HrEvaluateDatacontext())
            {
                return model.Roles.ToList();
            }
        }

        /// <summary>
        /// Creates the role.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <param name="description">The description.</param>
        public static void CreateRole(string roleName, string description)
        {
            ShopRoleProvider iszRole = Roles.Provider as ShopRoleProvider;

            iszRole.CreateRole(roleName, description);
        }

        /// <summary>
        /// Roles the exists.
        /// </summary>
        /// <param name="roleId">The role id.</param>
        /// <returns></returns>
        public static bool RoleExists(int roleId)
        {
            using (var model = new HrEvaluateDatacontext())
            {
                return model.Roles.Any(item => item.Id == roleId);
            }
        }

        /// <summary>
        /// Gets the role.
        /// </summary>
        /// <param name="roleId">The role id.</param>
        /// <returns></returns>
        public static Role GetRole(int roleId)
        {
            using (var model = new HrEvaluateDatacontext())
            {
                return model.Roles.SingleOrDefault(item => item.Id == roleId);
            }
        }

        /// <summary>
        /// Gets the role.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <returns></returns>
        public static Role GetRole(string roleName)
        {
            using (var model = new HrEvaluateDatacontext())
            {
                return model.Roles.SingleOrDefault(item => item.RoleName == roleName);
            }
        }

        /// <summary>
        /// Edits the role.
        /// </summary>
        /// <param name="roleId">The role id.</param>
        /// <param name="newRoleName">New name of the role.</param>
        /// <returns></returns>
        public static bool EditRole(int roleId, string newRoleName)
        {
            using (var model = new HrEvaluateDatacontext())
            {
                Role role = GetRole(newRoleName);
                if (role == null || role.Id == roleId)
                {
                    role = model.Roles.SingleOrDefault(item => item.Id == roleId);

                    if (role != null)
                    {
                        role.RoleName = newRoleName;
                        model.SaveChanges();

                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Edits the role.
        /// </summary>
        /// <param name="roleId">The role id.</param>
        /// <param name="newRoleName">New name of the role.</param>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        public static bool EditRole(int roleId, string newRoleName, string description)
        {
            using (var model = new HrEvaluateDatacontext())
            {
                Role role = GetRole(newRoleName);
                if (role == null || role.Id == roleId)
                {
                    role = model.Roles.SingleOrDefault(item => item.Id == roleId);

                    if (role != null)
                    {
                        role.RoleName = newRoleName;
                        model.SaveChanges();

                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        public static MembershipUser GetUser(string username)
        {
            return Membership.GetUser(username);
        }

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="currentPassword">The current password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns></returns>
        public static bool ChangePassword(string userName, string currentPassword, string newPassword)
        {
            bool success = false;
            try
            {
                MembershipUser currentUser = Membership.GetUser(userName, true);
                success = currentUser.ChangePassword(currentPassword, newPassword);
            }
            catch (ArgumentException)
            {
            }

            return success;
        }

        /// <summary>
        /// Deletes the user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        public static bool DeleteUser(string username)
        {
            return Membership.DeleteUser(username);
        }

        /// <summary>
        /// Gets the user id.
        /// </summary>
        /// <param name="username">Name of the user.</param>
        /// <returns></returns>
        public static int GetUserId(string username)
        {
            ShopMembershipProvider iszMembership = Membership.Provider as ShopMembershipProvider;

            return iszMembership.GetUserId(username);
        }

        /// <summary>
        /// Creates the account.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="firstname">The firstname.</param>
        /// <param name="lastname">The lastname.</param>
        /// <param name="displayname">The displayname.</param>
        /// <param name="email">The email.</param>
        /// <param name="requireConfirmationToken">if set to <c>true</c> [require confirmation token].</param>
        /// <returns></returns>
        //public static string CreateAccount(string username, string password, string firstname, string lastname, string displayname, string email, bool requireConfirmationToken)
        //{
        //    ShopMembershipProvider iszMembership = Membership.Provider as ShopMembershipProvider;

        //    return iszMembership.CreateAccount(username, password, firstname, lastname, displayname, email, requireConfirmationToken);
        //}
        public static string CreateAccount(string username, string password, string fullname, string address, string phone, string email, bool requireConfirmationToken)
        {
            ShopMembershipProvider iszMembership = Membership.Provider as ShopMembershipProvider;

            return iszMembership.CreateAccount(username, password, fullname, address, phone, email, requireConfirmationToken);
        }


        /// <summary>
        /// Creates the account.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="firstname">The firstname.</param>
        /// <param name="lastname">The lastname.</param>
        /// <param name="displayname">The displayname.</param>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        //public static string CreateAccount(string username, string password, string firstname, string lastname, string displayname, string email)
        //{
        //    return CreateAccount(username, password, firstname, lastname, displayname, email, requireConfirmationToken: false);
        //}
        public static string CreateAccount(string username, string password, string fullname, string address, string phone, string email)
        {
            return CreateAccount(username, password, fullname, address, phone, email, requireConfirmationToken: false);
        }

        /// <summary>
        /// Finds the users by email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public static List<MembershipUser> FindUsersByEmail(string email, int pageIndex, int pageSize)
        {
            int totalRecords;

            return Membership.FindUsersByEmail(email, pageIndex, pageSize, out totalRecords).Cast<MembershipUser>().ToList();
        }

        /// <summary>
        /// Finds the name of the users by.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public static List<MembershipUser> FindUsersByName(string username, int pageIndex, int pageSize)
        {
            int totalRecords;

            return Membership.FindUsersByName(username, pageIndex, pageSize, out totalRecords).Cast<MembershipUser>().ToList();
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public static List<MembershipUser> GetAllUsers(int pageIndex, int pageSize)
        {
            int totalRecords;

            return Membership.GetAllUsers(pageIndex, pageSize, out totalRecords).Cast<MembershipUser>().ToList();
        }

        [Obsolete()]
        public static void InitializeDatabaseConnection(string connectionStringName, string userTableName, string userIdColumn, string userNameColumn, bool autoCreateTables)
        {
        }

        [Obsolete()]
        public static void InitializeDatabaseConnection(string connectionString, string providerName, string userTableName, string userIdColumn, string userNameColumn, bool autoCreateTables)
        {
        }
    }
}