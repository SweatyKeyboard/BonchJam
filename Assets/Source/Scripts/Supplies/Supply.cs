
using System.Collections;

public class Supply : IEnumerable
{
    private SupplyItem[] _items;
    public SupplyItem this[int i]
    {
        get => _items[i];
        set => _items[i] = value;
    }

    public int Count => _items.Length;
    public Supply(SupplyItem[] items)
    {
        _items = items;
    }

    public IEnumerator GetEnumerator()
    {
        return _items.GetEnumerator();
    }
}
