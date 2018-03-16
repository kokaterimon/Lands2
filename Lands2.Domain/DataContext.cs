//para conectarme a una base de datos por ADO
namespace Lands2.Domain
{
    using System.Data.Entity;

    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {

        }
    }
}
