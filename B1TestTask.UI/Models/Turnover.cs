using B1TestTask.UI.Models.Base;

namespace B1TestTask.UI.Models;

public class Turnover : Entity
{
    public decimal Debit { get; set; }
    public decimal Credit { get; set; }
}