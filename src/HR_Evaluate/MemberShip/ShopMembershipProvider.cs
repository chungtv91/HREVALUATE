using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//namespace HR_Evaluate.MemberShip
using HR_Evaluate.Models;

namespace HR_Evaluate.MemberShip
{

    using System;
    using System.Collections.Generic;
    using System.Configuration.Provider;
    using System.Data;
    using System.Data.SqlClient;
    using System.Globalization;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Configuration;
    using System.Web.Helpers;
    using System.Web.Security;


    public class ShopMembershipProvider : MembershipProvider
    {
        internal const int SALT_SIZE = 16;
        private bool _EnablePasswordRetrieval;
        private bool _EnablePasswordReset;
        private bool _RequiresQuestionAndAnswer;
        private string _AppName;
        private bool _RequiresUniqueEmail;
        private int _MaxInvalidPasswordAttempts;
        private int _PasswordAttemptWindow;
        private int _MinRequiredPasswordLength;
        private int _MinRequiredNonalphanumericCharacters;
        private string _PasswordStrengthRegularExpression;
        private MembershipPasswordFormat _PasswordFormat;
        private const int TokenSizeInBytes = 16;

        // public int CurrentUserId;

        public override bool EnablePasswordRetrieval
        {
            get
            {
                return this._EnablePasswordRetrieval;
            }
        }


        public override bool EnablePasswordReset
        {
            get
            {
                return this._EnablePasswordReset;
            }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get
            {
                return this._RequiresQuestionAndAnswer;
            }
        }

        public override string ApplicationName
        {
            get
            {
                return this._AppName;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ProviderException("Provider Application Name is not null.");
                }

                if (value.Length > 256)
                {
                    throw new ProviderException("Provider_application_name_too_long");
                }

                this._AppName = value;
            }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get
            {
                return this._MaxInvalidPasswordAttempts;
            }
        }

        public override int PasswordAttemptWindow
        {
            get
            {
                return this._PasswordAttemptWindow;
            }
        }


        public override bool RequiresUniqueEmail
        {
            get
            {
                return this._RequiresUniqueEmail;
            }
        }


        public override MembershipPasswordFormat PasswordFormat
        {
            get
            {
                return this._PasswordFormat;
            }
        }


        public override int MinRequiredPasswordLength
        {
            get
            {
                return this._MinRequiredPasswordLength;
            }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get
            {
                return this._MinRequiredNonalphanumericCharacters;
            }
        }

