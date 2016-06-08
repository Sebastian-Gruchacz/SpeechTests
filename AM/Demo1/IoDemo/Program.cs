using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IoDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var sourceFileBuilder = new PathBuilder(@"WikiSample.xml");
            //Console.WriteLine(sourceFileBuilder.GetFullPath());
            //var reader = new SimpleFileReading(sourceFileBuilder.GetFullPath());

            Console.OutputEncoding = Encoding.UTF8;
            //Console.WriteLine(reader.ReadAll());

            //int a, b;
            //Console.WriteLine(a = b = 3);

            //PrintXmlSerializationDemo();
            WikiPageReader wrdr = new WikiPageReader();

            using (var sr = new FileStream(sourceFileBuilder.GetFullPath(), FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var model = wrdr.ReadModel(sr);

                Console.WriteLine(model.Title);
            }


#if DEBUG
                Console.WriteLine();
            Console.WriteLine("Press ENTER");
            Console.ReadLine();
#endif
        }

        private static void PrintXmlSerializationDemo()
        {
            var obj = new XmlSample(5, "Czarek");
            var ser = new XmlSerializer(typeof(XmlSample)); // obj.GetType()

            using (var memStr = new MemoryStream())
            {
                ser.Serialize(memStr, obj);

                memStr.Position = 0;
                var buff = memStr.GetBuffer();
                string buffContent = Encoding.UTF8.GetString(buff, 0, (int)memStr.Length);

                Console.WriteLine(buffContent);
            }
        }
    }
}
