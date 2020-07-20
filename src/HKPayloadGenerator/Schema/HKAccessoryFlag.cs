using System;

namespace HKPayloadGenerator.Schema
{
    [Flags]
    public enum HKAccessoryFlag
    {
        IP = 2,
        BLE = 4,
        WAC = 8
    }
}
