using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HRE.Core.DI;
using HRE.Core.Extensions;
using HRE.Core.Identity;
using HRE.Web.UI.Seeds.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Syncfusion.XlsIO;

namespace HRE.Web.UI.Seeds
{
    public class UserSeed : BaseSeed, ITransientDependency
    {
        private readonly ILogger _logger;
        private readonly CustomRoleManager _roleManager;
        private readonly CustomUserManager _userManager;

        public UserSeed(ILogger<UserSeed> logger, CustomRoleManager roleManager, CustomUserManager userManager)
        {
            _logger = logger;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public override async Task RunAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("UserSeed started.");
            var users = await _userManager.Users.ToListAsync(cancellationToken);
            if (users.Count > 0) return;

            var records = await ReadExcelTemplateAs(cancellationToken);
            var roles = await _roleManager.Roles.Select(x => new { Id = x.Id, Name = x.Name }).ToListAsync(cancellationToken);
            foreach (var record in records)
            {
                var user = users.SingleOrDefault(x => string.Equals(x.UserName, record.UserName, StringComparison.OrdinalIgnoreCase));

                if (user == null)
                {
                    user = new()
                    {
                        UserName = record.UserName,
                        FullName = record.FullName,
                        Email = record.Email,
                        IsDisabled = false
                    };
                    var userResult = await _userManager.CreateAsync(user, record.Password);
                    if (!string.IsNullOrWhiteSpace(record.RoleName))
                    {
                        var role = roles.Find(x => string.Equals(x.Name, record.RoleName, StringComparison.OrdinalIgnoreCase));
                        var roleResult = await _userManager.AddToRoleAsync(user, role.Name);
                    }
                }
                else
                {
                    //user.FullName = record.FullName;
                    //user.Email = record.Email;
                    //var updateResult = await _userManager.UpdateAsync(user);
                }
            }

            _logger.LogInformation("UserSeed success.");
        }

        private async Task<List<UserInitModel>> ReadExcelTemplateAs(CancellationToken cancellationToken)
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, "Seeds", "Data", Template);
            var items = Activator.CreateInstance<List<UserInitModel>>();
            if (!File.Exists(filePath)) return items;

            // Work with Excel.

            // https://help.syncfusion.com/file-formats/xlsio/getting-started-create-excel-file-csharp-vbnet
            // https://help.syncfusion.com/file-formats/xlsio/create-read-edit-excel-files-in-asp-net-core-c-sharp
            using ExcelEngine excelEngine = new();
            //Instantiate the Excel application object
            IApplication excelApp = excelEngine.Excel;

            //Assigns default application version
            excelApp.DefaultVersion = ExcelVersion.Xlsx;

            //A existing workbook is opened.
            await using FileStream xlsxFile = new(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            IWorkbook workbook = excelApp.Workbooks.Open(xlsxFile);

            //Access first worksheet from the workbook.
            IWorksheet worksheet = workbook.Worksheets.FirstOrDefault(x => x.Name == "Users");
            if (worksheet != null)
            {
                var usedRange = worksheet.UsedRange;
                var lastRow = usedRange.LastRow;
                for (int row = 2; row <= lastRow; row++)
                {
                    var col = 1;
                    var no = worksheet[row, col++].DisplayText.AsInt32();
                    var userName = worksheet[row, col++].DisplayText;
                    var password = worksheet[row, col++].DisplayText;
                    var fullName = worksheet[row, col++].DisplayText;
                    var email = worksheet[row, col++].DisplayText;
                    var roleName = worksheet[row, col].DisplayText;
                    UserInitModel item = new()
                    {
                        UserName = userName,
                        Password = password,
                        FullName = fullName,
                        Email = email,
                        RoleName = roleName
                    };

                    items.Add(item);
                }
            }

            //Closing the workbook.
            workbook.Close();

            cancellationToken.ThrowIfCancellationRequested();
            return await Task.FromResult(items);
        }
    }
}
