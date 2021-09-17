using System;
using System.IO;

namespace HRE.Core
{
    /// <summary>
    /// Central point for application version.
    /// </summary>
    public class AppVersionHelper
    {
        /// <summary>
        /// Gets current version of the web application.
        /// It's also shown in the web page.
        /// </summary>
        public const string Version = "3.0.0";

        /// <summary>
        /// Gets release (last build) date of the web application.
        /// It's shown in the web page.
        /// </summary>
        public static DateTime ReleaseDate => new FileInfo(typeof(AppVersionHelper).Assembly.Location).LastWriteTime;
    }
}