namespace FileAccessor.FileAccessorHelpers
{
    internal class FileWorker
    {
        private string _path;

        public FileWorker(string path)
        {
            _path = path;
        }

        public string? GetItemsString()
        {
            if (!File.Exists(_path)) return null;
            using var stream = new StreamReader(new FileStream(_path, FileMode.Open, FileAccess.Read));
            return stream.ReadToEnd();
        }

        public void SaveItems(string items)
        {
            using var stream = new StreamWriter(new FileStream(_path, FileMode.OpenOrCreate, FileAccess.Write));
            stream.WriteLine(items);
        }
    }
}