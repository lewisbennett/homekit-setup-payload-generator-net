using HKPayloadGenerator;
using HKPayloadGenerator.Schema;
using System;

namespace BulkPayloadSample
{
    public static class Program
    {
        public static void Main()
        {
            const string setupId = "1A2B";

            var categories = new[]
            {
                HKAccessoryCategory.Outlets,
                HKAccessoryCategory.Thermostats,
                HKAccessoryCategory.Fans,
                HKAccessoryCategory.IPCameras
            };

            var flags = new[]
            {
                HKAccessoryFlag.BLE,
                HKAccessoryFlag.IP,
                HKAccessoryFlag.WAC,
                HKAccessoryFlag.IP | HKAccessoryFlag.WAC
            };

            var setupCodes = new[]
            {
                "123-45-678",
                "01234567",
                "34567890",
                HKSetupCode.RandomSetupCode()
            };

            for (var i = 0; i < setupCodes.Length; i++)
            {
                var setupCode = setupCodes[i];

                if (HKSetupCode.IsValid(setupCode))
                    Console.WriteLine(HKSetupPayload.Make(categories[i], setupCode, setupId, flags[i]));
            }

            Console.ReadLine();
        }
    }
}
