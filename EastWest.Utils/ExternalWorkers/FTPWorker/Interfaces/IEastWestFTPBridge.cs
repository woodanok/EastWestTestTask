using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EastWest.Utils.ExternalWorkers.FTPWorker.Interfaces
{
    public interface IEastWestFTPBridge
    {
        Task DeleteFile(string fileName);
        Task<List<string>> GetFilesInfoFromDirectoryAsync(string path);
        Task DownLoad(string ftpPath, string localPath);
    }
}
