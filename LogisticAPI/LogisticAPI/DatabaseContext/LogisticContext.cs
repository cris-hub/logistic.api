using Microsoft.EntityFrameworkCore;

namespace LogisticAPI.DatabaseContext
{
    public class LogisticContext : BaseContext
    {
        public LogisticContext(DbContextOptions<LogisticContext> options) : base(options)
        {

        }
    }
}
