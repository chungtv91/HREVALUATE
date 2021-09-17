using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace HRE.Web.UI.Seeds
{
    public abstract class BaseSeed
    {
        public virtual string Template { get; } = "HR_Init_Data.xlsx";

        public abstract Task RunAsync(CancellationToken cancellationToken);

        protected virtual async Task<T> ReadTemplateAs<T>(CancellationToken cancellationToken)
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, "Seeds", "Data", Template);
            if (!File.Exists(filePath)) return default;

            // Work with JSON.
            var json = await File.ReadAllTextAsync(filePath, cancellationToken);
            var obj = JsonSerializer.Deserialize<T>(json);
            return obj;
        }
    }
}
