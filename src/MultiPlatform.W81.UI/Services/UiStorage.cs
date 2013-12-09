using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using MultiPlatform.Domain.Interfaces;

namespace MultiPlatform.Shared.Services
{
    public class UiStorage : IStorage 
    {


        private static readonly StorageFolder Storage = ApplicationData.Current.LocalFolder;


        public Task CopyFile(string sourceFileName, string destinationFileName)
        {
            return CopyFile(sourceFileName, destinationFileName, false);
        }


        public async Task CopyFile(string sourceFileName, string destinationFileName, bool overwrite)
        {
            var file = await Storage.GetFileAsync(sourceFileName);

            var destinationFolderName = Path.GetDirectoryName(destinationFileName);
            var destinationFolder = await Storage.GetFolderAsync(destinationFolderName);

            destinationFileName = Path.GetFileName(destinationFileName);

            var nameCollisionOption = overwrite ? NameCollisionOption.ReplaceExisting : NameCollisionOption.FailIfExists;

            await file.CopyAsync(destinationFolder, destinationFileName, nameCollisionOption);
        }

        public async Task CreateDirectory(string dir)
        {
            await Storage.CreateFolderAsync(dir);
        }


        public Task<Stream> CreateFile(string path)
        {
            return Storage.OpenStreamForWriteAsync(path, CreationCollisionOption.ReplaceExisting);
        }


        public async Task DeleteDirectory(string dir)
        {
            var folder = await Storage.GetFolderAsync(dir);

            await folder.DeleteAsync();
        }


        public async Task DeleteFile(string path)
        {
            var file = await Storage.GetFileAsync(path);

            await file.DeleteAsync();
        }


        public async Task<bool> DirectoryExists(string dir)
        {
            return await FileExists(dir);
        }


        public async Task<bool> FileExists(string path)
        {
           
                var file = await Storage.TryGetItemAsync(path);

                return file != null;
           
        }


        public async Task<string[]> GetDirectoryNames()
        {
            var folders = await Storage.GetFoldersAsync();

            return folders
                .Select(x => x.Name)
                .ToArray();
        }


        public async Task<string[]> GetDirectoryNames(string searchPattern)
        {
            var folderName = Path.GetDirectoryName(searchPattern);
            var folder = string.IsNullOrEmpty(folderName) ? Storage : await Storage.GetFolderAsync(folderName);

            var folders = await folder.GetFoldersAsync();

            searchPattern = Path.GetFileName(searchPattern);

            if (string.IsNullOrEmpty(searchPattern))
            {
                return folders
                    .Select(x => x.Name)
                    .ToArray();
            }

            var regexPattern = FilePatternToRegex(searchPattern);

            return folders
                .Where(x => regexPattern.IsMatch(x.Name))
                .Select(x => x.Name)
                .ToArray();
        }


        public async Task<string[]> GetFileNames()
        {
            var files = await Storage.GetFilesAsync();

            return files
                .Select(x => x.Name)
                .ToArray();
        }


        public async Task<string[]> GetFileNames(string searchPattern)
        {
            var folderName = Path.GetDirectoryName(searchPattern);
            var folder = string.IsNullOrEmpty(folderName) ? Storage : await Storage.GetFolderAsync(folderName);

            var files = await folder.GetFilesAsync();

            searchPattern = Path.GetFileName(searchPattern);

            if (string.IsNullOrEmpty(searchPattern))
            {
                return files
                    .Select(x => x.Name)
                    .ToArray();
            }

            var regexPattern = FilePatternToRegex(searchPattern);

            return files
                .Where(x => regexPattern.IsMatch(x.Name))
                .Select(x => x.Name)
                .ToArray();
        }


        public Task<Stream> OpenFileForRead(string path)
        {
            return Storage.OpenStreamForReadAsync(path);
        }


        public async Task<string> ReadAllText(string path)
        {
            using (var fileStream = await OpenFileForRead(path))
            {
                using (var streamReader = new StreamReader(fileStream))
                {
                    return await streamReader.ReadToEndAsync();
                }
            }
        }


        public async Task<string> ReadAllText(string path, Encoding encoding)
        {
            using (var fileStream = await OpenFileForRead(path))
            {
                using (var streamReader = new StreamReader(fileStream, encoding))
                {
                    return await streamReader.ReadToEndAsync();
                }
            }
        }


        public async Task<string[]> ReadAllLines(string path)
        {
            using (var fileStream = await OpenFileForRead(path))
            {
                var lines = new List<string>();

                using (var streamReader = new StreamReader(fileStream))
                {
                    while (!streamReader.EndOfStream)
                    {
                        lines.Add(await streamReader.ReadLineAsync());
                    }
                }

                return lines.ToArray();
            }
        }


