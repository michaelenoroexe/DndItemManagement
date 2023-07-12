namespace Administration.Service.Contracts;

public interface IHasher
{
    string HashPassword(string unhashedPassword);
    bool VerifyPassword(string hashedPassword, string password);
}
