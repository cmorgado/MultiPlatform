using System;
using System.Threading.Tasks;
namespace MultiPlatform.Domain.Interfaces
{
  public  interface IStorage
    {
        Task AppendAllLines(string path, System.Collections.Generic.IEnumerable<string> contents);
        Task AppendAllLines(string path, System.Collections.Generic.IEnumerable<string> contents, System.Text.Encoding encoding);
        Task AppendAllText(string path, string contents);
        Task AppendAllText(string path, string contents, System.Text.Encoding encoding);
        Task CopyFile(string sourceFileName, string destinationFileName);
        Task CopyFile(string sourceFileName, string destinationFileName, bool overwrite);
        Task CreateDirectory(string dir);
        Task<System.IO.Stream> CreateFile(string path);
        Task DeleteDirectory(string dir);
        Task DeleteFile(string path);
        Task<bool> DirectoryExists(string dir);
        Task<bool> FileExists(string path);
        Task<string[]> GetDirectoryNames();
        Task<string[]> GetDirectoryNames(string searchPattern);
        Task<string[]> GetFileNames();
        Task<string[]> GetFileNames(string searchPattern);
        Task<System.IO.Stream> OpenFileForRead(string path);
        Task<byte[]> ReadAllBytes(string path);
        Task<string[]> ReadAllLines(string path);
        Task<string[]> ReadAllLines(string path, System.Text.Encoding encoding);
        Task<string> ReadAllText(string path);
        Task<string> ReadAllText(string path, System.Text.Encoding encoding);
        Task WriteAllBytes(string path, byte[] bytes);
        Task WriteAllLines(string path, System.Collections.Generic.IEnumerable<string> contents);
        Task WriteAllLines(string path, System.Collections.Generic.IEnumerable<string> contents, System.Text.Encoding encoding);
        Task WriteAllText(string path, string contents);
        Task WriteAllText(string path, string contents, System.Text.Encoding encoding);
    }
}
