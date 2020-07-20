using HKPayloadGenerator.Schema;
using System;
using System.Text;
using System.Text.RegularExpressions;

namespace HKPayloadGenerator
{
    public static class HKSetupPayload
    {
        #region Fields
        private static readonly char[] _base36 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        private static readonly Regex _setupIdRegex = new Regex("[0-9A-Z]{4}");
        #endregion

        #region Public Methods
        /// <summary>
        /// Create a HomeKit setup payload.
        /// </summary>
        /// <param name="category">The HomeKit accessory category.</param>
        /// <param name="setupCode">The 8 digit HomeKit accessory setup code.</param>
        /// <param name="setupId">A unique 4 character, alphanumeric code.</param>
        /// <param name="flags">Flags for the HomeKit setup process.</param>
        /// <param name="reserved">The reserved section of the payload. This should be left as 0 in most cases.</param>
        /// <param name="version">The version section of the payload. This should be left as 0 in most cases.</param>
        public static string Make(HKAccessoryCategory category, string setupCode, string setupId, HKAccessoryFlag flags, int reserved = 0, int version = 0)
        {
            var mutableSetupCode = setupCode?.Replace("-", string.Empty);
            
            if (!HKSetupCode.IsValid(mutableSetupCode))
                throw new ArgumentException($"Invalid setup code provided: {setupCode}");

            if (!_setupIdRegex.IsMatch(setupId))
                throw new ArgumentException($"Invalid setup ID provided: {setupId}");

            var payload = 0L;
            payload |= version & 0x7L;

            payload <<= 4;
            payload |= reserved & 0x7L;

            payload <<= 8;
            payload |= (int)category & 0xFFL;

            payload <<= 4;
            payload |= (int)flags & 0xFL;

            payload <<= 27;
            payload |= int.Parse(mutableSetupCode) & 0x7FFFFFFFL;

            var stringBuilder = new StringBuilder();

            for (var i = 0; i < 9; i++)
            {
                stringBuilder.Insert(0, _base36[payload % 36]);
                payload /= 36;
            }

            return $"X-HM://{stringBuilder}{setupId}";
        }
        #endregion
    }
}
