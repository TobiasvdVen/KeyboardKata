using System.Collections.Generic;

namespace KeyboardKata.Domain.Tests.Helpers
{
    internal static class Stubs
    {
        public static Input Down(string key)
        {
            return new Input(new TestKey(key), KeyPress.Down);
        }

        public static Input Up(string key)
        {
            return new Input(new TestKey(key), KeyPress.Up);
        }

        public static IEnumerable<Input> Inputs(params Input[] inputs)
        {
            return inputs;
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
