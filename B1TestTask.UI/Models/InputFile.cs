using B1TestTask.UI.Models.Base;

namespace B1TestTask.UI.Models;

public class InputFile : Entity
{
    public string FileName { get; set; }
    public List<BankAccountClass> AccountClasses { get; set; }

    public override string ToString() => FileName;
}