namespace B1TestTask.UI.Data.Readers.Base;
public interface IExcelReader<T> where T : class
{
    IEnumerable<T> Read(string path);
}
