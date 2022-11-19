namespace DataAccess
{
    public class Item : IEquatable<Item>
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }

        #region IEquatable
        public bool Equals(Item? other) => other is not null && other.ItemId == ItemId;
        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (obj.GetType() != typeof(Item)) return false;
            return Equals((Item)obj);
        }
        public override int GetHashCode() => ItemId;
        #endregion
    }
}