        public override string PasswordStrengthRegularExpression
        {
            get
            {
                return this._PasswordStrengthRegularExpression;
            }
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
                if (HttpContext.Current.User != null)
                {
                    var identity = HttpContext.Current.User.Identity;
                    if (identity.IsAuthenticated)
                    {
                        return this.GetUserId(identity.Name);
                    }
                }

                return -1;
            }
        }


        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            // HttpRuntime.CheckAspNetHostingPermission(AspNetHostingPermissionLevel.Low, "Feature_not_supported_at_this_level");
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }

            if (string.IsNullOrEmpty(name))
            {
                name = "IszMembershipProvider";
            }

            if (string.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Thiết lập cấu hình bởi :) ");
            }

            base.Initialize(name, config);
            this._EnablePasswordRetrieval = SecUtility.GetBooleanValue(config, "enablePasswordRetrieval", false);
            this._EnablePasswordReset = SecUtility.GetBooleanValue(config, "enablePasswordReset", true);
            this._RequiresQuestionAndAnswer = SecUtility.GetBooleanValue(config, "requiresQuestionAndAnswer", true);
            this._RequiresUniqueEmail = SecUtility.GetBooleanValue(config, "requiresUniqueEmail", true);
            this._MaxInvalidPasswordAttempts = SecUtility.GetIntValue(config, "maxInvalidPasswordAttempts", 5, false, 0);
            this._PasswordAttemptWindow = SecUtility.GetIntValue(config, "passwordAttemptWindow", 10, false, 0);
            //this._MinRequiredPasswordLength = SecUtility.GetIntValue(config, "minRequiredPasswordLength", 7, false, 128);
            this._MinRequiredPasswordLength = SecUtility.GetIntValue(config, "minRequiredPasswordLength", 6, false, 128);
            this._MinRequiredNonalphanumericCharacters = SecUtility.GetIntValue(config, "minRequiredNonalphanumericCharacters", 1, true, 128);
            this._PasswordStrengthRegularExpression = config["passwordStrengthRegularExpression"];
            if (this._PasswordStrengthRegularExpression != null)
            {
                this._PasswordStrengthRegularExpression = this._PasswordStrengthRegularExpression.Trim();
                if (this._PasswordStrengthRegularExpression.Length != 0)
                {
                    try
                    {
                        Regex regex = new Regex(this._PasswordStrengthRegularExpression);
                    }
                    catch (ArgumentException ex)
                    {
                        throw new ProviderException(ex.Message, ex);
                    }
                }
            }
            else
            {
                this._PasswordStrengthRegularExpression = string.Empty;
            }

            if (this._MinRequiredNonalphanumericCharacters > this._MinRequiredPasswordLength)
            {
                throw new HttpException("MinRequiredNonalphanumericCharacters_can_not_be_more_than_MinRequiredPasswordLength");
            }

            this._AppName = config["applicationName"];
            if (string.IsNullOrEmpty(this._AppName))
            {
                this._AppName = SecUtility.GetDefaultAppName();
            }

            if (this._AppName.Length > 256)
            {
                throw new ProviderException("Provider_application_name_too_long");
            }

            switch (config["passwordFormat"] ?? "Hashed")
            {
                case "Clear":
                    this._PasswordFormat = MembershipPasswordFormat.Clear;
                    break;
                case "Encrypted":
                    this._PasswordFormat = MembershipPasswordFormat.Encrypted;
                    break;
                case "Hashed":
                    this._PasswordFormat = MembershipPasswordFormat.Hashed;
                    break;
                default:
                    throw new ProviderException("Provider_bad_password_format");
            }

            if (this.PasswordFormat == MembershipPasswordFormat.Hashed && this.EnablePasswordRetrieval)
            {
                throw new ProviderException("Provider_can_not_retrieve_hashed_password");
            }

            config.Remove("connectionStringName");
            config.Remove("connectionString");
            config.Remove("enablePasswordRetrieval");
            config.Remove("enablePasswordReset");
            config.Remove("requiresQuestionAndAnswer");
            config.Remove("applicationName");
            config.Remove("requiresUniqueEmail");
            config.Remove("maxInvalidPasswordAttempts");
            config.Remove("passwordAttemptWindow");
            config.Remove("commandTimeout");
            config.Remove("passwordFormat");
            config.Remove("name");
            config.Remove("minRequiredPasswordLength");
            config.Remove("minRequiredNonalphanumericCharacters");
            config.Remove("passwordStrengthRegularExpression");
            config.Remove("passwordCompatMode");
            if (config.Count <= 0)
            {
                return;
            }

            string key = config.GetKey(0);
            if (string.IsNullOrEmpty(key))
            {
                return;
            }

            throw new ProviderException("Provider_unrecognized_attribute");
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            if (string.IsNullOrEmpty(username))
            {
                status = MembershipCreateStatus.InvalidUserName;

                return null;
            }

            if (string.IsNullOrEmpty(password))
            {
                status = MembershipCreateStatus.InvalidPassword;

                return null;
            }

            string hashedPassword = Crypto.HashPassword(password);
            if (hashedPassword.Length > 128)
            {
                status = MembershipCreateStatus.InvalidPassword;

                return null;
            }

            using (var model = new HrEvaluateDatacontext())
            {
                if (model.Logins.Any(item => item.UserName.Equals(username)))
                {
                    status = MembershipCreateStatus.DuplicateUserName;

                    return null;
                }

                DateTimeOffset currentTime = DateTimeOffset.Now;
                int currentUserId = CurrentUserId;
                Login user = new Login
                {
                    UserName = username,
                    Password = hashedPassword,
                    //FullName =
                    //Email = email,

                    //NgayTao = DateTime.Now,
                    //MaQuyen = 2, // TODO: set role
                    //MaNhanVien = null,
                    //KichHoat = true
                };

                model.Logins.Add(user);
                model.SaveChanges();
                status = MembershipCreateStatus.Success;

                return new MembershipUser(Membership.Provider.Name, user.UserName, user.Password, "member@gmail.com", null, null, true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.MaxValue);
            }
        }

        public string CreateAccount(string username, string password, string firstname, string lastname, string displayname, string email, bool requireConfirmationToken)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new MembershipCreateUserException(MembershipCreateStatus.InvalidUserName);
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new MembershipCreateUserException(MembershipCreateStatus.InvalidPassword);
            }

            if (string.IsNullOrEmpty(email))
            {
                throw new MembershipCreateUserException(MembershipCreateStatus.InvalidEmail);
            }

            string hashedPassword = Crypto.HashPassword(password);
            if (hashedPassword.Length > 128)
            {
                throw new MembershipCreateUserException(MembershipCreateStatus.InvalidPassword);
            }

            using (var model = new HrEvaluateDatacontext())
            {
                if (model.Logins.Any(item => item.UserName.Equals(username)))
                {
                    throw new MembershipCreateUserException(MembershipCreateStatus.DuplicateUserName);
                }

                string token = string.Empty;
                if (requireConfirmationToken)
                {
                    token = GenerateToken();
                }

                DateTimeOffset currentTime = DateTimeOffset.Now;
                int currentUserId = CurrentUserId;
                Login user = new Login
                {
                    UserName = username,
                    Password = hashedPassword,
                    //NgayTao = DateTime.Now,
                    //MaQuyen = 2, // TODO: set role
                    //MaNhanVien = null,
                    // KichHoat = true
                };

                model.Logins.Add(user);
                model.SaveChanges();

                return token;
            }
        }


        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotSupportedException("Consider using methods from WebSecurity module.");
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotSupportedException("Consider using methods from WebSecurity module.");
        }


        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            if (string.IsNullOrEmpty(username))
            {
                return false;
            }

            if (string.IsNullOrEmpty(oldPassword))
            {
                return false;
            }

            if (string.IsNullOrEmpty(newPassword))
            {
                return false;
            }

            using (var model = new HrEvaluateDatacontext())
            {
                Login user = model.Logins.FirstOrDefault(item => item.UserName == username);
                if (user == null)
                {
                    return false;
                }

                string hashedPassword = user.Password;
                bool verificationSucceeded = hashedPassword != null && Crypto.VerifyHashedPassword(hashedPassword, oldPassword);
                if (!verificationSucceeded)
                {
                    return false;
                }

                string newHashedPassword = Crypto.HashPassword(newPassword);
                if (newHashedPassword.Length > 128)
                {
                    return false;
                }

                user.Password = newHashedPassword;
                model.SaveChanges();

                return true;
            }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotSupportedException("Consider using methods from WebSecurity module.");
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotSupportedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                return false;
            }

            if (string.IsNullOrEmpty(password))
            {
                return false;
            }

            return Membership.ValidateUser(username, password);
            //using (var model = new HrEvaluateDatacontext())
            //{
            //    Login user = model.Logins.FirstOrDefault(item => item.UserName == username);
            //    if (user == null)
            //    {
            //        return false;
            //    }

            //    string hashedPassword = user.Password;

            //    bool verificationSucceeded = hashedPassword != null && Crypto.VerifyHashedPassword(hashedPassword, password);

            //    if (verificationSucceeded)
            //    {
            //        return true;
            //    }

            //    return false;
            //}
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            return null;
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            if (string.IsNullOrEmpty(username))
            {
                return null;
            }

            using (HrEvaluateDatacontext model = new HrEvaluateDatacontext())
            {
                Login user = model.Logins.FirstOrDefault(item => item.UserName == username);
                if (user != null)
                {

                    return new MembershipUser(Membership.Provider.Name, user.UserName, user.UserName, "member@gmail.com", null, null, true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.MinValue, DateTime.MinValue);
                }

                return null;
            }
        }

        public override string GetUserNameByEmail(string email)
        {

            return string.Empty;
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            if (string.IsNullOrEmpty(username))
            {
                return false;
            }
            using (HrEvaluateDatacontext model = new HrEvaluateDatacontext())
            {
                Login user = model.Logins.FirstOrDefault(item => item.UserName == username);
                if (user != null)
                {
                    model.Logins.Remove(user);
                    model.SaveChanges();

                    return true;
                }

                return false;
            }
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            MembershipUserCollection membershipUsers = new MembershipUserCollection();
            totalRecords = 0;

            return membershipUsers;
        }

        public override int GetNumberOfUsersOnline()
        {
            return 1;
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            MembershipUserCollection membershipUsers = new MembershipUserCollection();
            totalRecords = 0;

            return membershipUsers;
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            MembershipUserCollection membershipUsers = new MembershipUserCollection();
            totalRecords = 0;

            return membershipUsers;
        }

        /// <summary>
        /// Gets the user id.
        /// </summary>
        /// <param name="username">Name of the user.</param>
        /// <returns></returns>
        public int GetUserId(string username)
        {
            MembershipUser user = Membership.GetUser(username);

            return (int)user.ProviderUserKey;
        }

        private static string GenerateToken()
        {
            using (var prng = new RNGCryptoServiceProvider())
            {
                return GenerateToken(prng);
            }
        }

        internal static string GenerateToken(RandomNumberGenerator generator)
        {
            byte[] tokenBytes = new byte[TokenSizeInBytes];
            generator.GetBytes(tokenBytes);

            return HttpServerUtility.UrlTokenEncode(tokenBytes);
        }



    }
}