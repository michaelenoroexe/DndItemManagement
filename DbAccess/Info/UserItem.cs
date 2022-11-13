namespace DbAccess.Info
{
    public class UserItem
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int ItemId { get; set; }

        public int ItemNumber { get; set; }

        public User User { get; set; } = null!;

        public Item Item { get; set; } = null!;
    }
}
