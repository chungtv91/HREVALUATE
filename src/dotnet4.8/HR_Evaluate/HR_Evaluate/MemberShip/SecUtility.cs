//namespace HR_Evaluate.MemberShip
namespace HR_Evaluate.MemberShip
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Configuration.Provider;
    using System.Data;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Web;
    using System.Web.DataAccess;
    using System.Web.Hosting;

    internal class SecUtility
    {
        public static string GetDefaultAppName()
        {
            try
            {
                string str = HostingEnvironment.ApplicationVirtualPath;
                if (string.IsNullOrEmpty(str))
                {
                    str = Process.GetCurrentProcess().MainModule.ModuleName;
                    int startIndex = str.IndexOf('.');
                    if (startIndex != -1)
                    {
                        str = str.Remove(startIndex);
                    }
                }

                if (string.IsNullOrEmpty(str))
                {
                    return "/";
                }

                return str;
            }
            catch
            {
                return "/";
            }
        }

        public static string GetConnectionString(NameValueCollection config)
        {
            string str = config["connectionString"];
            if (!string.IsNullOrEmpty(str))
            {
                return str;
            }

            string specifiedConnectionString = config["connectionStringName"];

            ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings[specifiedConnectionString];
            string connectionString = connectionStringSettings.ConnectionString;
            if (!string.IsNullOrEmpty(connectionString))
            {
                return connectionString;
            }

            throw new ProviderException("Connection_string_not_found");
        }

        public static bool ValidatePasswordParameter(ref string param, int maxSize)
        {
            return param != null && param.Length >= 1 && (maxSize <= 0 || param.Length <= maxSize);
        }

        public static bool ValidateParameter(ref string param, bool checkForNull, bool checkIfEmpty, bool checkForCommas, int maxSize)
        {
            if (param == null)
            {
                return !checkForNull;
            }

            param = param.Trim();

            return (!checkIfEmpty || param.Length >= 1) && (maxSize <= 0 || param.Length <= maxSize) && (!checkForCommas || !param.Contains(","));
        }

        public static void CheckPasswordParameter(ref string param, int maxSize, string paramName)
        {
            if (param == null)
            {
                throw new ArgumentNullException(paramName);
            }

            if (param.Length < 1)
            {
                throw new ArgumentException("Parameter_can_not_be_empty", paramName);
            }

            if (maxSize <= 0 || param.Length <= maxSize)
            {
                return;
            }

            throw new ArgumentException("Parameter_too_long", paramName);
        }

        internal static void CheckParameter(ref string param, bool checkForNull, bool checkIfEmpty, bool checkForCommas, int maxSize, string paramName)
        {
            if (param == null)
            {
                if (checkForNull)
                {
                    throw new ArgumentNullException(paramName);
                }
            }
            else
            {
                param = param.Trim();
                if (checkIfEmpty && param.Length < 1)
                {
                    throw new ArgumentException("Parameter_can_not_be_empty", paramName);
                }

                if (maxSize > 0 && param.Length > maxSize)
                {
                    throw new ArgumentException("Parameter_too_long", paramName);
                }

                if (!checkForCommas || !param.Contains(","))
                {
                    return;
                }

                throw new ArgumentException("Parameter_can_not_contain_comma", paramName);
            }
        }

        public static void CheckArrayParameter(ref string[] param, bool checkForNull, bool checkIfEmpty, bool checkForCommas, int maxSize, string paramName)
        {
            if (param == null)
            {
                throw new ArgumentNullException(paramName);
            }

            if (param.Length < 1)
            {
                throw new ArgumentException("Parameter_array_empty", paramName);
            }

            Hashtable hashtable = new Hashtable(param.Length);
            for (int index = param.Length - 1; index >= 0; --index)
            {
                CheckParameter(ref param[index], checkForNull, checkIfEmpty, checkForCommas, maxSize, paramName + "[ " + index.ToString((IFormatProvider)CultureInfo.InvariantCulture) + " ]");
                if (hashtable.Contains((object)param[index]))
                {
                    throw new ArgumentException("Parameter_duplicate_array_element", paramName);
                }

                hashtable.Add((object)param[index], (object)param[index]);
            }
        }

        public static bool GetBooleanValue(NameValueCollection config, string valueName, bool defaultValue)
        {
            string str = config[valueName];
            if (str == null)
            {
                return defaultValue;
            }

            bool result;
            if (bool.TryParse(str, out result))
            {
                return result;
            }

            throw new ProviderException("Value_must_be_boolean");
        }

        public static int GetIntValue(NameValueCollection config, string valueName, int defaultValue, bool zeroAllowed, int maxValueAllowed)
        {
            string s = config[valueName];
            if (s == null)
            {
                return defaultValue;
            }

            int result;
            if (!int.TryParse(s, out result))
            {
                if (zeroAllowed)
                {
                    throw new ProviderException("Value_must_be_non_negative_integer");
                }

                throw new ProviderException("Value_must_be_positive_integer");
            }

            if (zeroAllowed && result < 0)
            {
                throw new ProviderException("Value_must_be_non_negative_integer");
            }

            if (!zeroAllowed && result <= 0)
            {
                throw new ProviderException("Value_must_be_positive_integer");
            }

            if (maxValueAllowed <= 0 || result <= maxValueAllowed)
            {
                return result;
            }

            throw new ProviderException("Value_too_big");
        }

        public static void CheckSchemaVersion(ProviderBase provider, SqlConnection connection, string[] features, string version, ref int schemaVersionCheck)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }

            if (features == null)
            {
                throw new ArgumentNullException("features");
            }

            if (version == null)
            {
                throw new ArgumentNullException("version");
            }

            if (schemaVersionCheck == -1)
            {
                throw new ProviderException("Provider_Schema_Version_Not_Match");
            }

            if (schemaVersionCheck != 0)
            {
                return;
            }

            lock (provider)
            {
                if (schemaVersionCheck == -1)
                {
                    throw new ProviderException("Provider_Schema_Version_Not_Match");
                }

                if (schemaVersionCheck != 0)
                {
                    return;
                }

                foreach (string item_0 in features)
                {
                    SqlCommand local_1_1 = new SqlCommand("dbo.aspnet_CheckSchemaVersion", connection);
                    local_1_1.CommandType = CommandType.StoredProcedure;
                    SqlParameter local_2_1 = new SqlParameter("@Feature", (object)item_0);
                    local_1_1.Parameters.Add(local_2_1);
                    SqlParameter local_2_2 = new SqlParameter("@CompatibleSchemaVersion", (object)version);
                    local_1_1.Parameters.Add(local_2_2);
                    SqlParameter local_2_3 = new SqlParameter("@ReturnValue", SqlDbType.Int);
                    local_2_3.Direction = ParameterDirection.ReturnValue;
                    local_1_1.Parameters.Add(local_2_3);
                    local_1_1.ExecuteNonQuery();
                    if ((local_2_3.Value != null ? (int)local_2_3.Value : -1) != 0)
                    {
                        schemaVersionCheck = -1;

                        throw new ProviderException("Provider_Schema_Version_Not_Match");
                    }
                }

                schemaVersionCheck = 1;
            }
        }
    }


}