using System.Text;

namespace KeyboardKata.Domain.Extensions.Nullability
{
    internal static class NullIntegrityTesterFactory
    {
        public static NullIntegrityTester FromType(Type type, object target, StringBuilder errorSummaryBuilder, Stack<string> path)
        {
            Type testerType = typeof(NullIntegrityTester<>).MakeGenericType(type);

            object? activated = Activator.CreateInstance(testerType, target, errorSummaryBuilder, path);

            if (activated is NullIntegrityTester tester)
            {
                return tester;
            }

            throw new ArgumentException($"Failed to construct NullIntegrityTester based on type {type} and target {target} (of type {target.GetType()}");
        }

        public static NullIntegrityTester FromTarget(object target, StringBuilder errorSummaryBuilder, Stack<string> path)
        {
            return FromType(target.GetType(), target, errorSummaryBuilder, path);
        }
    }
}
