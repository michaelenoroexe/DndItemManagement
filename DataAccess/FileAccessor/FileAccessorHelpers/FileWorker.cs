namespace FileAccessor.FileAccessorHelpers
{
    internal class FileWorker
    {
        private string path;

        public FileWorker(string path)
        {
            this.path = path;
        }

        public string? GetItemsString()
        {
            if (!File.Exists(path)) return null;
            using var stream = new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read));
            return stream.ReadToEnd();
        }

        public void SaveItems(string items)
        {
            if (File.Exists(path)) File.Delete(path);

            using var stream = new StreamWriter(new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write));
            stream.WriteLine(items);
        }
    }
}