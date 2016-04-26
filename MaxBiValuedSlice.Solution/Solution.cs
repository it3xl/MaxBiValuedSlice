
// ReSharper disable once CheckNamespace
public class Solution
{
    // ReSharper disable InconsistentNaming
    public int solution(int[] A)
    {
        var processor = new BiValuedSliceProcessor(A);
        processor.Process();

        return processor.MaxSize;
    }
}

public struct Item
{
    public int Integer { get; }
    public int Index { get; }

    public Item(int integer, int index)
    {
        Integer = integer;
        Index = index;
    }
}

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

public class BiValuedSliceProcessor
{
    private readonly int[] _ints;
    private PlainSlice? _lastPlainSlice;
    private PlainSlice? _beforeLastPlainSlice;

    public BiValuedSliceProcessor(int[] ints)
    {
        _ints = ints;
    }

    public int MaxSize { get; private set; }

    public void Process()
    {
        var biSlice = new BiSlice(_lastPlainSlice);

        for (var i = 0; i < _ints.Length; i++)
        {
            var item = new Item(_ints[i], i);

            ProcessBiSlice(item, ref biSlice);
            ProcessLastPlainSlice(item);
        }

        SetMaxSize(biSlice.Size);
    }

    private void ProcessLastPlainSlice(Item item)
    {
        if (_lastPlainSlice.HasValue && _lastPlainSlice.Value.Integer == item.Integer)
        {
            var last = _lastPlainSlice.Value;
            last.SetLastIndex(item);
            _lastPlainSlice = last;

            return;
        }

        var beforeLastPlainSlice = _lastPlainSlice;
        _lastPlainSlice = new PlainSlice(item);
        ProcessBeforeLastPlainSlice(beforeLastPlainSlice);
    }

    private void ProcessBeforeLastPlainSlice(PlainSlice? beforeLastPlainSlice)
    {
        var old = _beforeLastPlainSlice;
        _beforeLastPlainSlice = beforeLastPlainSlice;
        if (old == null)
        {
            return;
        }

        var size = old.Value.Count + _beforeLastPlainSlice.Value.Count;

        SetMaxSize(size);
    }

    private void ProcessBiSlice(Item item, ref BiSlice biSlice)
    {
        biSlice.RegisterItem(item);
        if (!biSlice.Stopped)
        {
            return;
        }

        SetMaxSize(biSlice.Size);

        biSlice = new BiSlice(_lastPlainSlice);
        biSlice.RegisterItem(item);
    }

    private void SetMaxSize(int size)
    {
        if (size <= MaxSize)
        {
            return;
        }
        MaxSize = size;
    }
}

