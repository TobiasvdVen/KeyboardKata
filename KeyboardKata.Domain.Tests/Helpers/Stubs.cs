using KeyboardKata.Domain.Actions;
using KeyboardKata.Domain.Actions.Pools;
using KeyboardKata.Domain.InputMatching;
using KeyboardKata.Domain.InputProcessing;
using System.Collections.Generic;

namespace KeyboardKata.Domain.Tests.Helpers
{
    public static class Stubs
    {
        public static Key Key(string key)
        {
            return new TestKey(key);
        }

        public static Input Down(string key)
        {
            return new Input(Key(key), KeyPress.Down);
        }

        public static Input Up(string key)
        {
            return new Input(Key(key), KeyPress.Up);
        }

        public static ExactMatchPattern Pattern(params Input[] inputs)
        {
            return new ExactMatchPattern(inputs);
        }

        public static KeyboardAction Action(string prompt, params Input[] inputs)
        {
            return new KeyboardAction(prompt, Pattern(inputs));
        }

        public static IEnumerable<KeyboardAction> Actions(params KeyboardAction[] keyboardActions)
        {
            return keyboardActions;
        }

        public static SingleActionPool Single(KeyboardAction keyboardAction, int? repeats = null)
        {
            return new SingleActionPool(keyboardAction.Prompt, keyboardAction.Pattern, repeats);
        }

        public static LinearActionPool Linear(int repeats, params KeyboardActionPool[] pools)
        {
            return new LinearActionPool(pools, repeats);
        }

        public static LinearActionPool Linear(params KeyboardActionPool[] pools)
        {
            return new LinearActionPool(pools, repeats: null);
        }

        private static IEnumerable<Input> LinearInputs(params string[] inputs)
        {
            foreach (string input in inputs)
            {
                yield return Down(input);
                yield return Up(input);
            }
        }
    }
}
