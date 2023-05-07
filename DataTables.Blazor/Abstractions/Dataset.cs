using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataTables.Blazor.Abstractions;

public interface IDataset
{
    IEnumerable<object> GetData();
}

public class Dataset<T> : IDataset, IEnumerable<T>
{
    private readonly List<T> _data;

    public Dataset()
    {
        _data = new List<T>();
    }

    public Dataset(IEnumerable<T> data)
    {
        _data = data.ToList();
    }

    public void Add(T item) => _data.Add(item);

    public IEnumerator<T> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => _data.GetEnumerator();

    public IEnumerable<object> GetData() => _data.Cast<object>();
}