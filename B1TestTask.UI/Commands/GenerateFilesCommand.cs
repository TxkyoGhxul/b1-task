using B1TestTask.UI.Commands.Base;
using B1TestTask.UI.Extensions;
using B1TestTask.UI.Models;
using System.IO;

namespace B1TestTask.UI.Commands;
internal class GenerateFilesCommand : Command.WithParams<GenerateFilesCommand.ExecuteParams>
{
    public static Action<string> OnGenerated;

    protected override void Execute(ExecuteParams @params)
    {
        var random = new Random();
        var currentFileNumber = 1;
        for (int i = 0; i < @params.CountFilesToGenerate; i++)
        {
            using var streamWriter = new StreamWriter(File.OpenWrite($"{@params.FilesNamePrefix}{currentFileNumber++}.txt"));
            for (int j = 0; j < @params.CountRowsPerFile; j++)
            {
                var randomRowData = GetRandomRowData(random);
                streamWriter.WriteLine(randomRowData);
            }
        }

        OnGenerated?.Invoke("Generated");
    }

    protected override bool ValidateParams(ExecuteParams @params) =>
        !string.IsNullOrWhiteSpace(@params.FilesNamePrefix) &&
        @params.CountRowsPerFile > 0 &&
        @params.CountFilesToGenerate > 0;

    private static RandomRowData GetRandomRowData(Random random)
    {
        //Hardcoded with task values :)
        var date = random.DateForTheLastYears(5);
        var latinString = random.LatinStringWithLength(10);
        var russianString = random.RussianStringWithLength(10);
        var evenNumber = random.PositiveEvenInRange(2..100_000_000);
        var number = random.PositiveInRangeWithDecimalPlaces(1..20, 8);

        var randomRowData = new RandomRowData(date, latinString, russianString, evenNumber, number);
        return randomRowData;
    }

    public record ExecuteParams(string FilesNamePrefix, int CountFilesToGenerate, int CountRowsPerFile);
}
