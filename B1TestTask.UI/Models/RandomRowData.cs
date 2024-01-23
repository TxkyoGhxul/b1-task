using B1TestTask.UI.Models.Base;

namespace B1TestTask.UI.Models;
public class RandomRowData : Entity, IEquatable<RandomRowData?>
{
    public DateTime Date { get; set; }
    public string LatinString { get; set; }
    public string RussianString { get; set; }
    public int PositiveEvenNumber { get; set; }
    public double PositiveNumber { get; set; }

    public RandomRowData() //EF needed
    {
    }

    public RandomRowData(DateTime date, string latinString, string russianString, int positiveEvenNumber, double positiveNumber)
    {
        Date = date;
        LatinString = latinString;
        RussianString = russianString;
        PositiveEvenNumber = positiveEvenNumber;
        PositiveNumber = positiveNumber;
    }

    public override string ToString() =>
        $"{Date:dd.MM.yyyy}||{LatinString}||{RussianString}||{PositiveEvenNumber}||{PositiveNumber:F8}||";

    public override bool Equals(object? obj) =>
        obj is RandomRowData rowData && Equals(rowData);

    public bool Equals(RandomRowData? other) =>
        other is not null && Id.Equals(other.Id);

    public override int GetHashCode() => Id.GetHashCode();

    public static bool operator ==(RandomRowData? left, RandomRowData? right) => left?.Equals(right) ?? false;

    public static bool operator !=(RandomRowData? left, RandomRowData? right) => !(left == right);

    public static RandomRowData Create(DateTime date, string latinString, string russianString, int positiveEvenNumber, double positiveNumber) => 
        new(date, latinString, russianString, positiveEvenNumber, positiveNumber)
        {
            Id = Guid.NewGuid()
        };

    public static RandomRowData? FromLine(string line)
    {
        var props = line.Split("||");
        if (props.Length != 6) 
        {
            return null;
        }

        bool isDate = DateTime.TryParse(props[0], out var date);
        bool isInt = int.TryParse(props[3], out var evenNumber);
        bool isDouble = double.TryParse(props[4], out var positiveDouble);
        if (!isDate || !isInt || !isDouble)
        {
            return null;
        }

        return Create(date, props[1], props[2], evenNumber, positiveDouble);
    }
}
