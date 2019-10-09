using System;
using System.IO;
using System.IO.Compression;
using Ionic.Zip;

namespace DataSaver
{

    class DataSaverProgram
    {
        string m_monitoredLocation
        {
            get; set;
        }
        double m_compressionRate
        {
            get; set;
        }
        int m_compressionTime
        {
            get; set;
        }

        struct myDirectory
        {
        }

        public void StartCompression(string monitoredLocation, double compressionRate, int compressionTime)
        {
            m_monitoredLocation = monitoredLocation;
            m_compressionRate = compressionRate;
            m_compressionTime = compressionTime;
            DateTime currentTime = DateTime.Now;
            ScanDirectory(m_monitoredLocation, currentTime);
        }

        public void ScanDirectory(string rootPath, DateTime currentTime)
        {
            string[] folderList = Directory.GetDirectories(rootPath);
            string[] fileList = Directory.GetFiles(rootPath);

            foreach(string folderPath in folderList)
            {
                ScanDirectory(folderPath, currentTime);
            }

            foreach(string filePath in fileList)
            {
                DateTime lastAccessTime = File.GetLastAccessTime(filePath);
                TimeSpan difference = currentTime - lastAccessTime;
                if(m_compressionTime < Convert.ToInt32(difference.TotalSeconds))
                {
                    CompressFile(filePath);
                }
            }
        }

        public void CompressFile(string filePath)
        {
            using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
            {
                zip.AddFile(filePath);
                zip.Save(filePath + ".zip");
            }
            File.WriteAllText(filePath, string.Empty);
        }
    }
}
