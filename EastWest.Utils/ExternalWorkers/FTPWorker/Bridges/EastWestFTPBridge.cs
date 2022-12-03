using EastWest.Utils.ExternalWorkers.FTPWorker.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EastWest.Utils.ExternalWorkers.FTPWorker.Bridges
{
    public class EastWestFTPBridge : FTPGeneric, IEastWestFTPBridge
    {
        public EastWestFTPBridge(IConfigurationRoot configuration) : base(configuration["FTP_SETTINGS:IP"], configuration["FTP_SETTINGS:USER_ID"], configuration["FTP_SETTINGS:PASSWORD"])
        { }
    }
}
