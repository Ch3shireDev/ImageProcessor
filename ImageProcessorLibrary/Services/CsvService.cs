using System.Globalization;
using CsvHelper;

namespace ImageProcessorLibrary.Services;

public class CsvService
{
    public string ToText(List<FeatureVector> featureVectors)
    {
        using var writer = new StringWriter();

        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

        csv.WriteRecords(featureVectors);

        return writer.ToString();
    }
}