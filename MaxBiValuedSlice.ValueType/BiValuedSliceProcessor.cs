using System.Diagnostics;

// ReSharper disable once CheckNamespace
public class BiValuedSliceProcessor
{
    private readonly int[] _ints;
    private PlainSlice? _lastPlainSlice;
    private PlainSlice? _beforeLastPlainSlice;

    [DebuggerStepThrough]
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