using DataAccess;
using DataAccess.Interfaces;
using FileAccessor.FileAccessorHelpers;

namespace FileAccessor
{
    internal sealed class FileAccessor : IDataAccessor
    {
        private FileWorker _storageManager;
        private List<Item>? _storage;

        public FileAccessor()
        {
            _storageManager = new FileWorker(Path.Combine(Environment.CurrentDirectory, "items.json"));      
        }

        public IList<Item> GetItems()
        {
            return _storage = _storageManager.GetItemsString()?.GetItemList() ?? new List<Item>();
        }

        public void SaveItems()
        {
            if (_storage != null) _storageManager.SaveItems(_storage.GetJsonString());
        }
    }
}
