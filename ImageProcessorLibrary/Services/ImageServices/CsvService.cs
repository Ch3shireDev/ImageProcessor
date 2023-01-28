using System.Globalization;
using CsvHelper;
using ImageProcessorLibrary.DataStructures;

namespace ImageProcessorLibrary.Services.ImageServices;

/// <summary>
///     Serwis do konwersji listy wektorów cech do formatu CSV.
/// </summary>
public class CsvService
{
    /// <summary>
    ///     Konwertuje listę wektorów cech do formatu CSV.
    /// </summary>
    /// <param name="featureVectors"></param>
    /// <returns></returns>
    public string ToText(List<FeatureVector> featureVectors)
    {
        using var writer = new StringWriter();

        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

        csv.WriteRecords(featureVectors);

        return writer.ToString();
    }
}