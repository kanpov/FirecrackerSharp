using System.Text.Json.Serialization;
using FirecrackerSharp.Data.Ballooning;
using FirecrackerSharp.Data.Drives;
using FirecrackerSharp.Data.Observability;

namespace FirecrackerSharp.Data;

public record VmConfiguration(
    [property: JsonPropertyName("boot-source")]
    VmBootSource BootSource,
    [property: JsonPropertyName("machine-config")]
    VmMachineConfiguration MachineConfiguration,
    [property: JsonPropertyName("drives")]
    IEnumerable<VmDrive> Drives,
    [property: JsonPropertyName("balloon")]
    VmBalloon? Balloon = null,
    [property: JsonPropertyName("logger")]
    VmLogger? Logger = null);
