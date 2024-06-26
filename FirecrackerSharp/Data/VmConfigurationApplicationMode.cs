namespace FirecrackerSharp.Data;

/// <summary>
/// Defines how a <see cref="VmConfiguration"/> should be applied to the Firecracker process from a technical
/// standpoint.
/// </summary>
public enum VmConfigurationApplicationMode
{
    /// <summary>
    /// Apply the <see cref="VmConfiguration"/> through serializing it to a temporary JSON configuration file and
    /// passing that JSON's path into the process
    /// </summary>
    JsonConfiguration,
    /// <summary>
    /// Apply the <see cref="VmConfiguration"/> through sequentially making calls to the Management API pre-boot
    /// endpoints.
    /// </summary>
    SequentialApiCalls,
    /// <summary>
    /// Apply the <see cref="VmConfiguration"/> through making parallelized (through tasks) calls to the Management
    /// API pre-boot endpoints.
    /// </summary>
    ParallelizedApiCalls
}