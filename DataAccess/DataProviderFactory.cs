using DataAccess.DataAccessors;
using DataAccess.Interfaces;

namespace DataAccess
{
    public static class DataProviderFactory
    {
        private static IDataAccessor _dataAccessor;

        static DataProviderFactory()
        {
            _dataAccessor = new FileAccessor();
        }

        public static IDataAccessor GetDataAccessor() => _dataAccessor;
    }
}
