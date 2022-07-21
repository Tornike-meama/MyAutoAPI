using Microsoft.EntityFrameworkCore;
using MyAutoAPI1.Models;
using System.Threading.Tasks;

namespace MyAutoAPI1.BackgroundServices.MigrateDb
{
    public class MIgrationDb : IMigrateDb
    {
        private readonly MyDbContext _dbContext;
        public MIgrationDb(MyDbContext myDbContext)
        {
            _dbContext = myDbContext;
        }

        public void Migration()
        {
            _dbContext.Database.Migrate();
        }
    }
}
