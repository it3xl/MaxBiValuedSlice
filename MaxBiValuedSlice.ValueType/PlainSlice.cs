 // ReSharper disable once CheckNamespace
public struct PlainSlice
{
    public PlainSlice(Item item)
    {
        Integer = item.Integer;
        _startIndex = item.Index;
        _lastIndex = -1;
    }

    public int Integer { get; }
    private readonly int _startIndex;
    private int _lastIndex;

    public void SetLastIndex(Item item)
    {
        _lastIndex = item.Index;
    }

    public int Count
    {
        get
        {
            if (_lastIndex == -1)
            {
                return 1;
            }

            return _lastIndex - _startIndex + 1;
        }
    }
}