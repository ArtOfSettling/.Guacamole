﻿using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace WellFired.Guacamole.FileSystem
{
    public interface IFileSystem
    {
        Task<Stream> GetStream(string path, FileMode mode, CancellationToken cancellationToken);
    }
}