        public async Task<string[]> ReadAllLines(string path, Encoding encoding)
        {
            using (var fileStream = await OpenFileForRead(path))
            {
                var lines = new List<string>();

                using (var streamReader = new StreamReader(fileStream, encoding))
                {
                    while (!streamReader.EndOfStream)
                    {
                        lines.Add(await streamReader.ReadLineAsync());
                    }
                }

                return lines.ToArray();
            }
        }


        public async Task<byte[]> ReadAllBytes(string path)
        {
            using (var fileStream = await OpenFileForRead(path))
            {

                var memoryStream = fileStream as MemoryStream;

                using (memoryStream = new MemoryStream())
                {
                    await fileStream.CopyToAsync(memoryStream);

                    return memoryStream.ToArray();
                }

            }
        }


        public async Task WriteAllText(string path, string contents)
        {
            using (var fileStream = await CreateFile(path))
            {
                using (var streamWriter = new StreamWriter(fileStream))
                {
                    await streamWriter.WriteAsync(contents);
                }
            }
        }


        public async Task WriteAllText(string path, string contents, Encoding encoding)
        {
            using (var fileStream = await CreateFile(path))
            {
                using (var streamWriter = new StreamWriter(fileStream, encoding))
                {
                    await streamWriter.WriteAsync(contents);
                }
            }
        }


        public async Task WriteAllLines(string path, IEnumerable<string> contents)
        {
            using (var fileStream = await CreateFile(path))
            {
                using (var streamWriter = new StreamWriter(fileStream))
                {
                    foreach (var line in contents)
                    {
                        await streamWriter.WriteAsync(line);
                        await streamWriter.WriteAsync(Environment.NewLine);
                    }
                }
            }
        }


        public async Task WriteAllLines(string path, IEnumerable<string> contents, Encoding encoding)
        {
            using (var fileStream = await CreateFile(path))
            {
                using (var streamWriter = new StreamWriter(fileStream, encoding))
                {
                    foreach (var line in contents)
                    {
                        await streamWriter.WriteAsync(line);
                        await streamWriter.WriteAsync(Environment.NewLine);
                    }
                }
            }
        }


        public async Task WriteAllBytes(string path, byte[] bytes)
        {
            using (var fileStream = await CreateFile(path))
            {
                await fileStream.WriteAsync(bytes, 0, bytes.Length);
            }
        }


        public async Task AppendAllText(string path, string contents)
        {
            using (var fileStream = await Storage.OpenStreamForWriteAsync(path, CreationCollisionOption.OpenIfExists))
            {
                using (var streamWriter = new StreamWriter(fileStream))
                {
                    await streamWriter.WriteAsync(contents);
                }
            }
        }


        public async Task AppendAllText(string path, string contents, Encoding encoding)
        {
            using (var fileStream = await Storage.OpenStreamForWriteAsync(path, CreationCollisionOption.OpenIfExists))
            {
                using (var streamWriter = new StreamWriter(fileStream, encoding))
                {
                    await streamWriter.WriteAsync(contents);
                }
            }
        }


        public async Task AppendAllLines(string path, IEnumerable<string> contents)
        {
            using (var fileStream = await Storage.OpenStreamForWriteAsync(path, CreationCollisionOption.OpenIfExists))
            {
                using (var streamWriter = new StreamWriter(fileStream))
                {
                    foreach (var line in contents)
                    {
                        await streamWriter.WriteAsync(line);
                        await streamWriter.WriteAsync(Environment.NewLine);
                    }
                }
            }
        }

        public async Task AppendAllLines(string path, IEnumerable<string> contents, Encoding encoding)
        {
            using (var fileStream = await Storage.OpenStreamForWriteAsync(path, CreationCollisionOption.OpenIfExists))
            {
                using (var streamWriter = new StreamWriter(fileStream, encoding))
                {
                    foreach (var line in contents)
                    {
                        await streamWriter.WriteAsync(line);
                        await streamWriter.WriteAsync(Environment.NewLine);
                    }
                }
            }
        }

        private Regex FilePatternToRegex(string pattern)
        {
            var regexPattern = Regex.Replace(pattern, "[*?]|[^*?]+", m =>
            {
                switch (m.Value)
                {
                    case "*":
                        return ".*";

                    case "?":
                        return ".";

                    default:
                        return Regex.Escape(m.Value);
                }
            });

            return new Regex(regexPattern);
        }
    }



}
