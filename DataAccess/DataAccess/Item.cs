namespace DataAccess
{
    public class Item : IEquatable<Item>
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }

        public Item(int itemId, string name, int number)
        {
            ItemId = itemId;
            Name = name;
            Number = number;
        }
        /// <summary>
        /// Deserealize client request body to item without GUID to add in programm buffer.
        /// </summary>
        /// <param name="name">Name of item.</param>
        /// <param name="number">Number of item user have.</param>
        private Item(string name, int number) : this(0, name, number) { }

        #region IEquatable
        public bool Equals(Item? other) => other is not null && other.ItemId == ItemId;
        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (obj.GetType() != typeof(Item)) return false;
            return Equals((Item)obj);
        }
        public override int GetHashCode() => ItemId.GetHashCode();
        #endregion
    }
}
