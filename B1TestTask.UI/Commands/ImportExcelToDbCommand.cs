using B1TestTask.UI.Commands.Base;
using B1TestTask.UI.Data.Readers.Base;
using B1TestTask.UI.Data.Repositories.Base;
using B1TestTask.UI.Models;
using System.IO;

namespace B1TestTask.UI.Commands;
internal class ImportExcelToDbCommand : Command.WithParams<ImportExcelToDbCommand.ExecuteParams>
{
    public static Action<string> OnImportFinished;

    private readonly IExcelReader<BankAccountClass> _excelReader;
    private readonly IRepository<InputFile> _repository;

    public ImportExcelToDbCommand(IExcelReader<BankAccountClass> excelReader, IRepository<InputFile> repository)
    {
        _excelReader = excelReader;
        _repository = repository;
    }

    protected override async void Execute(ExecuteParams @params)
    {
        var accountClasses = _excelReader.Read(@params.Path);
        var inputFile = new InputFile
        {
            Id = Guid.NewGuid(),
            FileName = Path.GetFileName(@params.Path),
            AccountClasses = accountClasses.ToList()
        };
        await _repository.AddAsync(inputFile);
        OnImportFinished?.Invoke("Finished to import");
    }

    protected override bool ValidateParams(ExecuteParams @params) =>
        !string.IsNullOrWhiteSpace(@params.Path);

    public record ExecuteParams(string Path);
}
