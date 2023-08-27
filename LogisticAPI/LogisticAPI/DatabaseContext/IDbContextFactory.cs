namespace LogisticAPI.DatabaseContext
{
    public interface IDbContextFactory
    {
        BaseContext GetContext(string v);
    }
}