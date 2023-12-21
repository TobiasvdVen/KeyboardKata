using KeyboardKata.App.ViewModels;
using KeyboardKata.Domain.InputMatching;
using KeyboardKata.Domain.InputProcessing;
using System;

namespace KeyboardKata.App
{
    public class ShortcutContextManager : IPatternProcessor, INavigationService<ViewModel>
    {
        public ShortcutContextManager(ViewModel initial)
        {
            Current = initial;
        }

        public ViewModel Current { get; }

        public void GoBack()
        {
            throw new NotImplementedException();
        }

        public void GoTo(ViewModel destination)
        {
            throw new NotImplementedException();
        }

        public void Process(IPattern pattern)
        {
            Current.Process(pattern);
        }
    }
}
