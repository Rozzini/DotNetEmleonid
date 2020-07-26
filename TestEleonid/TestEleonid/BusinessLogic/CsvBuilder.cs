using CsvHelper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TestEleonid.Models;

namespace TestEleonid.BusinessLogic
{
    public static class CsvBuilder
    {

        public static MemoryStream ExportHelper(Func<Statuses?, Types?, IEnumerable<UserTransaction>> repositoryGetAllTransactions, Statuses? status, Types? type)
        {
            var memoryStream = new MemoryStream();
            var streamWriter = new StreamWriter(memoryStream);
            var csvWriter = new CsvWriter(streamWriter, System.Globalization.CultureInfo.CurrentCulture);
            csvWriter.WriteRecords(repositoryGetAllTransactions(status, type));
            streamWriter.Flush();
            memoryStream.Position = 0;
            return memoryStream;
        }

        public static void ImportHelper(Action<List<UserTransaction>> repositoryAddOrUpdateTransactions, IFormFile file)
        {
            if (file != null)
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<UserTransaction>().ToList<UserTransaction>();
                    repositoryAddOrUpdateTransactions(records);
                }
            }
        }
    }
}
