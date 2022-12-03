using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EastWest.Utils.ExternalWorkers.FTPWorker
{
    /// <summary>
    /// Далеко не оптимальный и неказыстый класс, но уже лет 5 работает 
    /// </summary>
    public class FTPGeneric
    {
        private readonly string ftpServerIP;
        private readonly string ftpUserID;
        private readonly string ftpPassword;

        public FTPGeneric(string ftpServerIP, string ftpUserID, string ftpPassword)
        {
            this.ftpServerIP = ftpServerIP;
            this.ftpUserID = ftpUserID;
            this.ftpPassword = ftpPassword;
        }
        
        public async Task DeleteFile(string fileName)
        {
            throw new NotImplementedException();
        }

        public async Task<List<string>> GetFilesInfoFromDirectoryAsync(string path)
        {
            FtpWebRequest Request = (FtpWebRequest)WebRequest.Create($"ftp://{ftpServerIP}/{path}");
            Request.Method = WebRequestMethods.Ftp.ListDirectory;
            Request.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
            FtpWebResponse Response = (FtpWebResponse)Request.GetResponse();
            Stream ResponseStream = Response.GetResponseStream();
            StreamReader Reader = new StreamReader(ResponseStream);

            List<string> files = new List<string>();

            while (!Reader.EndOfStream)
            {
                var line = await Reader.ReadLineAsync();
                files.Add(line.ToString());
            }

            Response.Close();
            ResponseStream.Close();
            Reader.Close();
            return files;
        }

        public async Task DownLoad(string ftpPath, string localPath)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create($"ftp://{ftpServerIP}/{ftpPath}");
            request.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            using (Stream ftpStream = (await request.GetResponseAsync()).GetResponseStream())
            using (Stream fileStream = File.Create(localPath))
            {
                ftpStream.CopyTo(fileStream);
            }
        }
    }
}
