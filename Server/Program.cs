using DbAccess;

Console.WriteLine("Hi");

using (var dbContext = new ItemsContext())
{
    Console.WriteLine(dbContext.Database.ProviderName);

    Console.WriteLine(dbContext.Users.Count());
}
