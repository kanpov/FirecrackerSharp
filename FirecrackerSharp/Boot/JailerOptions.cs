namespace FirecrackerSharp.Boot;

/// <summary>
/// The optional and mandatory options to be passed into the jailer binary.
/// </summary>
/// <param name="LinuxGid">The Linux group ID, under which the jailer should run after doing all superuser operations</param>
/// <param name="LinuxUid">The Linux user ID, under which the jailer should run after doing all superuser operations</param>
/// <param name="SudoPassword">If privilege escalation is required for the current host, it is required to pass this
/// password that will be passed to "su" in order to manually launch an escalated shell</param>
/// <param name="ChrootBaseDirectory">The base directory for chroot jails</param>
/// <param name="ExtraArguments">Any extra CLI arguments to pass into the jailer binary. Refer to Firecracker's
/// documentation as to which are possible</param>
/// <param name="WaitMillisAfterJailing">The amount of milliseconds to wait for the jail to start up the underlying
/// Firecracker process</param>
public record JailerOptions(
    ulong LinuxGid,
    ulong LinuxUid,
    string? SudoPassword = null,
    string ChrootBaseDirectory = "/srv/jailer",
    string ExtraArguments = "",
    uint WaitMillisAfterJailing = 200)
{
    internal string FormatToArguments(string firecrackerBinary, string vmId)
    {
        return
            $"--exec-file {firecrackerBinary} --id {vmId} --gid {LinuxGid} --uid {LinuxUid} --chroot-base-dir {ChrootBaseDirectory} {ExtraArguments}";
    }
}