using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace L01_2020GR603.models
{
    public class blogDB : DbContext
    {
        public blogDB(DbContextOptions<blogDB> options): base(options)
        {

        }
    }
}
