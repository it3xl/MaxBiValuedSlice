 // ReSharper disable once CheckNamespace
public class PlainSlice
{
    public PlainSlice(Item item)
    {
        Value = item.Value;
        _startIndex = item.Index;
    }

    public int Value { get; }
    private readonly int _startIndex;
    private int? _lastIndex;

    public void SetLastIndex(Item item)
    {
        _lastIndex = item.Index;
    }

    public int Count()
    {
        if (!_lastIndex.HasValue)
        {
            return 1;
        }

        return _lastIndex.Value - _startIndex + 1;
    }
}