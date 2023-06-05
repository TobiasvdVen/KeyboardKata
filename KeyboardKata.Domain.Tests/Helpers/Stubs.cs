using System.Collections.Generic;
using System.Linq;

namespace KeyboardKata.Domain.Tests.Helpers
{
    internal static class Stubs
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

        public static InputStream Inputs(params Input[] inputs)
        {
            return new InputStream(inputs);
        }

        public static Pattern Pattern(params Input[] inputs)
        {
            List<SubPattern> subPatterns = new();

            foreach (Input input in inputs)
            {
                subPatterns.Add(new SubPattern(input.Key, Enumerable.Empty<Key>()));
            }

            return new Pattern(subPatterns);
        }

        public static Sequence Linear(params string[] inputs)
        {
            return new Sequence(LinearInputs(inputs));
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
