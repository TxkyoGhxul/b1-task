using B1TestTask.UI.Commands;
using B1TestTask.UI.Data.Readers.Base;
using B1TestTask.UI.Data.Repositories.Base;
using B1TestTask.UI.Models;
using B1TestTask.UI.ViewModels.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace B1TestTask.UI.ViewModels;
internal class MainWindowViewModel : BaseViewModel
{
    #region Title : string - Window title

    /// <summary>
    /// Window title
    /// </summary>
    private string _Title = "Main window";

    /// <summary>
    /// Window title
    /// </summary>
    public string Title
    {
        get => _Title;
        set => Set(ref _Title, value);
    }

    #endregion

    #region FilesNamePrefix : string - Prefix for generating files

    /// <summary>
    /// Prefix for generating files
    /// </summary>
    private string _FilesNamePrefix = "RandomTaskData_";

    /// <summary>
    /// Prefix for generating files
    /// </summary>
    public string FilesNamePrefix
    {
        get => _FilesNamePrefix;
        set
        {
            if (Set(ref _FilesNamePrefix, value))
            {
                OnPropertyChanged(nameof(GenerateFilesCommandParams));
                OnPropertyChanged(nameof(CombineFilesCommandParams));
            }
        }
    }

    #endregion

    #region CountFilesToGenerate : int - Count files to generate

    /// <summary>
    /// Count files to generate
    /// </summary>
    private int _CountFilesToGenerate = 100;

    /// <summary>
    /// Count files to generate
    /// </summary>
    public int CountFilesToGenerate
    {
        get => _CountFilesToGenerate;
        set
        {
            if (Set(ref _CountFilesToGenerate, value))
            {
                OnPropertyChanged(nameof(GenerateFilesCommandParams));
            }
        }
    }

    #endregion

    #region CountRowsPerFile : int - Count rows per file

    /// <summary>
    /// Count rows per file
    /// </summary>
    private int _CountRowsPerFile = 100;

    /// <summary>
    /// Count rows per file
    /// </summary>
    public int CountRowsPerFile
    {
        get => _CountRowsPerFile;
        set
        {
            if (Set(ref _CountRowsPerFile, value))
            {
                OnPropertyChanged(nameof(GenerateFilesCommandParams));
            }
        }
    }

    #endregion

    #region GenerateFilesCommandParams : GenerateFilesCommand.ExecuteParams - Generate files command params

    /// <summary>
    /// Generate files command params
    /// </summary>
    public GenerateFilesCommand.ExecuteParams GenerateFilesCommandParams =>
        new(FilesNamePrefix, CountFilesToGenerate, CountRowsPerFile);

    #endregion

    #region CommonFileName : string - Common file name

    /// <summary>
    /// Common file name
    /// </summary>
    private string _CommonFileName = "CommonFileName.txt";

    /// <summary>
    /// Common file name
    /// </summary>
    public string CommonFileName
    {
        get => _CommonFileName;
        set
        {
            if (Set(ref _CommonFileName, value))
            {
                OnPropertyChanged(nameof(CombineFilesCommandParams));
                OnPropertyChanged(nameof(ImportFromFileToDbCommandParams));
            }
        }
    }

    #endregion

    #region ContainsValue : string - Contains value for removing lines

    /// <summary>
    /// Contains value for removing lines
    /// </summary>
    private string _ContainsValue = "2020";

    /// <summary>
    /// Contains value for removing lines
    /// </summary>
    public string ContainsValue
    {
        get => _ContainsValue;
        set
        {
            if (Set(ref _ContainsValue, value))
            {
                OnPropertyChanged(nameof(CombineFilesCommandParams));
            }
        }
    }

    #endregion

    #region RemoveLinesWithContainsValue : bool - Flag to remove / not remove lines with value

    /// <summary>
    /// Flag to remove / not remove lines with value
    /// </summary>
    private bool _RemoveLinesWithContainsValue = true;

    /// <summary>
    /// Flag to remove / not remove lines with value
    /// </summary>
    public bool RemoveLinesWithContainsValue
    {
        get => _RemoveLinesWithContainsValue;
        set
        {
            if (Set(ref _RemoveLinesWithContainsValue, value))
            {
                OnPropertyChanged(nameof(CombineFilesCommandParams));
            }
        }
    }

