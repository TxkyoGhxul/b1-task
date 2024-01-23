namespace B1TestTask.UI.Models.Base;

/// <summary>
/// Сальдо
/// </summary>
public abstract class Balance : Entity
{
    public decimal Active { get; set; }
    public decimal Passive { get; set; }
}