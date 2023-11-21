using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using System.Diagnostics;

public class CsvParserService
{
public List<T> ParseCsv<T>(string filePath) where T : new()
{
    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
    {
        PrepareHeaderForMatch = args => args.Header.ToUpper()

    };

    using (var reader = new StreamReader(filePath))
    using (var csv = new CsvReader(reader, config))
    {
        var records = csv.GetRecords<T>().ToList();

        return records;
    }
}
}