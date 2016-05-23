using System;
using System.IO;
using System.Text;

namespace IoDemo
{
    class SimpleFileReading
    {
        private readonly string _path;

        public SimpleFileReading(string path)
        {
            _path = path;
        }

        public string ReadAll()
        {
            using (var tr = new StreamReader(
                new FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.Read),
                Encoding.UTF8,
                detectEncodingFromByteOrderMarks: true))
            {
                // return tr.ReadToEnd();

                StringBuilder sb = new StringBuilder();
                string line;
                while ((line = tr.ReadLine()) != null)
                {
                    sb.AppendLine(line);

                    //sb.Append(line);
                    //sb.Append(Environment.NewLine);
                }
               
                tr.Close();

                return sb.ToString();
            }

            //TextReader tr = null;
            //try
            //{
            //    tr = new StreamReader(
            //        new FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.Read),
            //        Encoding.UTF8,
            //        detectEncodingFromByteOrderMarks: true);
            //    return tr.ReadToEnd();
            //}
            //finally
            //{
            //    if (tr != null)
            //    {
            //        tr.Dispose();
            //    }
            //}
        }
    }
}
