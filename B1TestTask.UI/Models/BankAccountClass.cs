using B1TestTask.UI.Models.Base;

namespace B1TestTask.UI.Models;
public class BankAccountClass : Entity
{
    public int Number { get; set; }
    public string Title { get; set; }
    public List<BankAccount> BankAccounts { get; set; }

    public BankAccountClass() // EF
    {
    }

    public BankAccountClass(Guid id, int number, string title, List<BankAccount> bankAccounts)
    {
        Id = id;
        Number = number;
        Title = title;
        BankAccounts = bankAccounts;
    }

    public override string ToString() => Title;

    public static BankAccountClass Create(int number, string title) =>
        new(Guid.NewGuid(), number, title, bankAccounts: []);
}
