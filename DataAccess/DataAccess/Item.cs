namespace DataAccess
{
    public class Item : IEquatable<Item>
    {
        public Guid ItemId { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }

        public Item(Guid itemId, string name, int number)
        {
            ItemId = itemId;
            Name = name;
            Number = number;
        }
        private Item(string name, int number) : this(Guid.Empty, name, number) { }

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