    #endregion

    #region CombineFilesCommandParams : CombineFilesCommand.ExecuteParams - Params to execute combine files command

    /// <summary>
    /// Params to execute combine files command
    /// </summary>
    public CombineFilesCommand.ExecuteParams CombineFilesCommandParams =>
        new(FilesNamePrefix, CommonFileName, ContainsValue, RemoveLinesWithContainsValue);

    #endregion

    #region ImportFromFileToDbCommandParams : ImportFromFileToDbCommand.ExecuteParams - Params to execute import from file to db command

    /// <summary>
    /// Params to execute import from file to db command
    /// </summary>
    public ImportFromFileToDbCommand.ExecuteParams ImportFromFileToDbCommandParams =>
        new(CommonFileName);

    #endregion

    #region ExcelFilePath : string - Excel file path

    /// <summary>
    /// Excel file path
    /// </summary>
    private string _ExcelFilePath;

    /// <summary>
    /// Excel file path
    /// </summary>
    public string ExcelFilePath
    {
        get => _ExcelFilePath;
        set
        {
            if (Set(ref _ExcelFilePath, value))
            {
                OnPropertyChanged(nameof(ImportExcelToDbCommandParams));
            }
        }
    }

    #endregion

    #region ImportExcelToDbCommandParams : ImportExcelToDbCommand.ExecuteParams - Params to execute excel import to db command

    /// <summary>
    /// Params to execute excel import to db command
    /// </summary>
    public ImportExcelToDbCommand.ExecuteParams ImportExcelToDbCommandParams =>
        new(ExcelFilePath);

    #endregion

    #region InputFiles : List<InputFile> - Loaded files

    /// <summary>
    /// Loaded files
    /// </summary>
    private List<InputFile> _InputFiles;

    /// <summary>
    /// Loaded files
    /// </summary>
    public List<InputFile> InputFiles
    {
        get => _InputFiles;
        set => Set(ref _InputFiles, value);
    }

    #endregion

    #region SelectedInputFile : InputFile - Selected input file

    /// <summary>
    /// Selected input file
    /// </summary>
    private InputFile _SelectedInputFile;

    /// <summary>
    /// Selected input file
    /// </summary>
    public InputFile SelectedInputFile
    {
        get => _SelectedInputFile;
        set => Set(ref _SelectedInputFile, value);
    }

    #endregion

    #region SelectedTabItemIndex : int - Selected tab item index

    /// <summary>
    /// Selected tab item index
    /// </summary>
    private int _SelectedTabItemIndex;

    /// <summary>
    /// Selected tab item index
    /// </summary>
    public int SelectedTabItemIndex
    {
        get => _SelectedTabItemIndex;
        set => Set(ref _SelectedTabItemIndex, value);
    }

    #endregion

    #region ReadingInputFile : InputFile - Reading input file

    /// <summary>
    /// Selected input file
    /// </summary>
    private InputFile _ReadingInputFile;

    /// <summary>
    /// Selected input file
    /// </summary>
    public InputFile ReadingInputFile
    {
        get => _ReadingInputFile;
        set => Set(ref _ReadingInputFile, value);
    }

    #endregion

    #region SelectedBankAccountClass : BankAccountClass - Selected bank account class of the selected input file

    /// <summary>
    /// Selected bank account class of the selected input file
    /// </summary>
    private BankAccountClass _SelectedBankAccountClass;

    /// <summary>
    /// Selected bank account class of the selected input file
    /// </summary>
    public BankAccountClass SelectedBankAccountClass
    {
        get => _SelectedBankAccountClass;
        set => Set(ref _SelectedBankAccountClass, value);
    }

    #endregion

    #region SelectedClassAccounts : ObservableCollection<BankAccount> - Selected class accounts

    /// <summary>
    /// Selected class accounts
    /// </summary>
    private ObservableCollection<BankAccount> _SelectedClassAccounts = new();

