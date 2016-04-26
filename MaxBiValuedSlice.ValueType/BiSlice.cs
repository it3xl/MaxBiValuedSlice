 // ReSharper disable once CheckNamespace
public struct BiSlice
{
    private Item? _initialItem;
    private Item? _otherItem;
    private Item? _lastItem;
    private readonly PlainSlice? _previousPlainSlice;
    private int _directSize;

    private bool HasPreviousValues
    {
        get
        {
            if (!_previousPlainSlice.HasValue)
            {
                return false;
            }
            if (!_initialItem.HasValue)
            {
                return false;
            }
            var previousValue = _previousPlainSlice.Value.Integer;
            if (previousValue == _initialItem.Value.Integer)
            {
                return true;
            }
            if (
                // It's the case of a last slice in an array.
                !_otherItem.HasValue
                // Otherwise we should have an other value.
                || _otherItem.Value.Integer == previousValue)
            {
                return true;
            }

            return false;
        }
    }

    public int Size
    {
        get
        {
            if (HasPreviousValues)
            {
                return _directSize + _previousPlainSlice.Value.Count;
            }

            return _directSize;
        }
    }

    public bool Stopped { get; private set; }

    public BiSlice(PlainSlice? previousPlainSlice)
    {
        _previousPlainSlice = previousPlainSlice;

        _initialItem = null;
        _otherItem = null;
        _lastItem = null;

        _directSize = 0;

        Stopped = false;
    }

    public void RegisterItem(Item item)
    {
        StoreItem(item);
        Stopped = LastValueAlient();
        if (Stopped)
        {
            return;
        }
        ++_directSize;
    }

    private void StoreItem(Item item)
    {
        _lastItem = item;

        if (!_initialItem.HasValue)
        {
            _initialItem = item;

            return;
        }

        if (_initialItem.Value.Integer == item.Integer)
        {
            return;
        }

        if (!_otherItem.HasValue)
        {
            _otherItem = item;
        }
    }

    private bool LastValueAlient()
    {
        if (!_otherItem.HasValue)
        {
            return false;
        }

        var lastValue = _lastItem.Value.Integer;



        return lastValue != _initialItem.Value.Integer
               && lastValue != _otherItem.Value.Integer;
    }

}