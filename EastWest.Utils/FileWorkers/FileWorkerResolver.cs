using EastWest.Utils.FileWorkers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EastWest.Utils.FileWorkers
{
    public class FileWorkerResolver
    {
        public IFileWorker Resolve(string filePath)
        {
            var ext = Path.GetExtension(filePath);

            return true switch
            {
                true when ext.EndsWith(".json") => new JsonWorkerStrategy(),
                true when ext.EndsWith(".xlsx") => new XlsxFileWorkerStrategy(),
                true when ext.EndsWith(".csv") => new CsvFileWorkerStrategy(),
                _ => new XmlFileWorkerStrategy(),
            };
        }
    }
}
