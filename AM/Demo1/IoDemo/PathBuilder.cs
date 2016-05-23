using System.IO;
using System.Reflection;

namespace IoDemo
{
    class PathBuilder
    {
        private readonly string _fileName;

        // TODO: Move 'fileName' parameter into GetFullPath() method instead
        public PathBuilder(string fileName)
        {
            _fileName = fileName;
        }

        public string GetFullPath()
        {
            return Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                _fileName);
        }
    }
}
