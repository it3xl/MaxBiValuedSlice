using System.Diagnostics;

// ReSharper disable once CheckNamespace
// !!! Remove the public modifier as in Codility.
public class Solution
{
    [DebuggerStepThrough]
    // ReSharper disable InconsistentNaming
    public int solution(int[] A)
    {
        var processor = new BiValuedSliceProcessor(A);
        processor.Process();

        return processor.MaxSize;
    }
}