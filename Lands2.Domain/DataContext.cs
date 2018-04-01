//para conectarme a una base de datos por ADO
namespace Lands2.Domain
{
    using System.Data.Entity;

    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {

        }

        public System.Data.Entity.DbSet<Lands2.Domain.User> Users { get; set; }

        public System.Data.Entity.DbSet<Lands2.Domain.UserType> UserTypes { get; set; }
    }
}
