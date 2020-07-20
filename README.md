<div align="center">

[![GitHub license](https://img.shields.io/badge/license-Apache%202-blue.svg?style=flat-square)](https://raw.githubusercontent.com/lewisbennett/homekit-setup-payload-generator-net/master/README.md)

</div>


# HomeKit Setup Payload Generator .NET

A .NET library for generating Apple HomeKit setup payloads.

## Usage
Create a new setup payload by calling `HKSetupPayload.Make`. Setup codes can be generated at random by calling `HKSetupCode.RandomSetupCode`, and pre-existing setup codes can be validated by calling `HKSetupCode.IsValid`.

View [sample project](https://github.com/lewisbennett/homekit-setup-payload-generator-net/tree/master/Samples/BulkPayloadSample) for example usage.

## Technical Payload Breakdown: `X-HM://0075OVT5A1A2B`

This is the format of a HomeKit setup payload: `X-HM://<version, category, flags and setup code><setup ID>`. In the example payload above, `0075OVT5A` is made up of the version (0), category (7), flags (BLE) and setup code (123-45-678), and `1A2B` is the setup ID.

`<version, category, flags and setup code><setup ID>` is 9 characters in length and represents the following 46-bit value encoded as a base-36 string:
* Bits 45-43, version - should be set to 0\`b000 in most cases.
* Bits 42-39, reserved - should be set to 0\`b0000 in most cases.
* Bit 30 - accessory supports WiFi Accessory Configuration (WAC) for configuring wireless credentials.
* Bit 29 - accessory supports HAP over BLE transport.
* Bit 28 - accessory supports HAP over IP transport; MUST be ON if bit 30 is ON.
* Bit 27 - accessory is paired with a controlled (only for accessories using programmable NFC tags to advertise the setup payload).
* Bits 26-0, setup code - maximum of 27 bits to encode 8-digit setup code.

The setup ID is included with no changes. It can however, only be 4 alphanumeric (0-9, A-Z) characters.
