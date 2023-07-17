using KeyboardKata.Domain.InputMatching;

namespace KeyboardKata.Domain.InputProcessing
{
    public interface IPatternProcessor
    {
        void Process(IPattern pattern);
    }
}
