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