    /// <summary>
    /// Selected class accounts
    /// </summary>
    public ObservableCollection<BankAccount> SelectedClassAccounts
    {
        get => _SelectedClassAccounts;
        set => Set(ref _SelectedClassAccounts, value);
    }

    #endregion


    private readonly IRepository<RandomRowData> _randomDataRepository;
    private readonly IRepository<BankAccount> _bankAccountRepository;
    private readonly IRepository<InputFile> _inputFileRepository;
    private readonly IExcelReader<BankAccountClass> _excelReader;

    public MainWindowViewModel(IRepository<RandomRowData> randomDataRepository, IExcelReader<BankAccountClass> excelReader, IRepository<InputFile> inputFileRepository, IRepository<BankAccount> bankAccountRepository)
    {
        _randomDataRepository = randomDataRepository;
        _bankAccountRepository = bankAccountRepository;
        _inputFileRepository = inputFileRepository;
        _excelReader = excelReader;
        SubscribeOnEvents();
        GetLoadedFilesCommand.Execute(null);
    }

    public ICommand GenerateFilesCommand => new GenerateFilesCommand();

    public ICommand CombineWithContainsValueCommand => new CombineFilesCommand();

    public ICommand ImportFromFileToDbCommand => new ImportFromFileToDbCommand(_randomDataRepository);

    public ICommand ImportExcelToDbCommand => new ImportExcelToDbCommand(_excelReader, _inputFileRepository);

    #region GetFileDataCommand

    public ICommand GetFileDataCommand => new LambdaCommand(OnGetFileDataCommandExecutedAsync);

    public async void OnGetFileDataCommandExecutedAsync(object p)
    {
        ReadingInputFile = await _inputFileRepository.Get(file => file.Id == SelectedInputFile.Id)
            .Include(file => file.AccountClasses)
            .FirstOrDefaultAsync();

        SelectedTabItemIndex = 1;
    }

    #endregion

    #region GetAccountByClassCommand

    public ICommand GetAccountByClassCommand => new LambdaCommand(OnGetAccountByClassCommandExecuted, CanGetAccountByClassCommandExecute);

    public bool CanGetAccountByClassCommandExecute(object p) => SelectedBankAccountClass is not null;

    public async void OnGetAccountByClassCommandExecuted(object p)
    {
        var selectedClassAccounts = await _bankAccountRepository
            .Get(account => account.Class == SelectedBankAccountClass)
            .OrderBy(account => account.Number)
            .Include(account => account.InputBalance)
            .Include(account => account.OutputBalance)
            .Include(account => account.Turnover)
            .ToListAsync();

        SelectedClassAccounts = new ObservableCollection<BankAccount>(selectedClassAccounts);
    }

    #endregion

    #region ChooseExcelFileCommand

    public ICommand ChooseExcelFileCommand => new LambdaCommand(OnChooseExcelFileCommandExecuted);

    public void OnChooseExcelFileCommandExecuted(object p)
    {
        var openFileDialog = new OpenFileDialog
        {
            Filter = "Excel Files|*.xls;*.xlsx|All Files|*.*",
            Title = "Select an Excel File"
        };

        if (!openFileDialog.ShowDialog() ?? false)
        {
            return;
        }

        ExcelFilePath = openFileDialog.FileName;
    }

    #endregion

    #region GetLoadedFilesCommand

    public ICommand GetLoadedFilesCommand => new LambdaCommand(OnGetLoadedFilesCommandExecuted);

    public void OnGetLoadedFilesCommandExecuted(object p)
    {
        InputFiles = _inputFileRepository.GetAll().OrderBy(file => file.FileName).ToList();
    }

    #endregion

    private void SetStatusToTitle(string title)
    {
        Title = title;
    }

    private void SubscribeOnEvents()
    {
        Commands.GenerateFilesCommand.OnGenerated += SetStatusToTitle;
        Commands.CombineFilesCommand.OnCombined += SetStatusToTitle;
        Commands.ImportFromFileToDbCommand.OnRowImporting += SetStatusToTitle;
        Commands.ImportExcelToDbCommand.OnImportFinished += SetStatusToTitle;
    }
}
