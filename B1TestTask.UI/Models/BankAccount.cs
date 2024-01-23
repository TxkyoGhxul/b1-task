using B1TestTask.UI.Models.Base;

namespace B1TestTask.UI.Models;

public class BankAccount : Entity
{
    public int Number { get; set; }
    public BankAccountClass Class { get; set; }
    public InputBalance InputBalance { get; set; }
    public OutputBalance OutputBalance { get; set; }
    public Turnover Turnover { get; set; }
}
