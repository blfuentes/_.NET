using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formatter
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileUTF8 = @"./Data/utf8.txt";
            string fileUTF8_BOM = @"./Data/utf8_bom.txt";

            var files = Directory.GetFiles(@"./Data");
            foreach (var file in files)
            {
                Encoding encoding = Checker.GetEncoding(file);
                Console.WriteLine("File: {0} - Detected: {1}", Path.GetFileName(file), encoding.ToString());
                using (Stream fs = File.Open(file, FileMode.Open))
                {
                    byte[] rawData = new byte[fs.Length];
                    encoding = EncodingTools.DetectInputCodepage(rawData);
                    //encoding = TextFileEncodingDetector.DetectTextByteArrayEncoding(rawData);
                    //encoding = TextFileEncodingDetector.DetectUnicodeInByteSampleByHeuristics(rawData);
                }
                //encoding = TextFileEncodingDetector.DetectTextFileEncoding(file);
                if (encoding != null)
                    Console.WriteLine("File: {0} - Real: {1}", Path.GetFileName(file), encoding.ToString());

            }

            //if(File.Exists(fileUTF8))
            //{
            //    Encoding encFileUTF8 = Checker.GetEncoding(fileUTF8);
            //    encFileUTF8 = TextFileEncodingDetector.DetectTextFileEncoding(fileUTF8);
            //}
            //if (File.Exists(fileUTF8_BOM))
            //{
            //    Encoding encFileUTF8BOM = Checker.GetEncoding(fileUTF8_BOM);
            //    encFileUTF8BOM = TextFileEncodingDetector.DetectTextFileEncoding(fileUTF8_BOM);
            //}

            Console.ReadKey();
        }
    }
}
