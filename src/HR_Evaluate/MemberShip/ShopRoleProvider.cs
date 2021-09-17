using HR_Evaluate.Models;

namespace HR_Evaluate.MemberShip
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Security;
    public class ShopRoleProvider : RoleProvider
    {
        /// <summary>
        /// Gets or sets the name of the application to store and retrieve role information for.
        /// </summary>
        /// <returns>The name of the application to store and retrieve role information for.</returns>
        public override string ApplicationName
        {
            get { return this.GetType().Assembly.GetName().Name; }
            set { this.ApplicationName = this.GetType().Assembly.GetName().Name; }
        }

        /// <summary>
        /// Gets the current user id.
        /// </summary>
        /// <value>
        /// The current user id.
        /// </value>
        public int CurrentUserId
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
        /// Gets a value indicating whether the specified user is in the specified role for the configured applicationName.
        /// </summary>
        /// <param name="username">The user name to search for.</param>
        /// <param name="roleName">The role to search in.</param>
        /// <returns>
        /// true if the specified user is in the specified role for the configured applicationName; otherwise, false.
        /// </returns>
        public override bool IsUserInRole(string username, string roleName)
        {
            if (string.IsNullOrEmpty(username))
            {
                return false;
            }

            if (string.IsNullOrEmpty(roleName))
            {
                return false;
            }

            using (HrEvaluateDatacontext model = new HrEvaluateDatacontext())
            {
                Login user = model.Logins.FirstOrDefault(item => item.UserName.Equals(username));
                if (user == null)
                {
                    return false;
                }

                Role role = user.Role;
                if (role == null || !role.RoleName.Equals(roleName))
                {
                    return false;
                }

                return true;
            }
        }

        /// <summary>
        /// Gets a list of the roles that a specified user is in for the configured applicationName.
        /// </summary>
        /// <param name="username">The user to return a list of roles for.</param>
        /// <returns>
        /// A string array containing the names of all the roles that the specified user is in for the configured applicationName.
        /// </returns>
        public override string[] GetRolesForUser(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return null;
            }

            using (HrEvaluateDatacontext model = new HrEvaluateDatacontext())
            {
                var user = model.Logins.SingleOrDefault(item => item.UserName == username);
                if (user != null)
                {
                    return new[] { user.Role.RoleName };
                }

                return null;
            }
        }

        /// <summary>
        /// Adds a new role to the data source for the configured applicationName.
        /// </summary>
        /// <param name="roleName">The name of the role to create.</param>
        public override void CreateRole(string roleName)
        {
            this.CreateRole(roleName, null);
        }

        /// <summary>
        /// Creates the role.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <param name="description">The description.</param>
        internal void CreateRole(string roleName, string description)
        {
            if (!string.IsNullOrEmpty(roleName))
            {
                using (HrEvaluateDatacontext model = new HrEvaluateDatacontext())
                {
                    Role role = model.Roles.FirstOrDefault(item => item.RoleName == roleName);
                    if (role == null)
                    {
                        DateTimeOffset currentTime = DateTimeOffset.Now;
                        role = new Role
                        {
                            RoleName = roleName
                        };

                        model.Roles.Add(role);
                        model.SaveChanges();
                    }
                }
            }
        }

        /// <summary>
        /// Removes a role from the data source for the configured applicationName.
        /// </summary>
        /// <param name="roleName">The name of the role to delete.</param>
        /// <param name="throwOnPopulatedRole">If true, throw an exception if <paramref name="roleName" /> has one or more members and do not delete <paramref name="roleName" />.</param>
        /// <returns>
        /// true if the role was successfully deleted; otherwise, false.
        /// </returns>
        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                return false;
            }

            using (HrEvaluateDatacontext model = new HrEvaluateDatacontext())
            {
                // Khong cho phep xoa role system
                Role role = model.Roles.FirstOrDefault(item => item.RoleName == roleName);

                if (role != null)
                {
                    model.Roles.Remove(role);
                    model.SaveChanges();

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets a value indicating whether the specified role name already exists in the role data source for the configured applicationName.
        /// </summary>
        /// <param name="roleName">The name of the role to search for in the data source.</param>
        /// <returns>
        /// true if the role name already exists in the data source for the configured applicationName; otherwise, false.
        /// </returns>
        public override bool RoleExists(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                return false;
            }

            using (HrEvaluateDatacontext model = new HrEvaluateDatacontext())
            {
                Role role = model.Roles.FirstOrDefault(item => item.RoleName == roleName);
                if (role != null)
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Adds the specified user names to the specified roles for the configured applicationName.
        /// </summary>
        /// <param name="usernames">A string array of user names to be added to the specified roles.</param>
        /// <param name="roleNames">A string array of the role names to add the specified user names to.</param>
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {

        }

        /// <summary>
        /// Removes the specified user names from the specified roles for the configured applicationName.
        /// </summary>
        /// <param name="usernames">A string array of user names to be removed from the specified roles.</param>
        /// <param name="roleNames">A string array of role names to remove the specified user names from.</param>
        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
        }

        /// <summary>
        /// Gets a list of users in the specified role for the configured applicationName.
        /// </summary>
        /// <param name="roleName">The name of the role to get the list of users for.</param>
        /// <returns>
        /// A string array containing the names of all the users who are members of the specified role for the configured applicationName.
        /// </returns>
        public override string[] GetUsersInRole(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                return null;
            }

            using (HrEvaluateDatacontext model = new HrEvaluateDatacontext())
            {
                Role role = model.Roles.FirstOrDefault(item => item.RoleName == roleName);
                if (role != null)
                {
                    return ((Role)role).Logins.Select(item => item.UserName).ToArray();
                }

                return null;
            }
        }

        /// <summary>
        /// Gets a list of all the roles for the configured applicationName.
        /// </summary>
        /// <returns>
        /// A string array containing the names of all the roles stored in the data source for the configured applicationName.
        /// </returns>
        public override string[] GetAllRoles()
        {
            using (HrEvaluateDatacontext model = new HrEvaluateDatacontext())
            {
                return model.Roles.Select(item => item.RoleName).ToArray();
            }
        }

        /// <summary>
        /// Gets an array of user names in a role where the user name contains the specified user name to match.
        /// </summary>
        /// <param name="roleName">The role to search in.</param>
        /// <param name="usernameToMatch">The user name to search for.</param>
        /// <returns>
        /// A string array containing the names of all the users where the user name matches <paramref name="usernameToMatch" /> and the user is a member of the specified role.
        /// </returns>
        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            return null;
            ;
        }
    }
}