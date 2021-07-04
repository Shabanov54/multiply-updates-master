using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Xml;

namespace MultiPlyUpdates{
    public class Filer {
        private const string nodes = "nodes.ini";
        private const string updatePacket = @"1551001.upd";
        private const string contentFile = @"Contents.xml";
        private string tempDir = "temp";
        private string doneDIr = "done";

        internal void Preparation()
        {
            ZipFile.ExtractToDirectory(updatePacket,tempDir);
        }

        internal void Finaller()
        {
            Directory.Delete(tempDir, true);
        }

        internal void CreateCopyUpdate(string node)
        {
            XmlDocument xmlDocument = new XmlDocument();
            var xmlFile = Path.Combine(tempDir, contentFile);
            xmlDocument.Load(xmlFile);
            XmlElement element = (XmlElement)xmlDocument.SelectSingleNode("//PACKET");
            element.SetAttribute("TO", node);
            using (TextWriter text = new StreamWriter(xmlFile, false, Encoding.GetEncoding(1251)))
            {
                xmlDocument.Save(text);
            }

            //string[] files = Directory.GetFiles(tempDir);
            string zipFile = $"1551{node}.upd";
            ZipFile.CreateFromDirectory(tempDir, Path.Combine(doneDIr, zipFile), CompressionLevel.Optimal, false, Encoding.GetEncoding(1251));
            Console.WriteLine($"Создан пакет {zipFile}");
        }
        public Filer()
        {
            if(Directory.Exists(tempDir))
            {
                Directory.Delete(tempDir,true);
            }

            Directory.CreateDirectory(tempDir);

            if(Directory.Exists(doneDIr))
            {
                Directory.Delete(doneDIr, true);
            }

            Directory.CreateDirectory(doneDIr);
        }
        public string [] GetNodes()
        {
            return File.ReadAllLines(nodes, Encoding.Default).ToArray();
        }
    }
}