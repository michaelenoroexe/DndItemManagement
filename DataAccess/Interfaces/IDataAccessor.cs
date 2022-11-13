namespace DataAccess.Interfaces
{
    public interface IDataAccessor
    {
        public IList<Item> GetItems();

        public void SaveItems();
    }
}
