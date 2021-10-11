using System;

namespace AlphaTest.Infrastructure
{
    internal static class VariableStorage
    {
        public static string DatabaseServer => "ALPHATEST_SERVER";

        public static string MigratorLogin => "ALPHATEST_MIGRATOR_LOGIN";

        public static string MigratorPassword => "ALPHATEST_MIGRATOR_PASSWORD";

        public static string SqlLogin => "ALPHATEST_SQL_LOGIN";

        public static string SqlPassword => "ALPHATEST_SQL_PASSWORD";

        public static string ReadVariable(string variable)
        {
            string value = Environment.GetEnvironmentVariable(variable);
            if (value is null)
                throw new InvalidOperationException($"Невозможно выполнить операцию - переменная {variable} не задана.");
            return value;
        }
    }
}
