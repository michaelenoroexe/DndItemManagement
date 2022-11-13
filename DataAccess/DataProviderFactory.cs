using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class DataProviderFactory
    {
        private static IDataAccessor _dataAccesser;

        static DataProviderFactory()
        {
            _dataAccesser = new ();
        }

        public static IDataProvider GetDataProvider() { }

        public static ISaveProvider GetSaveProvider() { }
    }
}
