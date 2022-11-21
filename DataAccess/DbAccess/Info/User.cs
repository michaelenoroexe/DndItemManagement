namespace DbAccess.Info
{
    public class User
    {
        public int UserId { get; set; }

        public string UserName { get; set; } = null!;

        public UserItem[]? Items { get; set; }
    }
}
