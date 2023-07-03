namespace KeyboardKata.Domain.Extensions.Nullability
{
    public static class NullIntegrityExtensions
    {
        public static bool HasNullIntegrity<T>(this T target) where T : notnull
        {
            return target.HasNullIntegrity(out string _);
        }

        public static bool HasNullIntegrity<T>(this T target, out string errorSummary) where T : notnull
        {
            NullIntegrityTester<T> tester = new(target);

            bool result = tester.TestIntegrity();

            errorSummary = tester.ErrorSummary;

            return result;
        }
    }
}
