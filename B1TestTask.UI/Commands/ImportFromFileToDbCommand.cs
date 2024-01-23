﻿using B1TestTask.UI.Commands.Base;
using B1TestTask.UI.Data.Repositories.Base;
using B1TestTask.UI.Models;
using System.Diagnostics;
using System.IO;

namespace B1TestTask.UI.Commands;
internal class ImportFromFileToDbCommand : Command.WithParams<ImportFromFileToDbCommand.ExecuteParams>
{
    public static Action<string> OnRowImporting;

    private const int _trackAfterCount = 1000;

    private readonly IRepository<RandomRowData> _repository;

    public ImportFromFileToDbCommand(IRepository<RandomRowData> repository) =>
        _repository = repository;

    protected override async void Execute(ExecuteParams @params)
    {
        var elapsedSeconds = await RunWithTimeCounter(async () =>
        {
            var rows = new List<RandomRowData>();
            var countLines = File.ReadLines(@params.CommonFileName).Count();
            var currentLine = 0;
            var lines = File.ReadLines(@params.CommonFileName);
            foreach (var line in lines)
            {
                OnRowImporting?.Invoke(StatusMessageForUser(currentLine++, countLines));

                var dataRow = RandomRowData.FromLine(line);
                if (dataRow is null) continue;

                rows.Add(dataRow);
                if (rows.Count == _trackAfterCount)
                {
                    await _repository.AddRangeAsync(rows);
                    rows.Clear();
                }
            }

            await _repository.AddRangeAsync(rows);
        });

        OnRowImporting?.Invoke($"Finished in {elapsedSeconds:F3}s");
    }

    protected override bool ValidateParams(ExecuteParams @params) =>
        !string.IsNullOrWhiteSpace(@params.CommonFileName);

    private static string StatusMessageForUser(int currentLine, int countLines)
    {
        double percentage = Math.Round((double)currentLine / countLines * 100, 3);
        return $"{currentLine} / {countLines} -> {percentage}%";
    }

    private static async Task<double> RunWithTimeCounter(Func<Task> func)
    {
        var stopwatch = Stopwatch.StartNew();

        await func?.Invoke();

        stopwatch.Stop();

        return stopwatch.Elapsed.TotalSeconds;
    }

    public record ExecuteParams(string CommonFileName);
}
