namespace LogisticAPI.DatabaseContext
{
    public class DbContextFactory : IDbContextFactory
    {
        private readonly IDictionary<string, BaseContext> _context;

        public DbContextFactory(IDictionary<string, BaseContext> context)
        {
            _context = context;
        }

        public BaseContext GetContext(string contextName)
        {
            return _context[contextName];
        }
    }
}
