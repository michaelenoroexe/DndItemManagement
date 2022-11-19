using DataAccess.Interfaces;

namespace FileAccessor
{
    public static class AccessorFactory
    {
        private static Lazy<FileAccessor> _accessor;

        static AccessorFactory() => _accessor = new Lazy<FileAccessor>();

        public static IDataAccessor GetAccessor() => _accessor.Value;
    }
}
