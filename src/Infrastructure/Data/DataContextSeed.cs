using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Arbetsprov.Core.Entities;
using Microsoft.EntityFrameworkCore;
using CsvHelper;

namespace Arbetsprov.Infrastructure.Data
{
    /// <summary>
    /// Provides functions to seed the database
    /// </summary>
    public static class DataContextSeed
    {
        public static readonly CSV_DataInfo InitialData = new CSV_DataInfo
        {
            ResourceName = "Arbetsprov.Infrastructure.Data.SeedData.price_detail.csv",
            Delimiter = "\t"
        };

        public static async Task SeedCSV(DataContext ctx)
        {
            // TODO: a better way to seed only if we haven't seeded before?
            if (!ctx.PriceDetails.Any())
            {
                try
                {
                    using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(InitialData.ResourceName))
                    using (var reader = new CsvReader(new StreamReader(stream, Encoding.UTF8), CultureInfo.InvariantCulture))
                    {
                        // reader.Configuration.RegisterClassMap<PriceDetailCSVMapper>();
                        reader.Configuration.TypeConverterOptionsCache.GetOptions<DateTime?>().NullValues.AddRange(new[] { "NULL", "0" });
                        reader.Configuration.Delimiter = InitialData.Delimiter;

                        foreach (var item in reader.GetRecords<PriceDetail>().ToList().AsReadOnly())
                            ctx.AddOrUpdate(item);

                        // We have to set IDENTITY_INSERT on PriceDetail to insert values with specified Primary key
                        await ctx.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT PriceDetail ON");
                        await ctx.SaveChangesAsync();
                        await ctx.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT PriceDetail OFF");
                    }
                }
                catch (Exception e)
                {
                    // TODO: Add better logging system
                    Debug.WriteLine(e);
                }
            }
        }
    }
    /// <summary>
    /// Provides meta info about a CSV file
    /// </summary>
    public struct CSV_DataInfo
    {
        // Manifest resource path
        public string ResourceName;
        // Value delimiter 
        public string Delimiter;
    }
}
