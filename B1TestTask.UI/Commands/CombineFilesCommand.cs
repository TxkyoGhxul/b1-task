using B1TestTask.UI.Commands.Base;
using B1TestTask.UI.Extensions;
using System.IO;

namespace B1TestTask.UI.Commands;
internal class CombineFilesCommand : Command.WithParams<CombineFilesCommand.ExecuteParams>
{
    public static Action<string> OnCombined;

    protected override void Execute(ExecuteParams @params)
    {
        var deletedLines = 0;
        using var commonFileWriter = new StreamWriter(@params.CommonFileName);
        var filesToCombine = GetFilesToCombine(@params.FilesPrefix);
        foreach (var file in filesToCombine)
        {
            using var reader = new StreamReader(file);
            if (@params.RemoveLinesWithContainsValue)
            {
                deletedLines += reader.RemoveAllLinesContaining(@params.ContainsValue, originalPath: file, onWriting: commonFileWriter.WriteLine);
            }
            else
            {
                reader.ReadLinesWithAction(commonFileWriter.WriteLine);
            }
        }

        OnCombined?.Invoke($"Combined -> Deleted {deletedLines} rows");
    }

    protected override bool ValidateParams(ExecuteParams @params) =>
        !string.IsNullOrWhiteSpace(@params.CommonFileName) &&
        !string.IsNullOrWhiteSpace(@params.FilesPrefix);

    private static IEnumerable<string> GetFilesToCombine(string filesPrefix) =>
        Directory.EnumerateFiles(Directory.GetCurrentDirectory())
            .Select(pathString => Path.GetFileName(pathString))
            .Where(fileName => fileName.StartsWith(filesPrefix) &&
                               fileName.EndsWith(".txt"));

    public record ExecuteParams(string FilesPrefix, string CommonFileName, string ContainsValue, bool RemoveLinesWithContainsValue);
}
