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
    public class RoleSeed : BaseSeed, ITransientDependency
    {
        private readonly ILogger _logger;
        private readonly CustomRoleManager _roleManager;

        public RoleSeed(ILogger<RoleSeed> logger, CustomRoleManager roleManager)
        {
            _logger = logger;
            _roleManager = roleManager;
        }

        public override async Task RunAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("RoleSeed started.");
            var roles = await _roleManager.Roles.ToListAsync(cancellationToken: cancellationToken);
            if (roles.Count > 0) return;

            var records = await ReadExcelTemplateAs(cancellationToken);
            foreach (var record in records)
            {
                var role = roles.SingleOrDefault(x => string.Equals(x.Name, record.Name, StringComparison.OrdinalIgnoreCase));
                if (role == null)
                {
                    role = new()
                    {
                        Name = record.Name,
                        Description = record.Description
                    };
                    var identityResult = await _roleManager.CreateAsync(role);
                }
                else
                {
                    role.Description = record.Description;
                    var updateResult = await _roleManager.UpdateAsync(role);
                }
            }

            _logger.LogInformation("RoleSeed success.");
        }

        private async Task<List<RoleInitModel>> ReadExcelTemplateAs(CancellationToken cancellationToken)
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, "Seeds", "Data", Template);
            var items = Activator.CreateInstance<List<RoleInitModel>>();
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
            IWorksheet worksheet = workbook.Worksheets.FirstOrDefault(x => x.Name == "Roles");
            if (worksheet != null)
            {
                var usedRange = worksheet.UsedRange;
                var lastRow = usedRange.LastRow;
                for (int row = 2; row <= lastRow; row++)
                {
                    var col = 1;
                    var no = worksheet[row, col++].DisplayText.AsInt32();
                    var name = worksheet[row, col++].DisplayText;
                    var description = worksheet[row, col++].DisplayText;
                    RoleInitModel item = new()
                    {
                        Name = name,
                        Description = description
                    };

                    items.Add(item);
                }
            }

            // Closing the workbook.
            workbook.Close();

            cancellationToken.ThrowIfCancellationRequested();
            return await Task.FromResult(items);
        }
    }
}
