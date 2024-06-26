using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using FirecrackerSharp.Host.Ssh;
using Renci.SshNet;

namespace FirecrackerSharp.Host.Ssh.Tests;

public class SshHostFixture(string username = "root", string password = "root123") : IAsyncLifetime
{
    private static IContainer? Container { get; set; }
    private static ConnectionInfo ConnectionInfo { get; set; } = null!;

    protected SshClient SshClient { get; private set; } = null!;
    protected SftpClient SftpClient { get; private set; } = null!;
    
    public async Task InitializeAsync()
    {
        if (Container != null)
        {
            Connect();
            return;
        }

        var hostSshPort = Random.Shared.Next(10000, 65536);
        Container = new ContainerBuilder()
            .WithImage("ssh_server:latest")
            .WithPortBinding(hostSshPort, 22)
            .Build();

        await Container.StartAsync();
        
        await Task.Delay(100); // wait until initialization of container

        ConnectionInfo = new ConnectionInfo(
            "127.0.0.1", hostSshPort, username, new PasswordAuthenticationMethod(username, password));
        
        SshHost.Configure(
            new ConnectionPoolConfiguration(
                ConnectionInfo,
                SshConnectionAmount: 2,
                SftpConnectionAmount: 2,
                KeepAliveInterval: TimeSpan.FromSeconds(1)),
            CurlConfiguration.Default,
            ShellConfiguration.Default);
        
        Connect();
    }

    private void Connect()
    {
        SshClient = new SshClient(ConnectionInfo);
        SshClient.Connect();
        
        SftpClient = new SftpClient(ConnectionInfo);
        SftpClient.Connect();
    }

    public Task DisposeAsync()
    {
        SshClient.Disconnect();
        SftpClient.Disconnect();
        
        return Task.CompletedTask;
    }
}