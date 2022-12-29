using DataAccess.Interfaces;

namespace FileAccessor
{
    public static class AccessorFactory
    {
        private static Lazy<FileAccessor> accessor;

        static AccessorFactory() => accessor = new Lazy<FileAccessor>();

        public static IDataAccessor GetAccessor() => accessor.Value;
    }
}
