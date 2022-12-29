using DataAccess;
using DataAccess.Interfaces;
using FileAccessor.FileAccessorHelpers;
using System.Collections;

namespace FileAccessor
{
    internal sealed class FileAccessor : IDataAccessor
    {
        private FileWorker storageManager;
        private List<Item>? storage;

        public FileAccessor()
        {
            storageManager = new FileWorker(Path.Combine(Environment.CurrentDirectory, "items.json"));      
        }

        public IList<Item> GetItems()
        {
            return storage = storageManager.GetItemsString()?.GetItemList() ?? new List<Item>();
        }
        
        public void SaveItems()
        {
            if (storage != null) storageManager.SaveItems(storage.GetJsonString());
        }
    }
}
