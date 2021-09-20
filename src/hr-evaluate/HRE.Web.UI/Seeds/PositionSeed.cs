using HRE.Core.DI;
using HRE.Core.Extensions;
using HRE.Core.Identity;
using HRE.EntityFrameworkCore;
using HRE.Web.UI.Seeds.Models;
using Microsoft.EntityFrameworkCore;
using Syncfusion.XlsIO;

namespace HRE.Web.UI.Seeds
{
    public class PositionSeed : BaseSeed, ITransientDependency
    {
        private readonly ILogger _logger;
        private readonly HrDbContext _dbContext;

        public PositionSeed(ILogger<PositionSeed> logger, HrDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public override async Task RunAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("LevelSeed started.");
            var positions = await _dbContext.Positions.ToListAsync(cancellationToken: cancellationToken);
            if (positions.Count > 0) return;

            var records = await ReadExcelTemplateAs(cancellationToken);
            foreach (var record in records)
            {
                var position = positions.SingleOrDefault(x => x.PositionName == record.Name);
                position = new()
                {
                    PositionName = record.Name
                };
                await _dbContext.Positions.AddAsync(position);
            }
            await _dbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("PositionSeed success.");
        }

        private async Task<List<LevelInitModel>> ReadExcelTemplateAs(CancellationToken cancellationToken)
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, "Seeds", "Data", Template);
            var items = Activator.CreateInstance<List<LevelInitModel>>();
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
            IWorksheet worksheet = workbook.Worksheets.FirstOrDefault(x => x.Name == "Positions");
            if (worksheet != null)
            {
                var usedRange = worksheet.UsedRange;
                var lastRow = usedRange.LastRow;
                for (int row = 2; row <= lastRow; row++)
                {
                    var col = 1;
                    var no = worksheet[row, col++].DisplayText.AsInt32();
                    var name = worksheet[row, col++].DisplayText;
                    LevelInitModel item = new()
                    {
                        Name = name
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
