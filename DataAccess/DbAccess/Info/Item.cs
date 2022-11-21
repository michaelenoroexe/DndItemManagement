namespace DbAccess.Info
{
    public sealed class Item
    {
        /// <summary>
        /// Id of item in database.
        /// </summary>
        public int ItemId { get; set; }
        /// <summary>
        /// Name of item.
        /// </summary>
        public string Name { get; set; } = null!;

        public UserItem[]? Users { get; set; }
    }
}
