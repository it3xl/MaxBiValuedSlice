 // ReSharper disable once CheckNamespace
public class BiSlice
{
    private Item _initialItem;
    private Item _otherItem;
    private Item _lastItem;
    private readonly PlainSlice _previousPlainSlice;
    private int _directSize;

    private bool HasPreviousValues
    {
        get
        {
            if (_previousPlainSlice == null)
            {
                return false;
            }
            var previousValue = _previousPlainSlice.Value;
            if (previousValue == _initialItem.Value)
            {
                return true;
            }
            if (
                // It's the case of a last slice in an array.
                _otherItem == null
                // Otherwise we should have an other value.
                || _otherItem.Value == previousValue)
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
                return _directSize + _previousPlainSlice.Count();
            }

            return _directSize;
        }
    }

    public bool Stopped { get; private set; }

    public BiSlice(PlainSlice previousPlainSlice)
    {
        _previousPlainSlice = previousPlainSlice;
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

        if (_initialItem == null)
        {
            _initialItem = item;

            return;
        }

        if (_initialItem.Value == item.Value)
        {
            return;
        }

        if (_otherItem == null)
        {
            _otherItem = item;
        }
    }

    private bool LastValueAlient()
    {
        var lastValue = _lastItem.Value;

        return lastValue != _initialItem.Value
               && lastValue != _otherItem.Value;
    }

}