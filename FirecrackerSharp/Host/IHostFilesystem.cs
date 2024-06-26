namespace FirecrackerSharp.Host;

public interface IHostFilesystem
{
    public static IHostFilesystem Current { get; set; } = null!;

    public Task WriteTextFileAsync(string path, string content);

    public void AppendTextFile(string path, string content);

    public Task WriteBinaryFileAsync(string path, byte[] content);

    public Task<string> ReadTextFileAsync(string path);

    public Task CopyFileAsync(string sourcePath, string destinationPath);

    public string GetTemporaryFilename();

    public bool FileOrDirectoryExists(string filename);

    void CreateTextFile(string filename);
    
    public void CreateDirectory(string path);

    public IEnumerable<string> GetSubdirectories(string path);

    public IEnumerable<string> GetFiles(string path);

    public Task ExtractGzipAsync(string archivePath, string destinationPath);

    public void MakeFileExecutable(string path);

    public void DeleteFile(string path);

    public void DeleteDirectoryRecursively(string path);

    public string CreateTemporaryDirectory();

    public string JoinPaths(params string[] paths);
}