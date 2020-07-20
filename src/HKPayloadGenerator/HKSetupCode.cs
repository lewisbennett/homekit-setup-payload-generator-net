using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace HKPayloadGenerator
{
    public static class HKSetupCode
    {
        #region Fields
        private static readonly string[] _invalidSetupCodes = new[] { "00000000", "11111111", "22222222", "33333333", "44444444", "55555555", "66666666", "77777777", "88888888", "99999999", "123456789", "87654321" };
        private static readonly Random _random = new Random();
        private static readonly Regex _setupCodeRegex = new Regex("[0-9]{8}");
        #endregion

        #region Public Methods
        /// <summary>
        /// Checks whether a HomeKit setup code is valid.
        /// </summary>
        public static bool IsValid(string setupCode)
        {
            var mutableSetupCode = setupCode?.Replace("-", string.Empty);

            return !string.IsNullOrWhiteSpace(mutableSetupCode) && _setupCodeRegex.IsMatch(mutableSetupCode) && !_invalidSetupCodes.Contains(mutableSetupCode);
        }

        /// <summary>
        /// Creates a random, valid HomeKit setup code.
        /// </summary>
        public static string RandomSetupCode()
        {
            string setupCode = null;

            while (setupCode == null)
            {
                setupCode = _random.Next(10000000, 99999999).ToString().Substring(0, 8);

                if (IsValid(setupCode))
                    break;
                else
                    setupCode = null;
            }

            return setupCode.Insert(3, "-").Insert(6, "-");
        }
        #endregion
    }
}
